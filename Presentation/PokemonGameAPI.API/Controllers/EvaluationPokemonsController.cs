using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Category;
using PokemonGameAPI.Application.Abstraction.Services.EvaluationPokemon;

namespace PokemonGameAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluationPokemonsController(IEvolutionPokemonService evolutionPokemonService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await evolutionPokemonService
            .GetAllAsync();
        return Ok(response);
    }

    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await evolutionPokemonService
            .GetAsync(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(EvolutionPokemonRequestDto requestDto)
    {
        var response = await evolutionPokemonService
            .CreateAsync(requestDto);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Put(Guid id, EvolutionPokemonRequestDto requestDto)
    {
        var response = await evolutionPokemonService.UpdateAsync(id, requestDto);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await evolutionPokemonService
            .RemoveAsync(id);
        return Ok(response);
    }
}