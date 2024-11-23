using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Gym;

namespace PokemonGameAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GymsController(IGymService _gymService) : ControllerBase
{
   [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _gymService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _gymService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(GymRequestDto requestDto)
    {
        var response = await _gymService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Guid id,GymRequestDto requestDto)
    {
        var response = await _gymService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _gymService.RemoveAsync(id);
        return Ok(response);
    }
    [HttpGet("StartGymBattle/{mainTrainerId}/{gymId}")]
    public async Task<IActionResult> StartGymBattle(Guid mainTrainerId, Guid gymId)
    {
        var response = await _gymService.StartBattleAsync(mainTrainerId, gymId);
        return Ok(response);
    }
    [HttpPost("FightBoss")]
    public async Task<IActionResult> FightBoss(BossFightRequestDto requestDto)
    {
        var response = await _gymService.BossFight(requestDto);
        return Ok(response);
    }
    [HttpPost("reset-gym")]
    public async Task<IActionResult> ResetGym(ResetGymDto requestDto)
    {
        var response = await _gymService.ResetAsync(requestDto);
        return Ok(response);
    }
}