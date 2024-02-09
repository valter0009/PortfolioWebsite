using PortfolioWebsite.Api.Repositories.Contracts;
using System.Security.Claims;

namespace PortfolioWebsite.Api.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRepository(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
    }
}
