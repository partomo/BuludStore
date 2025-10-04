using BuludStore.Application.Interfaces;
using BuludStore.Identity.Domain.Entities;
using BuludStore.Identity.Domain.Repositories;
using BuludStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BuludStore.Identity.Infrastructure.Repositories;

public class UserRolesRepository(AppDbContext context, ICurrentUserContext currentUser) : BaseRepository<AppUserRole>(context, currentUser), IUserRolesRepository
{
    public async Task<List<AppRole>> Get(string userId)
    {
        var q = Entities.Where(x => x.UserId == userId);
        return await q.Select(x => x.Role!).ToListAsync();
    }
}