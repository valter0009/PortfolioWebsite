﻿using PortfolioWebsite.Api.Repositories.Contracts;
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
                BillingAddressCollection = "required",
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "US",
        "AE",
        "AG",
        "AL",
        "AM",
        "AR",
        "AT",
        "AU",
        "BA",
        "BE",
        "BG",
        "BH",
        "BO",
        "CA",
        "CH",
        "CI",
        "CL",
        "CO",
        "CR",
        "CY",
        "CZ",
        "DE",
        "DK",
        "DO",
        "EC",
        "EE",
        "EG",
        "ES",
        "ET",
        "FI",
        "FR",
        "GB",
        "GH",
        "GM",
        "GR",
        "GT",
        "GY",
        "HK",
        "HR",
        "HU",
        "ID",
        "IE",
        "IL",
        "IS",
        "IT",
        "JM",
        "JO",
        "JP",
        "KE",
        "KH",
        "KR",
        "KW",
        "LC",
        "LI",
        "LK",
        "LT",
        "LU",
        "LV",
        "MA",
        "MD",
        "MG",
        "MK",
        "MN",
        "MO",
        "MT",
        "MU",
        "MX",
        "MY",
        "NA",
        "NG",
        "NL",
        "NO",
        "NZ",
        "OM",
        "PA",
        "PE",
        "PH",
        "PL",
        "PT",
        "PY",
        "QA",
        "RO",
        "RS",
        "RW",
        "SA",
        "SE",
        "SG",
        "SI",
        "SK",
        "SN",
        "SV",
        "TH",
        "TN",
        "TR",
        "TT",
        "TZ",
        "UY",
        "UZ",
        "VN",
        "ZA",
        "BD",
        "BJ",
        "MC",
        "NE",
        "SM",
        "AZ",
        "BN",
        "BT",
        "AO",
        "DZ",
        "TW",
        "BS",
        "BW",
        "GA",
        "LA",
        "MZ",
        "KZ",
        "PK"}
                },
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