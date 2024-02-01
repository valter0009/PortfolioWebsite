using Blazored.LocalStorage;
using PortfolioWebsite.App.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.App.Services
{
    public class ManageProductsLocalStorageService(ILocalStorageService localStorageService, IProductService productService) : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService = localStorageService;
        private readonly IProductService productService = productService;

        private const string key = "ProductCollection";

        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await productService.GetItems();
            if (productCollection != null)
            {
                await localStorageService.SetItemAsync(key, productCollection);

            }
            return productCollection;
        }
    }
}
