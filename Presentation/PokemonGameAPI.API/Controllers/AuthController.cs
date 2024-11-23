using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Application.Abstraction.Services.Auth;

namespace PokemonGameAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _authService) : ControllerBase
{
   [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequestDto requestDto)
    {
        var response = await _authService.SignInAsync(requestDto);
        return Ok(response);
    }
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequestDto requestDto)
    {
        var response = await _authService.SignUpAsync(requestDto);
        return Ok(response);
    }
    [HttpGet("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        var response = await _authService.ConfirmEmailAsync(email, token);
        return Ok(response);
    }
}