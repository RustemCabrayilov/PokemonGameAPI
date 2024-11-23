using PokemonGameAPI.Application.Abstraction.Services.GymLeader;
using PokemonGameAPI.Application.Abstraction.Services.Trainer;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Abstraction.Services.Gym;

public record GymResponseDto(
    Guid Id,
    int GymLevel,
    string GymFieldName
    /*Guid GymFielId*/
    //GymField GymField cycle occurs
    /*GymLeaderResponseDto GymLeader*/
    );
