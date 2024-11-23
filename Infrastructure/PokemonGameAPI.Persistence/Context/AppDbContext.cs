using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Entities.Identity;

namespace PokemonGameAPI.Persistence.Context;

public class AppDbContext:IdentityDbContext<AppUser,AppRole,string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<PokemonTrainer> PokemonTrainers { get; set; }
    public DbSet<Gym> Gyms { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GymLeader> GymLeaders { get; set; }
    public DbSet<GymLeaderPokemon> GymLeaderPokemons { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<BadgeTrainer> BadgeTrainers { get; set; }
    public DbSet<QuestTrainer> QuestTrainer { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Arena> Arenas { get; set; }
    public DbSet<BattleResult> BattleResults { get; set; }
    public DbSet<BossFightResult> BossFightResults { get; set; }
    public DbSet<GymField> GymFields { get; set; }
}