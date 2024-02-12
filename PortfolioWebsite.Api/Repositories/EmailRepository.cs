using Azure.Communication.Email;
using PortfolioWebsite.Api.Repositories.Contracts;

public class EmailRepository : IEmailRepository
{
    private readonly EmailClient _emailClient;
    private readonly string _fromAddress;
    private readonly string _toAddress;

    public EmailRepository(EmailClient emailClient, IConfiguration configuration)
    {
        this._emailClient = emailClient;
        var emailSettings = configuration.GetSection("EmailSettings");
        this._fromAddress = emailSettings["FromAddress"];
        this._toAddress = emailSettings["ToAddress"];
    }

    public async Task SendEmailAsync(string subject, string message)
    {
        var fromEmailAddress = new EmailAddress(_fromAddress);
        var toEmailAddress = new EmailAddress(_toAddress);

        await _emailClient.SendAsync(Azure.WaitUntil.Started, fromEmailAddress.Address, toEmailAddress.Address, subject,
            message);
    }
}