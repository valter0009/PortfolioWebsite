using Azure.Communication.Email;
using PortfolioWebsite.App.Services.Interfaces;

public class EmailService : IEmailService
{
    private readonly EmailClient emailClient;
    private readonly string fromAddress;
    private readonly string toAddress;

    public EmailService(EmailClient emailClient, IConfiguration configuration)
    {
        this.emailClient = emailClient;
        var emailSettings = configuration.GetSection("EmailSettings");
        this.fromAddress = emailSettings["FromAddress"];
        this.toAddress = emailSettings["ToAddress"];
    }

    public async Task SendEmailAsync(string subject, string message)
    {
        var fromEmailAddress = new EmailAddress(fromAddress);
        var toEmailAddress = new EmailAddress(toAddress);

        await emailClient.SendAsync(Azure.WaitUntil.Started, fromEmailAddress.Address, toEmailAddress.Address, subject, message);
    }
}