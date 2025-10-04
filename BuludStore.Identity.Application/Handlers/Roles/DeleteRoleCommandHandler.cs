using Bulud.Base.Exceptions;
using BuludStore.Identity.Application.Commands.Roles;
using BuludStore.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuludStore.Identity.Application.Handlers.Roles;

public class DeleteRoleCommandHandler(RoleManager<AppRole> roleManager) : IRequestHandler<DeleteRoleCommand>
{
    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id);
        if (role == null)
            throw new NotFoundException("نقش مورد نظر در سامانه یافت نشد.");
        await roleManager.DeleteAsync(role);
    }
}