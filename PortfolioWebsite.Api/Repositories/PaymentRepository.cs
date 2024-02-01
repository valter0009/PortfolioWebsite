using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;
using Stripe;
using Stripe.Checkout;

namespace PortfolioWebsite.Api.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentRepository()
        {
            StripeConfiguration.ApiKey = "sk_test_51OemNmLRRNULBB8OshOA7jqTzqqGKxu5DdmW9inwiimryRT4Zxrw9BUAbI9Eb3U7gjqQA13tfvS0aJ4ejZSinLkr00SVfx8Cda";
        }
        public string CreateCheckoutSession(List<CartItemDto> cartItems)
        {
            if (cartItems == null)
            {
                return null;
            }

            var lineItems = new List<SessionLineItemOptions>();
            cartItems.ForEach(ci => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = ci.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = ci.ProductName,
                        Description = ci.ProductDescription
                    }
                },
                Quantity = ci.Qty
            }));

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["card"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7097/order-success",
                CancelUrl = "https://localhost:7097"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session.Url;
        }
    }
}
