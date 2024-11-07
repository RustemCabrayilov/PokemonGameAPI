using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PokemonGameAPI.Application.Extensions;

public static class AddPersistenceServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}