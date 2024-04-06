using System.Security.Claims;
using CleanArc.Domain.Models.Jwt;
using CleanArc.Domain.Entities.User;

namespace CleanArc.Domain.Contracts;

public interface IJwtService
{
    Task<AccessToken> GenerateAsync(User user);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    Task<AccessToken> GenerateByPhoneNumberAsync(string phoneNumber);
    Task<AccessToken> RefreshToken(Guid refreshTokenId);
}