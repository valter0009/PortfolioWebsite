using PortfolioWebsite.Client.Services.Contracts;
using System.Net.Http.Json;

namespace PortfolioWebsite.Client.Services
{
	public class EmailService : IEmailService
	{
		private readonly HttpClient httpClient;

		public EmailService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task SendEmailAsync(string subject, string message)
		{
			var emailRequest = new { Subject = subject, Message = message };
			await httpClient.PostAsJsonAsync("api/email/send", emailRequest);
		}
	}
}
