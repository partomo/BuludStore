using System.Linq.Expressions;
using Bazta.Identity.Domain.Entities;
using Bulud.Base.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BuludStore.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : AppDbContextBase<AppUser, AppRole, AppUserRole>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            
            var deletedAtProp = clrType.GetProperty("DeletedAt", typeof(DateTime?));
            if (deletedAtProp != null)
            {
                var parameter = Expression.Parameter(clrType, "e");
                var property = Expression.Property(parameter, "DeletedAt");
                var nullConstant = Expression.Constant(null, typeof(DateTime?));
                var condition = Expression.Equal(property, nullConstant);
                var lambda = Expression.Lambda(condition, parameter);
        
                builder.Entity(clrType).HasQueryFilter(lambda);
            }
        }
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added 
                        || e.State == EntityState.Modified 
                        || e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            var entity = entry.Entity;
            var entityType = entity.GetType();
            
            if (entry.State == EntityState.Added)
            {
                var createdAtProp = entityType.GetProperty("CreatedAt");
                if (createdAtProp != null && createdAtProp.CanWrite)
                {
                    createdAtProp.SetValue(entity, DateTime.UtcNow);
                }
            }
            
            if (entry.State == EntityState.Modified)
            {
                var updatedAtProp = entityType.GetProperty("UpdatedAt");
                if (updatedAtProp != null && updatedAtProp.CanWrite)
                {
                    updatedAtProp.SetValue(entity, DateTime.UtcNow);
                }
            }
            
            if (entry.State == EntityState.Deleted)
            {
                var deletedAtProp = entityType.GetProperty("DeletedAt");
                if (deletedAtProp != null && deletedAtProp.CanWrite)
                {
                    deletedAtProp.SetValue(entity, DateTime.UtcNow);
                    entry.State = EntityState.Modified;
                }
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}