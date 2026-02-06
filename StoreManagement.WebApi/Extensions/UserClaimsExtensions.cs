using StoreManagement.Common.Constants;
using System.Security.Claims;

namespace StoreManagement.WebApi.Extensions
{
    public static class UserClaimsExtensions
    {
        public static int GetCompanyId(this ClaimsPrincipal user)
        {
            // TODO: Add error handling
            var companyIdStr = user.FindFirst(HttpHeadersConstants.CompanyId)?.Value;
            return int.Parse(companyIdStr);
        }
    }
}