using AutoMapper;
using Bazta.Identity.Application.DTOs;
using Bazta.Identity.Application.Queries.Users;
using Bazta.Identity.Domain.Entities;
using Bulud.Base.Infrastructure;
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