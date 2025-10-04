using Bulud.Base.Infrastructure;
using BuludStore.Identity.Domain.Entities;

namespace BuludStore.Identity.Domain.Repositories;

public interface IUserRolesRepository : IRepository<AppUserRole>
{
    Task<List<AppRole>> Get(string userId);
}