using Bazta.Identity.Application.DTOs;
using Bulud.Base;
using Bulud.Base.Queries;
using MediatR;

namespace Bazta.Identity.Application.Queries.Users;

public class GetUsersRequestQuery : RequestQuery, IRequest<ListResult<UserDto>>
{
    
}