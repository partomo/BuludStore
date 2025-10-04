using AutoMapper;
using Bazta.Identity.Domain.Entities;
using Bulud.Base;
using Bulud.Base.Infrastructure;
using BuludStore.Identity.Application.DTOs;
using BuludStore.Identity.Application.Queries.Users;
using BuludStore.Identity.Domain.Entities;
using MediatR;

namespace Bazta.Identity.Application.Handlers.Users;

public class GetUsersListQueryHandler(IRepository<AppUser> repository, IMapper mapper)
    : IRequestHandler<GetUsersRequestQuery, ListResult<UserDto>>
{
    public async Task<ListResult<UserDto>> Handle(GetUsersRequestQuery request, CancellationToken cancellationToken)
    {
        var users = await repository.Get(request);

        var userDtos = mapper.Map<List<UserDto>>(users.Elements);

        return new ListResult<UserDto>
        {
            Elements = userDtos,
            Count = users.Count
        };
    }
}