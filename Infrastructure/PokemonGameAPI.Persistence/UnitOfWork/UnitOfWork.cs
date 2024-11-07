using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Persistence.Context;

namespace PokemonGameAPI.Persistence.UnitOfWork;

public class UnitOfWork(AppDbContext _dbContext):IUnitOfWork
{
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}