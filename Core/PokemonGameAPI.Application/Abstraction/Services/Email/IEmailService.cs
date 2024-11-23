namespace PokemonGameAPI.Application.Abstraction.Services.Email;

public interface IEmailService
{
    Task SenEmailAsync(string email, string subject, string body);
}