using Bulud.Base;
using Bulud.Base.Queries;
using BuludStore.Identity.Application.DTOs;
using MediatR;

namespace BuludStore.Identity.Application.Queries.Users;

public class GetUsersRequestQuery : RequestQuery, IRequest<ListResult<UserDto>>
{
    
}