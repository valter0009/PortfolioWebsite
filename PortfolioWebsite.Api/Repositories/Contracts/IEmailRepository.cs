namespace PortfolioWebsite.Api.Repositories.Contracts
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string subject, string message);
    }
}

