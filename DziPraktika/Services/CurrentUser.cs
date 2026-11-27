using System.Security.Claims;

namespace DziPraktika.Services;

public static class CurrentUser
{
    public const string DevUserId = "dev-user";

    public static bool DevFallbackEnabled { get; set; }

    public static string Id(ClaimsPrincipal? user)
    {
        var id = user?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(id))
        {
            return id;
        }

        return DevFallbackEnabled ? DevUserId : string.Empty;
    }
}
