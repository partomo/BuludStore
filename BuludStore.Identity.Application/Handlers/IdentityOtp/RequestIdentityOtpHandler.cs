using Bazta.Identity.Application.Commands.IdentityOtp;
using Bazta.Identity.Domain.Entities;
using BuludStore.Application.Commands.Otp;
using BuludStore.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BuludStore.Identity.Application.Handlers.IdentityOtp;

public class RequestIdentityOtpHandler(IMediator mediator, UserManager<AppUser> userManager) : IRequestHandler<RequestIdentityOtpCommand, string?>
{
    public async Task<string?> Handle(RequestIdentityOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.PhoneNumber);
        if (user is not null)
        {
            var otp = mediator.Send(new SendOtpCommand { Recipient = request.PhoneNumber }, cancellationToken);
        }

        throw new BadHttpRequestException("کاربری با این نام کاربری در سامانه یافت نشد.");
    }
}