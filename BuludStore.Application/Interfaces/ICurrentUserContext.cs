namespace BuludStore.Application.Interfaces;

public interface ICurrentUserContext
{
    public int? ProvinceId { get; }
    public int? CountyId { get; }
    public List<string>? RoleNames { get; }
    public bool HasRole(params string[] roles);
    bool IsCountyAdmin();
    bool IsProvinceAdmin();
    bool IsNationalAdmin(); 
}