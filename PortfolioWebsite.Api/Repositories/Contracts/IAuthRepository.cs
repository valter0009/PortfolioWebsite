using PortfolioWebsite.Api.Entities;

namespace PortfolioWebsite.Api.Repositories.Contracts
{
    public interface IAuthRepository
    {
        string GetUserIdFromClaims();

        string GetUserEmailFromClaims();

        Task<User> GetUserById(string userId);

        Task<User> GetUserByEmail(string userEmail);
        Task<User> CreateUser();
    }
}
