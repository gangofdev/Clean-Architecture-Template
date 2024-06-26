﻿using CleanArc.Domain.Contracts;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.Domain.Models.Jwt;
using Microsoft.AspNetCore.Identity;

namespace CleanArc.Application.Features.Users.Queries.GenerateUserToken;

internal class GenerateUserTokenQueryHandler : IRequestHandler<GenerateUserTokenQuery, OperationResult<AccessTokenResponse>>
{
    private readonly IJwtService _jwtService;
    private readonly IAppUserManager _userManager;


    public GenerateUserTokenQueryHandler(IJwtService jwtService, IAppUserManager userManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
    }

    public async ValueTask<OperationResult<AccessTokenResponse>> Handle(GenerateUserTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserByCode(request.UserKey);

        if (user is null)
            return OperationResult<AccessTokenResponse>.FailureResult("User Not found");

        var result = new IdentityResult();
        if (user.PhoneNumberConfirmed)
        {
            result = await _userManager.VerifyUserCode(user, request.Code);
        }
        else
        {
            result = await _userManager.ChangePhoneNumber(user, user.PhoneNumber, request.Code);
        }

        if (!result.Succeeded)
            return OperationResult<AccessTokenResponse>.FailureResult(result.Errors.StringifyIdentityResultErrors());

        await _userManager.UpdateUserAsync(user);

        var token = await _jwtService.GenerateAsync(user);

        return OperationResult<AccessTokenResponse>.SuccessResult(token);
    }
}