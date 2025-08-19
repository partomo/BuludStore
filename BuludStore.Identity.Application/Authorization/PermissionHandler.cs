using Bazta.Identity.Domain.Entities;
using Bulud.Base.Authorization;
using Bulud.Base.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application.Authorization;

public class PermissionHandler(UserManager<AppUser> userManager): AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var contextUser = context.User;
        var userId = contextUser.GetUserId();
        var appUser = await userManager.FindByIdAsync(userId!);
        if (appUser == null)
        {
            context.Fail();
            return;
        }
        
        if (contextUser.IsInRole("Programmer"))
        {
            context.Succeed(requirement);
            return;
        }
        
        var permissions = 
            contextUser
                .Claims.FirstOrDefault(x=>x.Type=="Permission" && x.Value.Contains(requirement.Resource));
        if (permissions == null)
        {
            context.Fail();
            return;
        }
        
        var action = permissions.Value.Split(":")[1];
        if (action.Contains(requirement.Action) || action == "*")
        {
            context.Succeed(requirement);
            return;
        }
        
        context.Fail();
    }
}