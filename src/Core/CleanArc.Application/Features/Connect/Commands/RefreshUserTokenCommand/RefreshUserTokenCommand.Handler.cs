﻿using CleanArc.Domain.Contracts;
using CleanArc.Domain.Models.Jwt;

namespace CleanArc.Application.Features.Connect.Commands.RefreshUserTokenCommand
{
    internal class RefreshUserTokenCommandHandler : IRequestHandler<RefreshUserTokenCommand,OperationResult<AccessToken>>
    {
        private readonly IJwtService _jwtService;

        public RefreshUserTokenCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async ValueTask<OperationResult<AccessToken>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
        {
            var newToken = await _jwtService.RefreshToken(request.RefreshToken);

            if(newToken is null)
                return OperationResult<AccessToken>.FailureResult("Invalid refresh token");

            return OperationResult<AccessToken>.SuccessResult(newToken);
        }
    }
}
