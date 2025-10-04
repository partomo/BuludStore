using System.Security.Authentication;
using AutoMapper;
using Bazta.Identity.Domain.Entities;
using Bulud.Base.Exceptions;
using Bulud.Base.Services;
using BuludStore.Identity.Application.Commands.Users;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application.Handlers.Users;

public class ResetPasswordCommandHandler(IOtpService otpService, UserManager<AppUser> userManager, IMapper mapper)
    : IRequestHandler<ResetPasswordCommand, UserDto>
{
    public async Task<UserDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var otpVerified = await otpService.VerifyAsync(request.PhoneNumber, request.Otp);
        if (!otpVerified)
            throw new InvalidCredentialException();
        var user = await userManager.FindByNameAsync(request.PhoneNumber);
        if (user is null)
            throw new NotFoundException("کاربری با این نام کاربری در سامانه یافت نشد.");
        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetPassword = await userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);
        if (!resetPassword.Succeeded)
        {
            var errors = string.Join(", ", resetPassword.Errors.Select(e => e.Description));
            throw new Exception($"خطا در تغییر رمز: {errors}");
        }
        
        return mapper.Map<UserDto>(user);
    }
}