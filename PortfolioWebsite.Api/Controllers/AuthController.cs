using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Repositories.Contracts;

namespace PortfolioWebsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser()
        {

            var user = await _authRepository.CreateUser();
            if (user == null)
            {
                return Conflict("User already exists.");
            }

            return Ok(user);
        }
    }
}
