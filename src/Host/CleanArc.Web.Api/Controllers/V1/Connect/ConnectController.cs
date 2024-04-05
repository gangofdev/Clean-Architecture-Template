﻿using Asp.Versioning;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Features.Users.Commands.RefreshUserTokenCommand;
using CleanArc.Application.Features.Users.Commands.RequestLogout;
using CleanArc.Application.Features.Users.Queries.GenerateUserToken;
using CleanArc.Application.Features.Users.Queries.TokenRequest;
using CleanArc.WebFramework.BaseController;
using CleanArc.WebFramework.Swagger;
using CleanArc.WebFramework.WebExtensions;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.UserManagement;

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
    public async Task<IActionResult> CreateUser(UserCreateCommand model)
    {
        var command = await _mediator.Send(model);

        return base.OperationResult(command);
    }


    [HttpPost("TokenRequest")]
    public async Task<IActionResult> TokenRequest(UserTokenRequestQuery model)
    {
        var query = await _mediator.Send(model);

        return base.OperationResult(query);
    }

    [HttpPost("LoginConfirmation")]
    public async Task<IActionResult> ValidateUser(GenerateUserTokenQuery model)
    {
        var result = await _mediator.Send(model);

        return base.OperationResult(result);
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

    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> RequestLogout()
    {
        var commandResult = await _mediator.Send(new RequestLogoutCommand(base.UserId));

        return base.OperationResult(commandResult);
    }
}