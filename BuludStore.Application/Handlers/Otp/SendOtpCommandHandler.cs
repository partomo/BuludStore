using Bulud.Base.Services;
using Bulud.communication.sms.kavenegar;
using BuludStore.Application.Commands.Otp;
using BuludStore.Application.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace BuludStore.Application.Handlers.Otp;

public class SendOtpCommandHandler(IOtpService otpService, IAnnouncementService announcementService, IOptions<SmsSettings> settings) : IRequestHandler<SendOtpCommand, string?>
{
    private readonly SmsSettings _settings = settings.Value;
    public async Task<string?> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var otp = await otpService.GenerateAsync(request.Recipient);
        var tokens = new string?[] { otp };
        if (_settings.IsActive)
        {
            await announcementService.SendAsync(request.Recipient, tokens, _settings.OtpTemplate);
            return null;
        }
        else
        {
            return otp;
        }
    }
}