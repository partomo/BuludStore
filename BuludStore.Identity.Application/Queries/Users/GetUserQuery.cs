using System.ComponentModel.DataAnnotations;
using Bazta.Identity.Application.DTOs;
using Bulud.Base.Queries;
using MediatR;

namespace Bazta.Identity.Application.Queries.Users;

public class GetUserQuery : IRequest<UserDto>
{
    [Required]
    public required string UserId { get; set; }
    [Required]
    public required RequestQuery Query { get; set; }
}