﻿using AutoMapper;
using PokemonGameAPI.Application.Abstraction.Services.Category;

namespace PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;

public class EvolutionPokemonMappingProfile:Profile
{
    public EvolutionPokemonMappingProfile()
    {
        CreateMap<EvolutionPokemonRequestDto, EvolutionPokemonResponseDto>();
        CreateMap<EvolutionPokemonRequestDto, Domain.Entities.EvolutionPokemon>();
        CreateMap<Domain.Entities.EvolutionPokemon, EvolutionPokemonResponseDto>();
    }
}