using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PokemonGameAPI.Application.Abstraction.Services.Category;
using PokemonGameAPI.Application.Abstraction.Services.Game;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.Services.Skill;
using PokemonGameAPI.Application.Abstraction.Services.Trainer;
using PokemonGameAPI.Infrastructure.Services.Category;
using PokemonGameAPI.Infrastructure.Services.Game;
using PokemonGameAPI.Infrastructure.Services.Pokemon;
using PokemonGameAPI.Infrastructure.Services.Skill;
using PokemonGameAPI.Infrastructure.Services.Trainer;
using PokemonGameAPI.Infrastructure.Validators;

namespace PokemonGameAPI.Infrastructure.Extensions;

public static class ServiceRegister
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPokemonService, PokemonService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(SkillValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(CategoryValidator)));
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("az");
    }
}