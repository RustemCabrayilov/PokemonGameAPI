namespace PokemonGameAPI.Infrastructure.Services;

using AutoMapper;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Infrastructure.Services;

public class PokemonService(IGenericRepository<Pokemon> _pokemonRepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork):IPokemonService
{
    public async Task<PokemonResponseDto> AddAsync(PokemonRequestDto dto)
    {
        var entity= _mapper.Map<Pokemon>(dto); 
        await _pokemonRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PokemonResponseDto> Update(PokemonRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<PokemonResponseDto> Remove(PokemonRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<PokemonResponseDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public IList<PokemonResponseDto> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}