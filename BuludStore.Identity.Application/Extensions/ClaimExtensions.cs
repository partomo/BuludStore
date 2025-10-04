using System.Security.Claims;
using BuludStore.Identity.Application.DTOs;

namespace BuludStore.Identity.Application.Extensions;

public static class ClaimExtensions
{
    public static PermissionDto? ToPermissionDto(this Claim claim)
    {
        if (claim.Type != "Permission" || string.IsNullOrWhiteSpace(claim.Value))
            return null;

        var parts = claim.Value.Split(':');
        if (parts.Length != 2)
            return null;

        var resource = parts[0];
        var actionsStr = parts[1];

        var actions = new Dictionary<string, bool>
        {
            ["C"] = actionsStr.Contains('C'),
            ["R"] = actionsStr.Contains('R'),
            ["U"] = actionsStr.Contains('U'),
            ["D"] = actionsStr.Contains('D')
        };

        return new PermissionDto
        {
            Resource = resource,
            Actions = actions
        };
    }
}