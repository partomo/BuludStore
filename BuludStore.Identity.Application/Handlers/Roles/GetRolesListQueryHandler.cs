using Bazta.Identity.Application.Queries.Roles;
using Bazta.Identity.Domain.Entities;
using Bulud.Base;
using Bulud.Base.Infrastructure;
using MediatR;

namespace Bazta.Identity.Application.Handlers.Roles;

public class GetRolesListQueryHandler(IRepository<AppRole> repository) : IRequestHandler<GetRolesRequestQuery, ListResult<AppRole>>
{
    public async Task<ListResult<AppRole>> Handle(GetRolesRequestQuery request, CancellationToken cancellationToken)
    {
        return await repository.Get(request);
    }
}