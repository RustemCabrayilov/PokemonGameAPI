using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonGameAPI.Application.Abstraction.Services.Auth;
using PokemonGameAPI.Application.Abstraction.Services.Badge;
using PokemonGameAPI.Application.Abstraction.Services.Category;
using PokemonGameAPI.Application.Abstraction.Services.Document;
using PokemonGameAPI.Application.Abstraction.Services.Email;
using PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;
using PokemonGameAPI.Application.Abstraction.Services.Game;
using PokemonGameAPI.Application.Abstraction.Services.Gym;
using PokemonGameAPI.Application.Abstraction.Services.GymLeader;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;
using PokemonGameAPI.Application.Abstraction.Services.Quest;
using PokemonGameAPI.Application.Abstraction.Services.Role;
using PokemonGameAPI.Application.Abstraction.Services.Skill;
using PokemonGameAPI.Application.Abstraction.Services.Trainer;
using PokemonGameAPI.Application.Abstraction.Services.User;
using PokemonGameAPI.Infrastructure.Services.Auth;
using PokemonGameAPI.Infrastructure.Services.Badge;
using PokemonGameAPI.Infrastructure.Services.Category;
using PokemonGameAPI.Infrastructure.Services.Document;
using PokemonGameAPI.Infrastructure.Services.Email;
using PokemonGameAPI.Infrastructure.Services.EvaluationPokemonService;
using PokemonGameAPI.Infrastructure.Services.Game;
using PokemonGameAPI.Infrastructure.Services.Gym;
using PokemonGameAPI.Infrastructure.Services.GymLeader;
using PokemonGameAPI.Infrastructure.Services.Pokemon;
using PokemonGameAPI.Infrastructure.Services.Quest;
using PokemonGameAPI.Infrastructure.Services.Role;
using PokemonGameAPI.Infrastructure.Services.Skill;
using PokemonGameAPI.Infrastructure.Services.Trainer;
using PokemonGameAPI.Infrastructure.Services.User;
using Serilog;

namespace PokemonGameAPI.Infrastructure.Extensions;

public static class ServiceRegister
{
    public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        /*services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );*/
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });
            /*.AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                opt.JsonSerializerOptions.WriteIndented = true;
            });*/

        services.AddScoped<IPokemonService, PokemonService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<IGymService, GymService>();
        services.AddScoped<IGymLeaderService, GymLeaderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBadgeService, BadgeService>();
        services.AddScoped<IQuestService, QuestService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEvolutionPokemonService, EvolutionPokemonService>();
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(SkillValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(CategoryValidator)));   
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(PokemonValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(SignInValidator)));  
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(SignUpValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(BadgeValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(GameValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(QuestValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(UserValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(RoleValidator)));
        services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(TrainerValidator)));
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("az");
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
    }
    public static void AddCustomSerilog(this ILoggingBuilder logBuilder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("serilog-config.json")
                .Build())
            .Enrich.FromLogContext()
            .CreateLogger();
        logBuilder.ClearProviders();
        logBuilder.AddSerilog(logger);
    }
}