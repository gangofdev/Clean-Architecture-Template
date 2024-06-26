﻿using CleanArc.Application.Features.Connect.Queries.GetUserInfo;
using CleanArc.Domain.Contracts.Identity;
using CleanArc.SharedKernel.Context;
using Mediator;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Connect.Queries.TokenRequest;

public class GetUserInfoRequestQueryHandler(IRequestContext requestContext, IAppUserManager userManager, IMediator mediator, ILogger<UserTokenRequestQueryHandler> logger) : IRequestHandler<GetUserInfoRequestQuery,OperationResult<GetUserInfoRequestQueryResponse>>
{
    private readonly IAppUserManager _userManager = userManager;
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UserTokenRequestQueryHandler> _logger = logger;
    private readonly IRequestContext _requestContext = requestContext;

    public async ValueTask<OperationResult<GetUserInfoRequestQueryResponse>> Handle(GetUserInfoRequestQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByUserName(_requestContext.UserName);

        if(user is null)
            return OperationResult<GetUserInfoRequestQueryResponse>.NotFoundResult("User Not found");

        return OperationResult<GetUserInfoRequestQueryResponse>.SuccessResult(new GetUserInfoRequestQueryResponse {UserKey = user.GeneratedCode, UserId=user.Id, DisplayName=user.Name});
    }
}