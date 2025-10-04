using Bulud.Base;
using Bulud.Base.Queries;
using BuludStore.Identity.Domain.Entities;
using MediatR;

namespace BuludStore.Identity.Application.Queries.Roles;

public class GetRolesRequestQuery : RequestQuery, IRequest<ListResult<AppRole>>
{
    
}