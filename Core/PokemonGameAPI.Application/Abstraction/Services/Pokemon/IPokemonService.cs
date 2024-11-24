namespace PokemonGameAPI.Application.Abstraction.Services.Pokemon;

public interface IPokemonService
{
    Task<PokemonResponseDto> CreateAsync(PokemonRequestDto dto);
    Task<PokemonResponseDto> UpdateAsync(Guid id, PokemonRequestDto dto);
    Task<PokemonResponseDto> RemoveAsync(Guid id);
    Task<PokemonResponseDto> GetAsync(Guid id);
    Task<IList<PokemonResponseDto>> GetAllAsync();
    Task<PokemonResponseDto> Attack(Guid opponentId,int damage);
    Task<PokemonResponseDto> Defense(Guid defenderId);
    Task<PokemonResponseDto> SetPokemonReady(Guid id); 
    Task<PokemonResponseDto> CheckEvaluationAsync(Guid id);
    Task<PokemonResponseDto> EvolutionUpdateAsync(Guid id);
}