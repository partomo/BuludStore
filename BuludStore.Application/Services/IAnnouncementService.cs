namespace BuludStore.Application.Services;

public interface IAnnouncementService
{
    Task SendAsync(string recipient, string body, string? subject = null);
    Task SendAsync(string? recipient, string?[] tokens, string template);
}