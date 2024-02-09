namespace PortfolioWebsite.Api.Repositories.Contracts
{
    public interface IAuthRepository
    {
        string GetUserId();
        string GetUserEmail();
    }
}
