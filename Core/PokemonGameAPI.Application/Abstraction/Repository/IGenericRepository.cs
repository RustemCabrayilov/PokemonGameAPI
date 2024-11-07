using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Abstraction.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    ValueTask<bool> AddAsync(T entity);
    bool Update(T entity);
    bool Remove(T entity);
    Task<T> GetAsync(Guid id,params string[] includes);
    IQueryable<T> GetAll();
}