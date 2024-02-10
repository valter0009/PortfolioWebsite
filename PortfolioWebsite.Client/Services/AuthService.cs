using Microsoft.AspNetCore.Components.Authorization;
using PortfolioWebsite.Client.Services.Contracts;
using System.Text;

namespace PortfolioWebsite.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(IHttpClientFactory httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }
        private HttpClient GetClient(bool requiresAuth = false)
        {
            return _httpClient.CreateClient(requiresAuth ? "AuthorizedClient" : "AnonymousClient");
        }
        public async Task CreateUser()
        {
            var httpClient = GetClient(true);
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity.IsAuthenticated)
            {
                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                await httpClient.PostAsync("api/auth/createUser", content);
            }

        }
    }
}
