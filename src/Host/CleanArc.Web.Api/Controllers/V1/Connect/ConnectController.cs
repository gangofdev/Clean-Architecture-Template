using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Features.Connect.Commands.RefreshUserTokenCommand;
using CleanArc.Application.Features.Users.Queries.TokenRequest;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CleanArc.Application.Features.Connect.Commands.Create;
using CleanArc.Application.Features.Connect.Queries.GetToken;
using CleanArc.Application.Features.Connect.Commands.RequestLogout;
using CleanArc.Application.Features.Connect.Queries.GetUserInfo;

namespace CleanArc.Web.Api.Controllers.V1.Connect;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Connect")]
public class ConnectController : BaseController
{
    private readonly IMediator _mediator;

    public ConnectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> CreateUser(SignupCommand model)
    {
        var command = await _mediator.Send(model);

        return base.OperationResult(command);
    }


    [HttpPost("Token")]
    public async Task<IActionResult> TokenRequest(GetTokenQuery model)
    {
        var query = await _mediator.Send(model);

        return base.OperationResult(query);
    }


    [HttpPost("RefreshSignIn")]
    [RequireTokenWithoutAuthorization]
    public async Task<IActionResult> RefreshUserToken(RefreshUserTokenCommand model)
    {
        var checkCurrentAccessTokenValidity =await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

        if (checkCurrentAccessTokenValidity.Succeeded)
            return BadRequest("Current access token is valid. No need to refresh");

        var newTokenResult = await _mediator.Send(model);

        return base.OperationResult(newTokenResult);
    }

    [Authorize]
    [HttpGet("UserInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var query = await _mediator.Send(new GetUserInfoRequestQuery());

        return base.OperationResult(query);
    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> RequestLogout()
    {
        var commandResult = await _mediator.Send(new RequestLogoutCommand(base.UserId));

        return base.OperationResult(commandResult);
    }
}