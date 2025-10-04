using System.ComponentModel.DataAnnotations;
using Bulud.Base.Queries;
using BuludStore.Identity.Application.DTOs;
using MediatR;

namespace BuludStore.Identity.Application.Queries.Users;

public class GetUserQuery : IRequest<UserDto>
{
    [Required]
    public required string UserId { get; set; }
    [Required]
    public required RequestQuery Query { get; set; }
}