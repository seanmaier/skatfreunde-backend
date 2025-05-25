using System.Net;

namespace skat_back.features.url;

public class UrlService(IHttpContextAccessor httpContextAccessor) : IUrlService
{
    public string GenerateConfirmationUrl(string userId, string token)
    {
        var encodedToken = WebUtility.UrlEncode(token);
        return $"{GetFrontendUrl()}/confirm-email?userId={userId}&token={encodedToken}";
    }

    
    public string GenerateResetPasswordUrl(string userId, string token)
    {
        var encodedToken = WebUtility.UrlEncode(token);
        return $"{GetFrontendUrl()}/reset-password?userId={userId}&token={encodedToken}";
    }

    
    private string GetFrontendUrl()
    {
        var request = httpContextAccessor.HttpContext?.Request;

        if (request == null)
            throw new InvalidOperationException("HttpContext is not available.");

        return $"{request.Scheme}://{request.Host}";
    }
}