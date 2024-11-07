namespace PokemonGameAPI.Application.Abstraction.UnitOfWork;

public interface IUnitOfWork
{
     void SaveChanges();
     Task SaveChangesAsync();
}