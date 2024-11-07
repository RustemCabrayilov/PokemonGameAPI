namespace PokemonGameAPI.Infrastructure.Exceptions;

public class ValidationException:Exception
{
    public ValidationException()
    {
        
    }

    public ValidationException(string msg) : base(msg)
    {
        
    }
}