namespace skat_back.features.url;

public interface IUrlService
{
    string GenerateConfirmationUrl(string email, string token);
    string GenerateResetPasswordUrl(string email, string token);
}