using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Quest;

namespace PokemonGameAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestsController(IQuestService _questService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _questService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _questService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(QuestRequestDto requestDto)
    {
        var response = await _questService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Guid id,QuestRequestDto requestDto)
    {
        var response = await _questService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _questService.RemoveAsync(id);
        return Ok(response);
    }
}