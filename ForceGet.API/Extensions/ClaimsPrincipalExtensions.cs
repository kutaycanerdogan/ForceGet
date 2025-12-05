using System.Security.Claims;

namespace ForceGet.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst("userId")?.Value;
            return int.TryParse(value, out var id) ? id : 0;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst("email")?.Value ?? string.Empty;
        }
    }
}