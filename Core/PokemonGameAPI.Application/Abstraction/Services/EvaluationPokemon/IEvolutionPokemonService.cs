namespace PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;

public interface IEvolutionPokemonService
{
    Task<EvolutionPokemonResponseDto> CreateAsync(EvolutionPokemonRequestDto dto);
    Task<EvolutionPokemonResponseDto> UpdateAsync(Guid id,EvolutionPokemonRequestDto dto);
    Task<EvolutionPokemonResponseDto> RemoveAsync(Guid id);
    Task<EvolutionPokemonResponseDto> GetAsync(Guid id);
    Task<IList<EvolutionPokemonResponseDto>> GetAllAsync();
}