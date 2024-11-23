using Microsoft.AspNetCore.Identity;
using PokemonGameAPI.API.ExceptionHandlers;
using PokemonGameAPI.Application.Extensions;
using PokemonGameAPI.Domain.Entities.Identity;
using PokemonGameAPI.Infrastructure.Extensions;
using PokemonGameAPI.Persistence.Context;
using PokemonGameAPI.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Logging.AddCustomSerilog();
builder.Services.AddIdentity<AppUser, AppRole>(opts =>
{
   opts.Password.RequireDigit = true;
   opts.Password.RequireLowercase = false;
   opts.Password.RequireNonAlphanumeric = false;
   opts.Password.RequireUppercase = false;
   opts.Password.RequiredLength = 6;

}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(setup =>
{

    setup.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:5294", "https://localhost:5294")
            .SetIsOriginAllowed(orgin => true);

    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseExceptionHandler(_ =>{ });
app.UseCors();
app.UseExceptionHandler(_ => { });
app.Run();