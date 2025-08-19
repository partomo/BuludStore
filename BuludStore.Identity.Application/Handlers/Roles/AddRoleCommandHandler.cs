using AutoMapper;
using Bazta.Identity.Application.Commands.Roles;
using Bazta.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application.Handlers.Roles;

public class AddRoleCommandHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    : IRequestHandler<AddRoleCommand, AppRole>
{
    public async Task<AppRole> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var role = mapper.Map<AppRole>(request);
        var result = await roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
            foreach (var permission in request.Permissions)
            {
                var claim = permission.ToClaim();
                if (claim != null)
                    await roleManager.AddClaimAsync(role, claim);
            }
            return role;
        }

        throw new Exception(result.Errors.First().Description);
    }
}
// 
// ClaimType => Permission , ClaimValue => User:CRUD