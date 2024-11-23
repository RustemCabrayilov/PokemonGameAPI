using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Domain.Entities;

public class BossFightResult:BaseEntity
{
    public Guid GymId { get; set; }
    public Gym Gym { get; set; }
    public Guid WinnerId { get; set; }
    public ParticipantType Winner { get; set; }
    public Guid LooserId { get; set; }
    public ParticipantType Looser { get; set; }
    public int WinnerDamages { get; set; }
    public int LooserDamages { get; set; }
}