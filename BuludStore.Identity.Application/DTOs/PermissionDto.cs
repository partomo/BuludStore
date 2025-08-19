using System.Security.Claims;

namespace Bazta.Identity.Application.DTOs;

public class PermissionDto
{
    public required string Resource { get; set; }

    public Dictionary<string, bool> Actions { get; set; } = new()
    {
        ["C"] = false,
        ["R"] = false,
        ["U"] = false,
        ["D"] = false
    };

    public Claim? ToClaim()
    {
        if (string.IsNullOrWhiteSpace(Resource))
            return null;

        var validActions = new[] { "C", "R", "U", "D" };

        var actionStr = string.Concat(
            validActions.Where(a =>
                Actions.TryGetValue(a, out var selected) && selected)
        );

        if (string.IsNullOrWhiteSpace(actionStr))
            return null;

        return new Claim(
            type: "Permission",
            value: $"{Resource}:{actionStr}"
        );
    }
}