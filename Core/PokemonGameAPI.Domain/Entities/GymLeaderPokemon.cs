namespace PokemonGameAPI.Domain.Entities;

public class GymLeaderPokemon: BaseEntity
{
    public GymLeader GymLeader { get; set; }
    public Guid GymLeaderId { get; set; }
    public Pokemon Pokemon { get; set; }
    public Guid PokemonId { get; set; }
}