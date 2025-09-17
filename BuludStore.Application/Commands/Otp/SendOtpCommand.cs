using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BuludStore.Application.Commands.Otp;

public class SendOtpCommand : IRequest<string>
{
    [Required]
    public required string Recipient { get; set; }
}