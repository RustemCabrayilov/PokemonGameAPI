using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PokemonGameAPI.Application.Abstraction.Services.Email;

namespace PokemonGameAPI.Infrastructure.Services.Email;

public class EmailService: IEmailService
{
    private EmailSettings _emailSettings;
    private readonly SmtpClient _smtpClient;

    public EmailService(IOptions<EmailSettings> configuration)
    {
        _emailSettings = configuration.Value;

        _smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_emailSettings.FromMail, _emailSettings.Password),

        };
    }
    public async Task SenEmailAsync(string toEmail, string subject, string message)
    {
        var mailMessage = new MailMessage()
        {
            From = new MailAddress(_emailSettings.FromMail, _emailSettings.FromName),
            Subject = subject,
            Body = message,
            IsBodyHtml = false
        };
        mailMessage.To.Add(toEmail);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}