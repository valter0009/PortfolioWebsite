﻿using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;
using Serilog;
using Stripe;
using Stripe.Checkout;

namespace PortfolioWebsite.Api.Repositories
{
	public class PaymentRepository : IPaymentRepository
	{
		private readonly string _stripeApiKey;
		private readonly string _stripeEndpointSecret;
		private readonly IShoppingCartRepository _shoppingCartRepository;
		private readonly IAuthRepository _authRepository;


		public PaymentRepository(
			IConfiguration configuration,
			IShoppingCartRepository shoppingCartRepository,
			IAuthRepository authRepository)
		{
			_stripeApiKey = configuration["StripeApiKey"];
			_stripeEndpointSecret = configuration["StripeEndpoindScrt"];
			StripeConfiguration.ApiKey = _stripeApiKey;

			this._shoppingCartRepository = shoppingCartRepository;
			this._authRepository = authRepository;
		}
		//Using webhook to listen for checkout session completed event and fulfilling order
		public async Task FulfillOrder(HttpRequest request)
		{
			var json = await new StreamReader(request.Body).ReadToEndAsync();

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(
					json,
					request.Headers["Stripe-Signature"],
					_stripeEndpointSecret);

				if (stripeEvent.Type == Events.CheckoutSessionCompleted)
				{
					var session = stripeEvent.Data.Object as Session;

					var email = session.CustomerEmail;
					var user = await _authRepository.GetUserByEmail(email);
					await _shoppingCartRepository.OnSuccessfulOrder(user.Id);
					Log.Information("Fulfillorder => {@json}", json);
				}
			}
			catch (Exception e)
			{
			}
		}

		public string CreateCheckoutSession(List<CartItemDto> cartItems)
		{
			if (cartItems == null)
			{
				return null;
			}

			var lineItems = new List<SessionLineItemOptions>();
			cartItems.ForEach(
				ci => lineItems.Add(
					new SessionLineItemOptions
					{
						PriceData =
							new SessionLineItemPriceDataOptions
							{
								UnitAmountDecimal = ci.Price * 100,
								Currency = "usd",
								ProductData =
									new SessionLineItemPriceDataProductDataOptions
									{
										Name = ci.ProductName,
										Description = ci.ProductDescription,
										Images = new List<string> { ci.ProductImageURL }
									}
							},
						Quantity = ci.Qty
					}));

			var options = new SessionCreateOptions
			{
				CustomerEmail = _authRepository.GetUserEmailFromClaims(),
				PaymentMethodTypes = ["card"],
				LineItems = lineItems,
				BillingAddressCollection = "required",
				ShippingAddressCollection =
					new SessionShippingAddressCollectionOptions
					{
						AllowedCountries =
							new List<string>
							{
								"US",
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
								"PK"
							}
					},
				Mode = "payment",
				SuccessUrl = "https://solaricrealmappservice.azurewebsites.net/order-success",
				CancelUrl = "https://solaricrealmappservice.azurewebsites.net"
			};

			var service = new SessionService();
			Session session = service.Create(options);

			return session.Url;
		}
	}
}