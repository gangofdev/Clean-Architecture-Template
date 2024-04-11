using AutoMapper;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.Domain.Entities.User;
using Mediator;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Connect.Commands.Create;

internal class SignupCommandHandler : IRequestHandler<SignupCommand, OperationResult<SignupCommandResult>>
{

    private readonly IAppUserManager _userManager;
    private readonly ILogger<SignupCommandHandler> _logger;
    private readonly IMapper _mapper;
    public SignupCommandHandler(IAppUserManager userRepository, ILogger<SignupCommandHandler> logger, IMapper mapper)
    {
        _userManager = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async ValueTask<OperationResult<SignupCommandResult>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var userObj=this._mapper.Map<User>(request);
        var userNameExist = await _userManager.IsExistUser(request.PhoneNumber);

        if (userNameExist)
            return OperationResult<SignupCommandResult>.FailureResult("Phone number already exists");

        var phoneNumberExist = await _userManager.IsExistUserName(request.UserName);

        if (phoneNumberExist)
            return OperationResult<SignupCommandResult>.FailureResult("Username already exists");

        //var user = new User { UserName = request.UserName, Name = request.FirstName, FamilyName = request.LastName, PhoneNumber = request.PhoneNumber };

        var user = _mapper.Map<User>(request);

        var createResult = await _userManager.CreateUserWithPasswordAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return OperationResult<SignupCommandResult>.FailureResult(string.Join(",", createResult.Errors.Select(c => c.Description)));
        }

        //var code = await _userManager.GeneratePhoneNumberConfirmationToken(user, user.PhoneNumber);


        //_logger.LogWarning($"Generated Code for User ID {user.Id} is {code}");

        //TODO Send Code Via Sms Provider

        return OperationResult<SignupCommandResult>.SuccessResult(new SignupCommandResult { Message = "User signup successful" });
    }
}