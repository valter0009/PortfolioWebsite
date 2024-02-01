using Microsoft.AspNetCore.Components;
using PortfolioWebsite.App.Services.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.App.Components.Pages.Shop
{
    public class ProductDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        public ProductDto Product { get; set; }

        public string ErrorMessage { get; set; }

        private List<CartItemDto> ShoppingCartItems { get; set; }


        private bool firstRenderCompleted = false;

        protected override async Task OnInitializedAsync()
        {

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !firstRenderCompleted)
            {
                firstRenderCompleted = true;

                try
                {
                    ShoppingCartItems = (List<CartItemDto>)await ManageCartItemsLocalStorageService.GetCollection();
                    Product = await GetProductById(Id);
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }

                StateHasChanged();
            }
        }

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);
                if (cartItemDto != null)
                {
                    ShoppingCartItems.Add(cartItemDto);
                    await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

                }

                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<ProductDto> GetProductById(int id)
        {
            var productsDtos = await ManageProductsLocalStorageService.GetCollection();
            if (productsDtos != null)
            {
                return productsDtos.SingleOrDefault(x => x.Id == id);
            }
            return null;
        }
    }
}
