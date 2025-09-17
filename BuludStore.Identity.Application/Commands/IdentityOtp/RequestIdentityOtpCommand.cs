using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Bazta.Identity.Application.Commands.IdentityOtp;

public class RequestIdentityOtpCommand : IRequest<string?>
{
    [Required]
    public required string PhoneNumber  { get; set; }
}