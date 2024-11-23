using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;
using PokemonGameAPI.Domain.Entities.Identity;
namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(TrainerConfiguration))]
public class Trainer:BaseEntity
{
    public int WinCount { get; set; } = 0;
    public int LooseCount { get; set; } = 0;
    public int TrainerLevel { get; set; } = 0;
    public bool ReadyForGymBattle { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public List<Game> Trainer1Games { get; set; }
    public List<Game> Trainer2Games { get; set; }
    public List<PokemonTrainer> TrainerPokemons { get; set; }
    public List<QuestTrainer> QuestTrainers { get; set; }
    public List<BadgeTrainer> BadgeTrainers { get; set; }
    public List<BattleResult> WinnerResults { get; set; }
    public List<BattleResult> LooserResults { get; set; }
}