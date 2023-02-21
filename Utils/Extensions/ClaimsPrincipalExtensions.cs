using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return new(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
        }

        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        }

        public static List<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();
        }
    }
}
