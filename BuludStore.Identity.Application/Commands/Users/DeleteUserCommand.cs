using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BuludStore.Identity.Application.Commands.Users;

public class DeleteUserCommand : IRequest
{
    [Required]
    public required string Id { get; set; }
}