using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;

namespace PortfolioWebsite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;

        public EmailController(IEmailRepository emailRepository)
        {
            this._emailRepository = emailRepository;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequest emailRequest)
        {
            await _emailRepository.SendEmailAsync(emailRequest.Subject, emailRequest.Message);
            return Ok();
        }
    }
}
