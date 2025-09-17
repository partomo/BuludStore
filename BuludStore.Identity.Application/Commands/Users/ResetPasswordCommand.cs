using System.ComponentModel.DataAnnotations;
using Bazta.Identity.Application.DTOs;
using MediatR;

namespace Bazta.Identity.Application.Commands.Users;

public class ResetPasswordCommand : IRequest<UserDto>
{
    [Required]
    public required string PhoneNumber { get; set; }
    [Required]
    public required string Otp { get; set; }
    [Required]
    public required string NewPassword { get; set; }
}