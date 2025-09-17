using System.ComponentModel.DataAnnotations;
using Bazta.Identity.Application.DTOs;
using Bazta.Identity.Domain.Entities;
using MediatR;

namespace Bazta.Identity.Application.Commands.Roles;

public class EditRoleCommand : IRequest<AppRole>
{
    [Required]
    public required string Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string DisplayName { get; set; }
    
    public List<PermissionDto> Permissions { get; set; } = [];
    
}