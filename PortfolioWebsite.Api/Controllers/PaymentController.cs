using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly string _stripeEndpointSecret;


        public PaymentController(IPaymentRepository paymentRepository, IConfiguration configuration)
        {
            this.paymentRepository = paymentRepository;

            _stripeEndpointSecret = configuration["StripeEndpoindScrt"];
        }


        [HttpPost("checkout")]

        public ActionResult CreateCheckoutSessions(List<CartItemDto> cartItems)
        {
            var url = paymentRepository.CreateCheckoutSession(cartItems);
            return Ok(url);
        }

        [HttpPost]

        public async Task<IActionResult> FulfillOrder()
        {
            try
            {
                await paymentRepository.FulfillOrder(Request);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }



    }
}

