namespace skat_back.features.url;

public interface IUrlService
{
    string GenerateConfirmationUrl(string userId, string token);
    string GenerateResetPasswordUrl(string userId, string token);
}