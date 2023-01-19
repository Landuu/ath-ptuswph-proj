using Microsoft.EntityFrameworkCore;
using ptuswph_backend.Database;
using ptuswph_backend.Models;
using System.Security.Claims;
using System.Security.Principal;

namespace ptuswph_backend.Utils
{
    public static class IIdentityExtensions
    {
        public static string? GetClaimValue(this IIdentity identity, string key)
        {
            ClaimsIdentity? ci = (ClaimsIdentity?) identity;
            if (ci == null) return null;
            string? claimValue = ci.Claims.FirstOrDefault(x => x.Type == key)?.Value;
            return claimValue;
        }

        public static int? GetUid(this IIdentity identity) {
            string? uid = identity.GetClaimValue("uid");
            if (uid == null) return null;
            return Convert.ToInt32(uid);
        }

        public static User? GetUser(this IIdentity identity, ApiContext context) {
            if (identity == null) return null;
            int? userId = identity.GetUid();
            return context.Users.Find(userId);
        }
    }
}
