using AutoMapper;
using Bazta.Identity.Domain.Entities;
using Bulud.Base.Infrastructure;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Application.Queries.Users;
using BuludStore.Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application.Handlers.Users;

public class GetUserQueryHandler(UserManager<AppUser> userManager,IRepository<AppUser> repository, IMapper mapper) : IRequestHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.Find(request.UserId, request.Query);
        
        var userDto = mapper.Map<UserDto>(user);
        
        userDto.Roles = await userManager.GetRolesAsync(user);
        
        return userDto;
    }
}