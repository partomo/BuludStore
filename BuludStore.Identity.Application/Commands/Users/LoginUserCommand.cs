using System.ComponentModel.DataAnnotations;
using BuludStore.Identity.Application.DTOs;
using MediatR;

namespace BuludStore.Identity.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<TokenDto>
    {
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        public required string Otp { get; set; }
    }
}
