using PortfolioWebsite.Client.Services.Contracts;
using System.Net.Http.Json;

namespace PortfolioWebsite.Client.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory _httpClient;

        public EmailService(IHttpClientFactory httpClient)
        {
            this._httpClient = httpClient;
        }
        private HttpClient GetClient(bool requiresAuth = false)
        {
            return _httpClient.CreateClient(requiresAuth ? "AuthorizedClient" : "AnonymousClient");
        }
        public async Task SendEmailAsync(string subject, string message)
        {
            var httpClient = GetClient();
            var emailRequest = new { Subject = subject, Message = message };
            await httpClient.PostAsJsonAsync("api/email/send", emailRequest);
        }
    }
}
