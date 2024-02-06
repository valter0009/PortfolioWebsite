using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;
using Serilog;
using Stripe;

namespace PortfolioWebsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly string _stripeEndpointSecret;

        public PaymentController(IPaymentRepository paymentRepository, IConfiguration configuration
                             )
        {
            this.paymentRepository = paymentRepository;

            _stripeEndpointSecret = configuration["StripeEndpoindScrt"];

        }
        // This is your Stripe CLI webhook secret for testing your endpoint locally.


        [HttpPost("checkout")]

        public ActionResult CreateCheckoutSessions(List<CartItemDto> cartItems)
        {
            var url = paymentRepository.CreateCheckoutSession(cartItems);
            return Ok(url);
        }



        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Log.Information("WebhookStripe => {@json}", json);

            try

            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _stripeEndpointSecret);


                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;


                }

                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

    }


}

