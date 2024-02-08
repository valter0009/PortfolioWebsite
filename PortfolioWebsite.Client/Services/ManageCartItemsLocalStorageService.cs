using Blazored.LocalStorage;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Services
{
	public class ManageCartItemsLocalStorageService(ILocalStorageService localStorageService,
											  IShoppingCartService shoppingCartService) : IManageCartItemsLocalStorageService
	{
		private readonly ILocalStorageService localStorageService = localStorageService;
		private readonly IShoppingCartService shoppingCartService = shoppingCartService;

		const string key = "CartItemCollection";

		public async Task<IEnumerable<CartItemDto>> GetCollection()
		{
			return await this.localStorageService.GetItemAsync<List<CartItemDto>>(key)
					?? await AddCollection();
		}

		public async Task RemoveCollection()
		{
			await this.localStorageService.RemoveItemAsync(key);
		}

		public async Task SaveCollection(List<CartItemDto> cartItemDtos)
		{
			await this.localStorageService.SetItemAsync(key, cartItemDtos);
		}

		private async Task<List<CartItemDto>> AddCollection()
		{
			var shoppingCartCollection = await this.shoppingCartService.GetItems();

			if (shoppingCartCollection != null)
			{
				await this.localStorageService.SetItemAsync(key, shoppingCartCollection);
			}

			return shoppingCartCollection;

		}

	}
}
