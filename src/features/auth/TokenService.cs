using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using skat_back.features.auth.models;

namespace skat_back.features.auth;

public class TokenService(
    IConfiguration configuration,
    ILogger<TokenService> logger,
    IHttpContextAccessor httpContentAccessor) : ITokenService
{
    /// <summary>
    ///     Generates a JWT token for the specified user with the provided claims and expiration time.
    /// </summary>
    /// <param name="userName">The user for whom the token is being generated.</param>
    /// <param name="claims">A list of claims to include in the token.</param>
    /// <param name="expireMinutes">The expiration time of the token in minutes.</param>
    /// <returns>A JWT token as a string.</returns>
    public string GenerateJwtToken(string userName, List<Claim> claims, int expireMinutes)
    {
        logger.LogInformation("Generating JWT Token for user: {User}", userName);
        var credentials = CreateCredentials();

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials);

        logger.LogInformation("JWT Token created");

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public RefreshToken GenerateRefreshToken(DateTime expiration, string userId)
    {
        logger.LogInformation("Generating Refresh Token");
        var ip = httpContentAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var userAgent = httpContentAccessor.HttpContext?.Request.Headers.UserAgent.ToString() ?? "unknown";

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserAgent = userAgent,
            Created = DateTime.UtcNow,
            CreatedByIp = ip,
            Expires = expiration,
            Token = GenerateRandomString(),
            ApplicationUserId = Guid.Parse(userId)
        };
        return refreshToken;
    }


    public (string accessToken, RefreshToken refreshToken) RotateTokens(ApplicationUser user,
        RefreshToken oldRefreshToken, List<Claim> claims)
    {
        var newRefreshToken = GenerateRefreshToken(oldRefreshToken.Expires, user.Id.ToString());

        oldRefreshToken.ReplacedByToken = newRefreshToken.Token;
        RevokeToken(oldRefreshToken);

        var newAccessToken = GenerateJwtToken(user.UserName ?? "unknown", claims, 15);

        return (newAccessToken, newRefreshToken);
    }

    public void RevokeToken(RefreshToken token)
    {
        token.Revoked = DateTime.UtcNow;
        token.RevokedByIp = httpContentAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
    }


    private SigningCredentials CreateCredentials()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_SECRET_KEY"]!));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }

    private static string GenerateRandomString(int length = 64)
    {
        var randomNumber = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}