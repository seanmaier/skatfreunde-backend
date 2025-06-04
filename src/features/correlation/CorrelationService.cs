using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace skat_back.features.correlation;

public class CorrelationService : ICorrelationService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _salt;

    public CorrelationService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _salt = _configuration["Security:UserIdSalt"] ??
                throw new InvalidOperationException("UserIdSalt not configured");
    }

    public string TraceId => Activity.Current?.Id ??
                             _httpContextAccessor.HttpContext?.TraceIdentifier ??
                             "unknown";

    public string SafeUserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return GetSafeUserId(userId);
        }
    }

    private string GetSafeUserId(string userId)
    {
        if (string.IsNullOrEmpty(userId)) return "Anonymous";
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(userId + _salt));
        return Convert.ToHexString(hashedBytes)[..8]; // First 8 chars for brevity
    }
}