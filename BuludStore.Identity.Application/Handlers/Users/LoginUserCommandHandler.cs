using System.Security.Authentication;
using System.Security.Claims;
using Bazta.Identity.Application.Commands.Users;
using Bazta.Identity.Application.DTOs;
using Bazta.Identity.Application.Interfaces;
using Bazta.Identity.Domain.Entities;
using Bazta.Identity.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application.Handlers.Users
{
    public class LoginUserCommandHandler(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IUserRolesRepository userRolesRepository,
        IJwtService jwtService,
        SignInManager<AppUser> signInManager)
        : IRequestHandler<LoginUserCommand, TokenDto>
    {
        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new InvalidCredentialException(request.UserName);
            var roles = await userRolesRepository.Get(user.Id);
            var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded)
                throw new InvalidCredentialException(request.UserName);

            var claims = new List<Claim>();
            int i = 0;
            foreach (var role in roles)
            {
                if (i == 0)
                    claims.Add(new Claim("roleDisplayName", role.DisplayName));
                
                claims.Add(new Claim(ClaimTypes.Role, role.Name!));
                var roleClaims = await roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
                i++;
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var token = jwtService.GenerateToken(user!, claims);
            return new TokenDto
            {
                AccessToken = token
            };
        }
    }
}