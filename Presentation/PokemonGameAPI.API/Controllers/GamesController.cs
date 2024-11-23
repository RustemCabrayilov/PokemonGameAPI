using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Repository;
using PokemonGameAPI.Application.Abstraction.Services.Game;
using PokemonGameAPI.Application.Abstraction.UnitOfWork;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GamesController(IGameService _gameService,
    IGenericRepository<Arena> _arenarepo,
    IUnitOfWork _unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _gameService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _gameService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(GameRequestDto requestDto)
    {
        var response = await _gameService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Post(Guid id,GameRequestDto requestDto)
    {
        var response = await _gameService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _gameService.RemoveAsync(id);
        return Ok(response);
    }

    [HttpGet("startBattle/{trainer1Id}/{trainer2Id}")]
    public async Task<IActionResult> StartBattle(Guid trainer1Id, Guid trainer2Id)
    {
        var response = await _gameService.StartBattleAsync(trainer1Id,trainer2Id);
        return Ok(response);
    }
    [HttpPost("fight")]
    public async Task<IActionResult> Fight(BattleResultRequestDto requestDto)
    {
        var response = await _gameService.FightAsync(requestDto);
        return Ok(response);
    }
    [HttpPost("reset-game")]
    public async Task<IActionResult> ResetGym(ResetGameDto requestDto)
    {
        var response = await _gameService.ResetAsync(requestDto);
        return Ok(response);
    }
}