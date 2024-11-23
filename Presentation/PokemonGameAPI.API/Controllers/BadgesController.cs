using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Badge;

namespace PokemonGameAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BadgesController(IBadgeService _badgeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _badgeService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _badgeService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromForm]BadgeRequestDto requestDto)
    {
        var response = await _badgeService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Guid id,BadgeRequestDto requestDto)
    {
        var response = await _badgeService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _badgeService.RemoveAsync(id);
        return Ok(response);
    }
}