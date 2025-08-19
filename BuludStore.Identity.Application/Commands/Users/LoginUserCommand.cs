using System.ComponentModel.DataAnnotations;
using Bazta.Identity.Application.DTOs;
using MediatR;

namespace Bazta.Identity.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<TokenDto>
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
