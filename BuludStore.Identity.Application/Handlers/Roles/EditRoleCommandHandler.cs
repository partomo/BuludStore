using AutoMapper;
using Bulud.Base.Exceptions;
using BuludStore.Identity.Application.Commands.Roles;
using BuludStore.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuludStore.Identity.Application.Handlers.Roles;

public class EditRoleCommandHandler(RoleManager<AppRole> roleManager, IMapper mapper) : IRequestHandler<EditRoleCommand, AppRole>
{
    public async Task<AppRole> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id);
        if (role == null)
            throw new NotFoundException("نقش مورد نظر در سامانه یافت نشد.");
        
        mapper.Map(request, role);
        await roleManager.UpdateAsync(role);
        
        var claims = await roleManager.GetClaimsAsync(role);
        
        foreach (var claim in claims)
            await roleManager.RemoveClaimAsync(role, claim);

        foreach (var permission in request.Permissions)
        {
            var claim = permission.ToClaim();
            if (claim != null)
                await roleManager.AddClaimAsync(role, claim);
        }
        return role;
    }
}