using System.Security.Authentication;
using System.Security.Claims;
using Bazta.Identity.Domain.Entities;
using Bulud.Base.Services;
using BuludStore.Identity.Application.Commands.Users;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Application.Interfaces;
using BuludStore.Identity.Domain.Entities;
using BuludStore.Identity.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuludStore.Identity.Application.Handlers.Users
{
    public class LoginUserCommandHandler(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IUserRolesRepository userRolesRepository,
        IJwtService jwtService,
        SignInManager<AppUser> signInManager,
        IOtpService otpService)
        : IRequestHandler<LoginUserCommand, TokenDto>
    {
        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var otpVerified = await otpService.VerifyAsync(request.PhoneNumber, request.Otp);
            if (!otpVerified)
                throw new InvalidCredentialException();

            var user = await userManager.FindByNameAsync(request.PhoneNumber);
            if (user is null)
            {
                user = new AppUser
                    { UserName = request.PhoneNumber, PhoneNumber = request.PhoneNumber, PhoneNumberConfirmed = true};
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "User");
                user = await userManager.FindByNameAsync(request.PhoneNumber);
            }

            var claims = new List<Claim>();
            var roles = await userManager.GetRolesAsync(user!);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = jwtService.GenerateToken(user!, claims);
            return new TokenDto
            {
                AccessToken = token
            };
        }
    }
}