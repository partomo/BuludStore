using AutoMapper;
using BuludStore.Identity.Application.Extensions;
using Bulud.Base.Exceptions;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Application.Queries.Roles;
using BuludStore.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuludStore.Identity.Application.Handlers.Roles;

public class GetRoleQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    : IRequestHandler<GetRoleQuery, RoleDto>
{
    public async Task<RoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id);
        if (role == null)
            throw new NotFoundException("نقش مورد نظر در سامانه یافت نشد.");

        var roleDto = mapper.Map<RoleDto>(role);

        var roleClaim = await roleManager.GetClaimsAsync(role);
        foreach (var claim in roleClaim)
        {
            var permissionDto = claim.ToPermissionDto();
            if (permissionDto != null)
                roleDto.Permissions.Add(permissionDto);
        }


        return roleDto;
    }
}