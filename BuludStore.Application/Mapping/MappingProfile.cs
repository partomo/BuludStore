using AutoMapper;
using BuludStore.Application.Commands.Provinces;
using BuludStore.Domain.Entities;

namespace BuludStore.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddProvinceCommand, Province>();
        CreateMap<EditProvinceCommand, Province>();
    }
}