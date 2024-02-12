using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Api.Data;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;
using System.Security.Claims;

namespace PortfolioWebsite.Api.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PortfolioWebsiteDbContext _portfolioWebsiteDbContext;

        public AuthRepository(IHttpContextAccessor httpContextAccessor,
            PortfolioWebsiteDbContext portfolioWebsiteDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            this._portfolioWebsiteDbContext = portfolioWebsiteDbContext;
        }

        public string GetUserIdFromClaims() =>
            _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public string GetUserEmailFromClaims() =>
            _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

        public Task<User> GetUserById(string userId)
        {
            if (userId != null)
            {
                var user = _portfolioWebsiteDbContext.Users.SingleOrDefaultAsync(u => u.Id.Equals(userId));
                return user;
            }

            return null;
        }

        public Task<User> GetUserByEmail(string userEmail)
        {
            if (userEmail != null)
            {
                var user = _portfolioWebsiteDbContext.Users.SingleOrDefaultAsync(u => u.Email.Equals(userEmail));
                return user;
            }

            return null;
        }

        public async Task<User> CreateUser()
        {
            try
            {
                var userId = GetUserIdFromClaims();
                var userEmail = GetUserEmailFromClaims();


                var existingUserById = await GetUserById(userId);
                var existingUserByEmail = await GetUserByEmail(userEmail);

                if (existingUserById != null || existingUserByEmail != null)
                {
                    return null;
                }

                var user = new User
                {
                    Id = userId,
                    Email = userEmail
                };

                await _portfolioWebsiteDbContext.Users.AddAsync(user);
                await _portfolioWebsiteDbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}