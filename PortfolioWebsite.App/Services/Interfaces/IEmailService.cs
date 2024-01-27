namespace PortfolioWebsite.App.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string message);
    }
}
