namespace BuludStore.Application.Interfaces;

public interface ICurrentUserContext
{
    public int? ProvinceId { get; }
    public int? CountyId { get; }
    bool IsCountyAdmin();
    bool IsProvinceAdmin();
    bool IsNationalAdmin(); 
}