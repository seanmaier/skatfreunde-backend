namespace skat_back.features.url;

public class UrlService(IHttpContextAccessor httpContextAccessor) : IUrlService
{
    public string GenerateConfirmationUrl(string userId, string token)
    {
        var request = httpContextAccessor.HttpContext?.Request;

        if (request == null)
            throw new InvalidOperationException("HttpContext is not available.");

        var baseUrl = $"{request.Scheme}://{request.Host}";
        var url =
            $"{baseUrl}/api/auth/confirm-email?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";

        return url;
    }

    public string GenerateResetPasswordUrl(string email, string token)
    {
        throw new NotImplementedException();
    }
}