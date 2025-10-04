using AutoMapper;
using Bazta.Identity.Application.Commands.Users;
using Bazta.Identity.Domain.Entities;
using BuludStore.Application.Interfaces;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Application.Extensions;
using Bulud.Base.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application.Handlers.Users;

public class AddUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper, ICurrentUserContext currentUser)
    : IRequestHandler<AddUserCommand, UserDto>
{
    public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.HasRole("Admin", "Programmer"))
            throw new BadHttpRequestException("you cant create an user");
        
        var user = mapper.Map<AppUser>(request);
        user.UserName = request.PhoneNumber;
        var result = await userManager.CreateAsync(user, request.Password);
        {
            
        }
        if (result.Succeeded)
        {
            await userManager.AddToRolesAsync(user, new List<string> { request.Role });
            return mapper.Map<UserDto>(user);
        }
        
        throw new AppValidationException(result.ToErrorDictionary());
    }
}
