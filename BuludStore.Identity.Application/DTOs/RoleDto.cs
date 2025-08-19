namespace Bazta.Identity.Application.DTOs;

public class RoleDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string DisplayName { get; set; }

    public List<PermissionDto> Permissions { get; set; } = [];
}