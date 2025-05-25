using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using skat_back.features.auth.models;
using static skat_back.utilities.constants.GeneralConstants;

namespace skat_back.features.email;

public class EmailService(
    IOptions<EmailSettings> settings,
    IWebHostEnvironment env
    ) : IEmailService
{
    private readonly EmailSettings _settings = settings.Value;

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.SmtpServer, 587, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.Username, _settings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public async Task SendResetPasswordAsync(string email, string resetUrl)
    {
        await SendEmailAsync(email, "Reset your password",
            $"Click here to reset your password: <a href='{resetUrl}'>Reset Password</a>");
    }

    public async Task SendConfirmationEmailAsync(string email, string confirmUrl)
    {
        var body = GetConfirmationEmailHtml(confirmUrl);
        
        await SendEmailAsync(email, "Confirm your email", body);
    }
    
    public async Task SendAdminConfirmationEmailAsync(string username)
    {
        await SendEmailAsync(Administrator, "User awaiting approval", $"User {username} has registered to Skatfreunde dashboard and confirmed their mail. Please review and approve");
    }
    
    private string GetConfirmationEmailHtml(string confirmUrl)
    {
        var path = Path.Combine(env.ContentRootPath, "wwwroot", "email-templates", "ConfirmEmailTemplate.html");
        var template = File.ReadAllText(path);
        return template.Replace("{{CONFIRM_URL}}", confirmUrl);
    } // TODO refactor
}