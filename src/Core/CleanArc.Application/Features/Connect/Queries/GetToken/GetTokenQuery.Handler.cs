using CleanArc.Domain.Contracts;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.Domain.Models.Jwt;

namespace CleanArc.Application.Features.Connect.Queries.GetToken;

public class GetTokenQueryHandler(IAppUserManager userManager, IJwtService jwtService) : IRequestHandler<GetTokenQuery,OperationResult<AccessTokenResponse>>
{
    private readonly IAppUserManager _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;

    public async ValueTask<OperationResult<AccessTokenResponse>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByUserName(request.UserName);

        if(user is null)
            return OperationResult<AccessTokenResponse>.FailureResult("User not found");

        var isUserLockedOut = await _userManager.IsUserLockedOutAsync(user);

        if(isUserLockedOut)
            if (user.LockoutEnd != null)
                return OperationResult<AccessTokenResponse>.FailureResult(
                    $"User is locked out. Try in {(user.LockoutEnd-DateTimeOffset.Now).Value.Minutes} Minutes");

        var passwordValidator = await _userManager.AdminLogin(user, request.Password);


        if (!passwordValidator.Succeeded)
        {
          var lockoutIncrementResult= await _userManager.IncrementAccessFailedCountAsync(user);

            return OperationResult<AccessTokenResponse>.FailureResult("Password is not correct");
        }

        var token= await _jwtService.GenerateAsync(user);


        return OperationResult<AccessTokenResponse>.SuccessResult(token);
    }
}