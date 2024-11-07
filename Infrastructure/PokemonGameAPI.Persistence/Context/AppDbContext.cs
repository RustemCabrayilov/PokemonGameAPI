using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Entities.Identity;

namespace PokemonGameAPI.Persistence.Context;

public class AppDbContext: IdentityDbContext<AppUser,AppRole,Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Gym> Gyms { get; set; }
    public DbSet<GymLeader> GymLeaders { get; set; }
    public DbSet<GymLeaderPokemon> GymLeaderPokemons { get; set; }
}