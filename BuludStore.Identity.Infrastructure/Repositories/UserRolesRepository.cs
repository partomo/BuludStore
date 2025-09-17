using Bazta.Identity.Domain.Entities;
using Bazta.Identity.Domain.Repositories;
using BuludStore.Application.Interfaces;
using BuludStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bazta.Identity.Infrastructure.Repositories;

public class UserRolesRepository(AppDbContext context, ICurrentUserContext currentUser) : BaseRepository<AppUserRole>(context, currentUser), IUserRolesRepository
{
    public async Task<List<AppRole>> Get(string userId)
    {
        var q = Entities.Where(x => x.UserId == userId);
        return await q.Select(x => x.Role!).ToListAsync();
    }
}