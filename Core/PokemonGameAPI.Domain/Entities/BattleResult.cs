using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Domain.Configurations;

namespace PokemonGameAPI.Domain.Entities;
[EntityTypeConfiguration(typeof(BattleResultConfiguration))]
public class BattleResult:BaseEntity
{
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid WinnerId { get; set; }
    public Trainer Winner { get; set; }
    public Guid LooserId { get; set; }
    public Trainer Looser { get; set; }
    public int WinnerDamages { get; set; }
    public int LooserDamages { get; set; }
}