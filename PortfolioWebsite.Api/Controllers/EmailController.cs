using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;

namespace PortfolioWebsite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository emailRepository;

        public EmailController(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequest emailRequest)
        {
            await emailRepository.SendEmailAsync(emailRequest.Subject, emailRequest.Message);
            return Ok();
        }
    }
}
