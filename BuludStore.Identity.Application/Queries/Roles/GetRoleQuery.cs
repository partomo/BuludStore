using System.ComponentModel.DataAnnotations;
using BuludStore.Identity.Application.DTOs;
using MediatR;

namespace BuludStore.Identity.Application.Queries.Roles;

public class GetRoleQuery : IRequest<RoleDto>
{
    [Required]
    public required string Id { get; set; }
}