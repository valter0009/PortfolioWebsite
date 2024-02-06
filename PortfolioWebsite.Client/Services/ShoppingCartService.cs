using Newtonsoft.Json;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;
using System.Net.Http.Json;
using System.Text;

namespace PortfolioWebsite.Client.Services
{
	public class ShoppingCartService : IShoppingCartService
	{
		private readonly HttpClient httpClient;

		public event Action<int> OnShoppingCartChanged;

		public ShoppingCartService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto)
		{
			try
			{
				var response = await httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", cartItemToAddDto);

				if (response.IsSuccessStatusCode)
				{
					if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
					{
						return default(CartItemDto);
					}

					return await response.Content.ReadFromJsonAsync<CartItemDto>();

				}
				else
				{
					var message = await response.Content.ReadAsStringAsync();
					throw new Exception($"Http status:{response.StatusCode} Message -{message}");
				}

			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<CartItemDto> DeleteItem(int id)
		{
			try
			{
				var response = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");

				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadFromJsonAsync<CartItemDto>();
				}
				return default(CartItemDto);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<List<CartItemDto>> GetItems(int userId)
		{
			try
			{
				var response = await httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

				if (response.IsSuccessStatusCode)
				{
					if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
					{
						return Enumerable.Empty<CartItemDto>().ToList();
					}
					return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
				}
				else
				{
					var message = await response.Content.ReadAsStringAsync();
					throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
				}

			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<string> Checkout(List<CartItemDto> cartItems)
		{
			var jsonRequest = JsonConvert.SerializeObject(cartItems);
			var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync("api/payment/checkout", content);

			var url = await response.Content.ReadAsStringAsync();
			return url;
		}

		public void RaiseEventOnShoppingCartChanged(int totalQty)
		{
			OnShoppingCartChanged?.Invoke(totalQty);
		}

		public async Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
		{
			try
			{
				var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
				var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

				var response = await httpClient.PatchAsync($"api/ShoppingCart/{cartItemQtyUpdateDto.CartItemId}", content);

				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadFromJsonAsync<CartItemDto>();
				}
				return null;

			}
			catch (Exception)
			{
				//Log exception
				throw;
			}
		}

		public async Task<bool> DeleteItems(int userId)
		{
			var response = await httpClient.DeleteAsync($"api/ShoppingCart/{userId}/DeleteItems");

			if (response.IsSuccessStatusCode)
			{

				return response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK;
			}
			else
			{
				var message = await response.Content.ReadAsStringAsync();
				throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
			}
		}
	}
}

