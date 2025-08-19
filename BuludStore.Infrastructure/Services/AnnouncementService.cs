using Bulud.Base.Services;
using BuludStore.Application.Services;

namespace BuludStore.Infrastructure.Services;

public class AnnouncementService(IEmailService emailService, ISmsService smsService) : IAnnouncementService
{
    public async Task SendAsync(string recipient, string body, string? subject = null)
    {
        if (IsEmail(recipient))
        {
            subject ??= "EmailNotification";
            await emailService.SendAsync(recipient, subject, body);
        }
        else if (IsPhoneNumber(recipient))
        {
            await smsService.SendAsync(recipient, body);
        }
        else
        {
            throw new ArgumentException("Recipient must be a valid email or phone number.");
        }
    }

    public async Task SendAsync(string? recipient, string?[] tokens, string template)
    {
        await smsService.SendAsync(recipient, tokens);
    }

    private static bool IsEmail(string recipient) => recipient.Contains('@');
    private static bool IsPhoneNumber(string recipient) => recipient.All(char.IsDigit) && recipient.Length >= 10;
}