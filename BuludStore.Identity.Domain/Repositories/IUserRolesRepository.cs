using Bazta.Identity.Domain.Entities;
using Bulud.Base.Infrastructure;

namespace Bazta.Identity.Domain.Repositories;

public interface IUserRolesRepository : IRepository<AppUserRole>
{
    Task<List<AppRole>> Get(string userId);
}