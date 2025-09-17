using AutoMapper;
using Bazta.Identity.Application.DTOs;
using Bazta.Identity.Application.Extensions;
using Bazta.Identity.Application.Queries.Roles;
using Bazta.Identity.Domain.Entities;
using Bulud.Base.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application.Handlers.Roles;

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