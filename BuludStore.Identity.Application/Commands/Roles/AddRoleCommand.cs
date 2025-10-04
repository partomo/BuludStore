using System.ComponentModel.DataAnnotations;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Domain.Entities;
using MediatR;

namespace BuludStore.Identity.Application.Commands.Roles;

public class AddRoleCommand : IRequest<AppRole>
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string DisplayName { get; set; }
    public List<PermissionDto> Permissions { get; set; } = [];
}