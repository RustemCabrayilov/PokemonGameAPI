using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.GymLeader;

namespace PokemonGameAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GymLeadersController(IGymLeaderService _gymLeaderService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _gymLeaderService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _gymLeaderService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(GymLeaderRequestDto requestDto)
    {
        var response = await _gymLeaderService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Guid id,GymLeaderRequestDto requestDto)
    {
        var response = await _gymLeaderService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _gymLeaderService.RemoveAsync(id);
        return Ok(response);
    }
    [HttpPost("AssignPokemon")]
    public async Task<IActionResult> AssignPokemon(GymLeaderPokemonDto requestDto)
    {
        var response = await _gymLeaderService.AsssignPokemonAsync(requestDto);
        return Ok(response);
    }
}