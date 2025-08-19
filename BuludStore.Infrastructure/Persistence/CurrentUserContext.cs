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