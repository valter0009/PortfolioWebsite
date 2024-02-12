using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Components.Pages.Shop
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }


        public IEnumerable<ProductDto> Products;











        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !firstRenderCompleted)
            {
                firstRenderCompleted = true;

                Products = await ManageProductsLocalStorageService.GetCollection();

                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                if (authState.User.Identity.IsAuthenticated)
                {
                    await ClearLocalStorage();
                    var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
                    var totalQty = shoppingCartItems.Sum(i => i.Qty);
                    ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
                }

                StateHasChanged();
            }
        }



        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetFroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

        private async Task ClearLocalStorage()
        {
            await ManageCartItemsLocalStorageService.RemoveCollection();
            await ManageProductsLocalStorageService.RemoveCollection();
        }
    }
}
