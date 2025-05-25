using Microsoft.AspNetCore.Identity;
using skat_back.features.auth.models;
using skat_back.features.email.models;

namespace skat_back.features.auth;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDto dto);
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
    Task<IdentityResult> LogoutAsync(string userId, string refreshToken);
    Task<RefreshTokenResponseDto> RefreshTokenAsync(string requestRefreshToken);
    Task SendForgotPasswordAsync(ForgotPasswordDto dto);
    Task ResetPasswordAsync(ResetPasswordDto dto);
    Task ConfirmEmailAsync(string userId, string token);
}