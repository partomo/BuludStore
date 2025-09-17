using Bazta.Identity.Domain.Entities;
using Bulud.Base;
using Bulud.Base.Queries;
using MediatR;

namespace Bazta.Identity.Application.Queries.Roles;

public class GetRolesRequestQuery : RequestQuery, IRequest<ListResult<AppRole>>
{
    
}