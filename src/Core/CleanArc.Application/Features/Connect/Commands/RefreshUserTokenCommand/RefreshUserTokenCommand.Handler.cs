using CleanArc.Domain.Contracts;
using CleanArc.Domain.Models.Jwt;

namespace CleanArc.Application.Features.Connect.Commands.RefreshUserTokenCommand
{
    internal class RefreshUserTokenCommandHandler(IJwtService jwtService) : IRequestHandler<RefreshUserTokenCommand,OperationResult<AccessTokenResponse>>
    {
        private readonly IJwtService _jwtService = jwtService;

        public async ValueTask<OperationResult<AccessTokenResponse>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
        {
            var newToken = await _jwtService.RefreshToken(request.RefreshToken);

            if(newToken is null)
                return OperationResult<AccessTokenResponse>.FailureResult("Invalid refresh token");

            return OperationResult<AccessTokenResponse>.SuccessResult(newToken);
        }
    }
}
