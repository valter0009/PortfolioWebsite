using Blazored.LocalStorage;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Services
{
    public class ManageProductsLocalStorageService(ILocalStorageService localStorageService, IProductService productService) : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private readonly IProductService _productService = productService;

        private const string key = "ProductCollection";

        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await _localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await _productService.GetItems();
            if (productCollection != null)
            {
                await _localStorageService.SetItemAsync(key, productCollection);

            }
            return productCollection;
        }
    }
}
