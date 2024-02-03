using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;
using Serilog;
using Stripe;

namespace PortfolioWebsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;


        public PaymentController(IPaymentRepository paymentRepository
                             )
        {
            this.paymentRepository = paymentRepository;

        }
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "whsec_e5d02861914032193d84368f065e8e31cba61af37c52ffb6a90eb8f746793f02";

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
                    Request.Headers["Stripe-Signature"], endpointSecret);


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

