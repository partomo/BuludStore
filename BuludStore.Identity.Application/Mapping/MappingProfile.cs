using AutoMapper;
using Bazta.Identity.Application.Commands.Roles;
using Bazta.Identity.Application.Commands.Users;
using Bazta.Identity.Application.DTOs;
using Bazta.Identity.Domain.Entities;
using Bulud.Base;

namespace Bazta.Identity.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
                src.UserRoles!.Select(ur => ur.Role!.DisplayName).ToList()));
        CreateMap<AddUserCommand, AppUser>();
        CreateMap<EditUserCommand, AppUser>();
        CreateMap<AddRoleCommand, AppRole>();
        CreateMap<EditRoleCommand, AppRole>();
        CreateMap<AppRole, RoleDto>();
        CreateMap(typeof(ListResult<>), typeof(ListResult<>));
    }
}