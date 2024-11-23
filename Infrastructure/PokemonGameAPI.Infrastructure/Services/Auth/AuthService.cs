using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PokemonGameAPI.Application.Abstraction.Services.Auth;
using PokemonGameAPI.Application.Abstraction.Services.Email;
using PokemonGameAPI.Application.Abstraction.Services.User;
using PokemonGameAPI.Domain.Entities.Identity;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.Infrastructure.Services.Auth;

public class AuthService(
    SignInManager<AppUser> _signInManager,
    UserManager<AppUser> _userManager,
    IEmailService _emailService,
    IMapper _mapper) : IAuthService
{
    public async Task<bool> SignInAsync(SignInRequestDto requestDto)
    {
        var user = await _userManager.FindByNameAsync(requestDto.UserName);
        if (user == null) throw new BadRequestException("Username or password is incorrect");
        var result =
            await _signInManager.PasswordSignInAsync(user, requestDto.Password, requestDto.IsPersistent, false);
        if (!result.Succeeded) throw new BadRequestException("Username or password is incorrect");
        return true;
    }

    public async Task<UserResponseDto> SignUpAsync(SignUpRequestDto requestDto)
    {
        var user = await _userManager.FindByNameAsync(requestDto.UserName);
        if (user is not null) throw new BadRequestException("This user already exits");
        var userToCreate = _mapper.Map<AppUser>(requestDto);
        userToCreate.LastIPAddress = "0.0.0.0";
        userToCreate.Id = Guid.NewGuid().ToString();
        var result = await _userManager.CreateAsync(userToCreate, requestDto.Password);
        if (!result.Succeeded) throw new BadRequestException($"User couldn't  be created:{result.Errors.FirstOrDefault().Description}");
        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userToCreate);
        var activationLink =
            $"http://localhost:5208/api/Auth/ConfirmEmail?email={userToCreate.Email}&token={emailConfirmationToken}";
        await _emailService.SenEmailAsync(userToCreate.Email, "Confirm email", activationLink);
        return _mapper.Map<UserResponseDto>(userToCreate);
    }

    public async Task<bool> ConfirmEmailAsync(string email, string token)
    {
        token = token.Replace(" ", "+");

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) throw new BadRequestException("Invalid email address");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded) throw new BadRequestException("Email couldn't be confirmed");
        return true;
    }
}