﻿namespace PokemonGameAPI.Application.Abstraction.Services.Game;

public interface IGameService
{
    Task<GameResponseDto> CreateAsync(GameRequestDto dto);
    Task<GameResponseDto> UpdateAsync(Guid id,GameRequestDto dto);
    Task<GameResponseDto> RemoveAsync(Guid id);
    Task<GameResponseDto> GetAsync(Guid id);
    Task<IList<GameResponseDto>> GetAllAsync();
    Task<GameResponseDto> StartBattleAsync(Guid attackerId, Guid defenderId);
    Task<BattleResultResponseDto> FightAsync(BattleResultRequestDto requestDto);
    ValueTask<bool> ResetAsync(ResetGameDto requestDto);
}