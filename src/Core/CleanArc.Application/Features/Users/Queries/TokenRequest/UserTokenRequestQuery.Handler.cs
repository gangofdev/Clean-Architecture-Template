using CleanArc.Domain.Contracts.Identity;
using Mediator;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Users.Queries.TokenRequest;

public class UserTokenRequestQueryHandler(IAppUserManager userManager, IMediator mediator, ILogger<UserTokenRequestQueryHandler> logger) : IRequestHandler<UserTokenRequestQuery,OperationResult<UserTokenRequestQueryResponse>>
{
    private readonly IAppUserManager _userManager = userManager;
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UserTokenRequestQueryHandler> _logger = logger;

    public async ValueTask<OperationResult<UserTokenRequestQueryResponse>> Handle(UserTokenRequestQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserByPhoneNumber(request.UserPhoneNumber);

        if(user is null)
            return OperationResult<UserTokenRequestQueryResponse>.NotFoundResult("User Not found");

        var code = user.PhoneNumberConfirmed? await _userManager.GenerateOtpCode(user) : await _userManager.GeneratePhoneNumberConfirmationToken(user,user.PhoneNumber);

        _logger.LogWarning($"Generated Code for user Id {user.Id} is {code}");

        //TODO Send Code Via Sms Provider

        return OperationResult<UserTokenRequestQueryResponse>.SuccessResult(new UserTokenRequestQueryResponse {UserKey = user.GeneratedCode});
    }
}