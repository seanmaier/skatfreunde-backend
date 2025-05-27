using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.auth.models;
using skat_back.features.email;
using skat_back.features.email.models;
using skat_back.features.url;
using skat_back.utilities.exceptions;
using skat_back.utilities.mapping;
using static skat_back.utilities.constants.GeneralConstants;

namespace skat_back.features.auth;

public class AuthService(
    ILogger<AuthService> logger,
    UserManager<ApplicationUser> userManager,
    IEmailService emailService,
    IUrlService urlService,
    ITokenService tokenService,
    AppDbContext context) : IAuthService
{
    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        var user = dto.ToEntity();

        if (string.IsNullOrEmpty(user.Email))
            throw new EmailNotFoundException(dto.Email);

        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            logger.LogError("User registration failed: {Errors}", result.Errors);
            return result;
        }

        logger.LogInformation("User {User} registered successfully.", user.UserName);

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationUrl = urlService.GenerateConfirmationUrl(user.Email, token);
        await emailService.SendConfirmationEmailAsync(user.Email, confirmationUrl);

        return result;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        // 1. Check if the input is an email or username
        var user = IsValidEmail(dto.LoginInput)
            ? await userManager.FindByEmailAsync(dto.LoginInput)
            : await userManager.FindByNameAsync(dto.LoginInput);

        if (user is null)
        {
            logger.LogError("User not found: {Input}", dto.LoginInput);
            throw new UnauthorizedAccessException();
        }

        // 2. Check if the user is locked out
        if (await userManager.IsLockedOutAsync(user))
        {
            logger.LogWarning("User {UserId} account locked out.", user.Id);
            throw new HttpException(423, "User account lockd out");
        }

        // Reject if user credentials are invalid
        if (!await userManager.CheckPasswordAsync(user, dto.Password))
        {
            logger.LogError("Invalid login attempt for {Input}", dto.LoginInput);
            throw new UnauthorizedAccessException();
        }

        // Mark and revoke old refresh tokens
        await context.Entry(user).Collection(u => u.RefreshTokens).LoadAsync(); // Without this, RefreshTokens is zero

        foreach (var group in user.RefreshTokens.Where(t => t.IsActive).GroupBy(t => t.UserAgent))
        {
            var tokensToKeep = group.OrderByDescending(t => t.Created).Take(4).ToList();

            var tokensToDelete = group.Except(tokensToKeep).ToList();
            foreach (var token in tokensToDelete) tokenService.RevokeToken(token);
        }

        // Delete expired tokens
        user.RefreshTokens.RemoveAll(token => !token.IsActive);

        // 5. Generate tokens
        var userClaims = await GetUserClaims(user);

        var accessToken = tokenService.GenerateJwtToken(user.UserName ?? "unknown", userClaims, TokenExpirationTime);

        var remembered = dto.RememberMe ? 30 : 1;
        var refreshExpiration = DateTime.UtcNow.AddDays(remembered);
        var refreshToken = tokenService.GenerateRefreshToken(refreshExpiration, user.Id.ToString());

        // 6. Save refresh token to database
        context.RefreshTokens.Add(refreshToken);
        var affectedRows = await context.SaveChangesAsync();

        // 7. Check if the update was successful
        if (affectedRows == 0)
        {
            logger.LogError("Failed to update user with refresh token");
            throw new HttpException(500, "An internal error occurred");
        }

        logger.LogInformation("User {UserId} logged in successfully.", user.Id);
        return new LoginResponseDto(accessToken, refreshToken);
    }

    public async Task<IdentityResult> LogoutAsync(string userId, string refreshToken)
    {
        // 1. Find the user and the refresh token in the database
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
        {
            logger.LogWarning("User not found in database.");
            throw new UnauthorizedAccessException();
        }

        // 2. Revoke the old refresh token
        await context.Entry(user).Collection(u => u.RefreshTokens).LoadAsync(); // Without this, RefreshTokens is zero
        var token = user.RefreshTokens.FirstOrDefault(rft => rft.Token == refreshToken);
        if (token is null)
        {
            logger.LogWarning("Refresh token not found in database.");
            throw new UnauthorizedAccessException();
        }

        tokenService.RevokeToken(token);

        // 3. Update the token in the database
        var affectedRows = await context.SaveChangesAsync();
        if (affectedRows == 0)
        {
            logger.LogError("Failed to update user with refresh token");
            throw new HttpException(500, "An internal error occurred");
        }

        logger.LogInformation("User logged out.");
        return IdentityResult.Success;
    }

    public async Task<RefreshTokenResponseDto> RefreshTokenAsync(string requestRefreshToken)
    {
        // 1. Find the user with the refresh token
        var user = await userManager.Users
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == requestRefreshToken));

        if (user is null)
        {
            logger.LogWarning("No user found for refresh token");
            throw new UnauthorizedAccessException();
        }

        var oldRefreshToken = user.RefreshTokens.Single(rft => rft.Token == requestRefreshToken);

        // 2. Validate token is active
        if (!oldRefreshToken.IsActive)
        {
            logger.LogWarning("Expired or revoked refresh token used");
            throw new UnauthorizedAccessException();
        }

        // 3. Rotate tokens
        var userClaims = await GetUserClaims(user);
        var (newAccessToken, newRefreshToken) = tokenService.RotateTokens(user, oldRefreshToken, userClaims);
        context.RefreshTokens.Remove(oldRefreshToken);

        // 4. Save to database
        context.RefreshTokens.Add(newRefreshToken);
        var affectedRows = await context.SaveChangesAsync();

        if (affectedRows == 0)
        {
            logger.LogError("Failed to update user with refresh token");
            throw new HttpException(500, "An internal error occurred");
        }

        logger.LogInformation("Refresh token rotated for user {UserId}", user.Id);
        return new RefreshTokenResponseDto(newAccessToken, newRefreshToken.Token, oldRefreshToken.Expires);
    }

    public async Task SendForgotPasswordAsync(ForgotPasswordDto dto) // TODO Redirect with frontend link
    {
        var user = await userManager.FindByEmailAsync(dto.Email)
                   ?? throw new UserNotFoundException(dto.Email);

        if (string.IsNullOrEmpty(user.Email))
            throw new EmailNotFoundException(dto.Email);

        if (await userManager.IsEmailConfirmedAsync(user))
            throw new EmailNotConfirmedException(dto.Email);

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetUrl = urlService.GenerateResetPasswordUrl(dto.Email, token);

        await emailService.SendResetPasswordAsync(user.Email, resetUrl);
        logger.LogInformation("Password reset link sent to {Email}", user.Email);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email) ?? throw new UserNotFoundException(dto.Email);

        var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        if (result.Succeeded) return;

        throw new PasswordResetFailedException(result.Errors);
    }

    public async Task ConfirmEmailAsync(string userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            throw new UserNotFoundException(userId);

        if (string.IsNullOrEmpty(user.UserName))
            throw new UserNotFoundException(userId);

        var result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            throw new EmailNotConfirmedException(userId);

        await emailService.SendAdminConfirmationEmailAsync(user.UserName);
    }

    private static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) return false;
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role))).ToList();

        return claims;
    }
}