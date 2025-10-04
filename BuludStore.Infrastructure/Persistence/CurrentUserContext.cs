using BuludStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BuludStore.Infrastructure.Persistence;

public class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    public int? ProvinceId
    {
        get
        {
            var provinceId = httpContextAccessor.HttpContext?.User.FindFirst("province_id")?.Value;
            if (provinceId == null)
                return null;

            return int.Parse(provinceId);
        }
    }

    public int? CountyId
    {
        get
        {
            var countyId = httpContextAccessor.HttpContext?.User.FindFirst("county_id")?.Value;
            if (countyId == null)
                return null;
            return int.Parse(countyId);
        }
    }


    public List<string>? RoleNames
    {
        get
        {
            var role = httpContextAccessor.HttpContext?.User.FindAll("role_name")?.Select(c => c.Value).ToList();
            return role;
        }
    }


    public bool HasRole(params string[] roles)
    {
        if (RoleNames == null) return false;

        return RoleNames.Any(r => roles.Any(role => r.Equals(role, StringComparison.OrdinalIgnoreCase)));
    }

    public bool IsCountyAdmin()
    {
        return CountyId is not null;
    }

    public bool IsProvinceAdmin()
    {
        return !IsCountyAdmin() && ProvinceId is not null;
    }

    public bool IsNationalAdmin()
    {
        return !IsCountyAdmin() && !IsProvinceAdmin();
    }
}