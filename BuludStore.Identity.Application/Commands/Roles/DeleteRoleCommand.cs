using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Bazta.Identity.Application.Commands.Roles;

public class DeleteRoleCommand : IRequest
{
    [Required]
    public required string Id { get; set; }
}