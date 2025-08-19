using System.Linq.Expressions;
using Bulud.Base;
using Bulud.Base.Exceptions;
using Bulud.Base.Infrastructure;
using Bulud.Base.Queries;
using BuludStore.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuludStore.Infrastructure.Persistence;

public class BaseRepository<TEntity>(AppDbContext context, ICurrentUserContext currentUser) : IRepository<TEntity>
    where TEntity : class
{
    private readonly DbContext _context = context;
    private DbSet<TEntity> DbSet => context.Set<TEntity>();
    protected IQueryable<TEntity> Entities => GetQuery();


    public async Task<TEntity> Find(object id, RequestQuery? query = null)
    {
        var entityType = _context.Model.FindEntityType(typeof(TEntity));
        if (entityType == null)
            throw new InvalidOperationException($"Entity type {typeof(TEntity)} not found in DbContext.");

        var keyProperty = entityType.FindPrimaryKey()?.Properties.FirstOrDefault();
        if (keyProperty == null)
            throw new InvalidOperationException($"Primary key not defined for {typeof(TEntity)}.");

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var propertyAccess = Expression.Property(parameter, keyProperty.Name);
        var equals = Expression.Equal(
            propertyAccess,
            Expression.Convert(Expression.Constant(id), propertyAccess.Type)
        );
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

        var q = Entities;
        if (query != null)
            q = q.ApplyQuery(query);
        var entity = await q.FirstOrDefaultAsync(lambda);
        
        if (entity is null)
            throw new NotFoundException();
        return entity;
    }

    public async Task<ListResult<TEntity>> Get(RequestQuery query)
    {
        var q = Entities.ApplyQuery(query);
        return new ListResult<TEntity>
        {
            Elements = query.CountOnly ? null : await q.ApplyPaging(query.Page, query.Length).ToListAsync(),
            Count = await q.CountAsync()
        };
    }

    public async Task AddAsync(TEntity entity)
    {

        await DbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await DbSet.FindAsync(id) != null;
    }
    
    private IQueryable<TEntity> GetQuery()
    {
        throw new NotImplementedException();
    }
}