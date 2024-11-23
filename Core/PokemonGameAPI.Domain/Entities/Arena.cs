using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGameAPI.Domain.Entities;

public class Arena:BaseEntity
{
    public string Name { get; set; }
    public Guid  GameId  { get; set;}
    public Game Game { get; set; }
}