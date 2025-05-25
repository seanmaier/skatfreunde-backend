namespace skat_back.features.email;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
    Task SendResetPasswordAsync(string email, string resetUrl);
    Task SendConfirmationEmailAsync(string username);
}