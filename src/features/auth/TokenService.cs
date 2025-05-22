using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using skat_back.features.auth.models;

namespace skat_back.features.auth;

public class TokenService(IConfiguration configuration, ILogger<TokenService> logger)
{
    public string GenerateToken(ApplicationUser user, List<Claim> claims)
    {
        logger.LogInformation("Generating JWT Token for user: {User}", user.UserName);
        var credentials = CreateCredentials();

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        logger.LogInformation("JWT Token created");

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private SigningCredentials CreateCredentials()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}