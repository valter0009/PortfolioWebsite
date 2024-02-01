using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentRepository paymentRepository) : ControllerBase
    {
        [HttpPost("checkout")]

        public ActionResult CreateCheckoutSessions(List<CartItemDto> cartItems)
        {
            var url = paymentRepository.CreateCheckoutSession(cartItems);
            return Ok(url);
        }
    }


}

