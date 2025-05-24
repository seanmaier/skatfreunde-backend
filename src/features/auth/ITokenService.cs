using System.Security.Claims;
using skat_back.features.auth.models;
using skat_back.Lib;

namespace skat_back.features.auth;

public interface ITokenService
{
    string GenerateJwtToken(string username, List<Claim> claims, int expireMinutes);
    RefreshToken GenerateRefreshToken(DateTime expiration, string userId);
    void RevokeToken(RefreshToken token);

    (string accessToken, RefreshToken refreshToken) RotateTokens(ApplicationUser user, RefreshToken oldRefreshToken,
        List<Claim> claims);
}