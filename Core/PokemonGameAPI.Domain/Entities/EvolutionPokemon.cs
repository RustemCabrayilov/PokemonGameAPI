namespace PokemonGameAPI.Domain.Entities;

public class EvolutionPokemon:BaseEntity
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int HP { get; set; }
    public int EvolutionLevel { get; set; }
    public Guid PokemonId { get; set; }
    public Pokemon Pokemon { get; set; }
}