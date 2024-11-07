namespace PokemonGameAPI.Application.Abstraction.Services.Common;

public class BaseDto
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }=DateTime.Now;
    public DateTime UpdateDate { get; set; }=DateTime.Now;
}