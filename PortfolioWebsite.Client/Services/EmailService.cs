using PortfolioWebsite.Client.Services.Contracts;
using System.Net.Http.Json;

namespace PortfolioWebsite.Client.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory httpClient;

        public EmailService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }
        private HttpClient GetClient(bool requiresAuth = false)
        {
            return httpClient.CreateClient(requiresAuth ? "AuthorizedClient" : "AnonymousClient");
        }
        public async Task SendEmailAsync(string subject, string message)
        {
            var httpClient = GetClient();
            var emailRequest = new { Subject = subject, Message = message };
            await httpClient.PostAsJsonAsync("api/email/send", emailRequest);
        }
    }
}
