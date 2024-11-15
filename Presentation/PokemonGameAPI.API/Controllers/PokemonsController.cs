﻿using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Pokemon;

namespace PokemonGameAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PokemonsController(IPokemonService _pokemonService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response= await _pokemonService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response= await _pokemonService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(PokemonRequestDto requestDto)
    {
        var response= await _pokemonService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Guid id,PokemonRequestDto requestDto)
    {
        var response= await _pokemonService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Put(Guid id)
    {
        var response= await _pokemonService.RemoveAsync(id);
        return Ok(response);
    }
}