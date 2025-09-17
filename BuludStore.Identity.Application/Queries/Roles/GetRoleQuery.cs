using System.ComponentModel.DataAnnotations;
using Bazta.Identity.Application.DTOs;
using MediatR;

namespace Bazta.Identity.Application.Queries.Roles;

public class GetRoleQuery : IRequest<RoleDto>
{
    [Required]
    public required string Id { get; set; }
}