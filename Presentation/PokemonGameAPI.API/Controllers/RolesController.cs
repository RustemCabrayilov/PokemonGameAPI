using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Role;

namespace PokemonGameAPI.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RolesController(IRoleService _roleService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response =await _roleService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(string id)
    {
        var response =await _roleService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(RoleRequestDto requestDto)
    {
        var response =await _roleService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(string id,RoleRequestDto requestDto)
    {
        var response =await _roleService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response =await _roleService.RemoveAsync(id);
        return Ok(response);
    }
}