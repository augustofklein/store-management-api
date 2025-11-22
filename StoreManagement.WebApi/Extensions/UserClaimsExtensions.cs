using System.Security.Claims;

namespace StoreManagement.WebApi.Extensions
{
    public static class UserClaimsExtensions
    {
        public static int GetCompanyId(this ClaimsPrincipal user)
        {
            var companyIdStr = user.FindFirst("companyId")?.Value;
            return int.Parse(companyIdStr);
        }
    }
}