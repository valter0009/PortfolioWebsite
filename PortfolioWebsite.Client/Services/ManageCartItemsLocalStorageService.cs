using Blazored.LocalStorage;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Services
{
    public class ManageCartItemsLocalStorageService(ILocalStorageService localStorageService,
                                              IShoppingCartService shoppingCartService) : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private readonly IShoppingCartService _shoppingCartService = shoppingCartService;

        const string key = "CartItemCollection";

        public async Task<IEnumerable<CartItemDto>> GetCollection()
        {
            return await this._localStorageService.GetItemAsync<List<CartItemDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this._localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CartItemDto> cartItemDtos)
        {
            await this._localStorageService.SetItemAsync(key, cartItemDtos);
        }

        private async Task<List<CartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await this._shoppingCartService.GetItems();

            if (shoppingCartCollection != null)
            {
                await this._localStorageService.SetItemAsync(key, shoppingCartCollection);
            }

            return shoppingCartCollection;

        }

    }
}
