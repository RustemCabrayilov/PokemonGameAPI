using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Persistence.Context;

namespace PokemonGameAPI.Persistence.Repository;

public class GenericRepository<T>(AppDbContext _dbContext):IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet=_dbContext.Set<T>();
    public async ValueTask<bool> AddAsync(T entity)
    {
       var entityEntry = await _dbContext.AddAsync(entity);
       return entityEntry.State == EntityState.Added;
    }

    public bool Update(T entity)
    {
       var entityEntry = _dbSet.Update(entity);
       return entityEntry.State == EntityState.Modified;
    }

    public bool Remove(T entity)
    {
      var entityEntry =  _dbSet.Remove(entity);
      return entityEntry.State == EntityState.Deleted;
    }

    public async Task<T> GetAsync(Guid id,params string[] includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query.Include(include);
        }
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> GetAll()
    {
        var entities = _dbSet.AsQueryable().AsNoTracking();
        return entities;
    }
}