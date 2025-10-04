using Bulud.Base;
using Bulud.Base.Infrastructure;
using BuludStore.Identity.Application.Queries.Roles;
using BuludStore.Identity.Domain.Entities;
using MediatR;

namespace BuludStore.Identity.Application.Handlers.Roles;

public class GetRolesListQueryHandler(IRepository<AppRole> repository) : IRequestHandler<GetRolesRequestQuery, ListResult<AppRole>>
{
    public async Task<ListResult<AppRole>> Handle(GetRolesRequestQuery request, CancellationToken cancellationToken)
    {
        return await repository.Get(request);
    }
}