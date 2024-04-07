using System.Security.Claims;
using CleanArc.Domain.Models.Jwt;
using CleanArc.Domain.Entities.User;

namespace CleanArc.Domain.Contracts;

public interface IJwtService
{
    Task<AccessTokenResponse> GenerateAsync(User user);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    Task<AccessTokenResponse> GenerateByPhoneNumberAsync(string phoneNumber);
    Task<AccessTokenResponse> RefreshToken(Guid refreshTokenId);
}