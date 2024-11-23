using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.User;

namespace PokemonGameAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService _userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response =await _userService.GetAllAsync();
        return Ok(response);
    }
    [HttpGet("id")]
    public async Task<IActionResult> Get(string id)
    {
        var response =await _userService.GetAsync(id);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(UserRequestDto requestDto)
    {
        var response =await _userService.CreateAsync(requestDto);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Put(string id,UserRequestDto requestDto)
    {
        var response =await _userService.UpdateAsync(id,requestDto);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var response =await _userService.RemoveAsync(id);
        return Ok(response);
    }
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(UserRoleDto requestDto)
    {
        var response =await _userService.AssignRoleAsync(requestDto);
        return Ok(response);
    }
}