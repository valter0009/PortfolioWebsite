using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;
using System.Globalization;


namespace PortfolioWebsite.Client.Components.Pages.Shop
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject] public IJSRuntime Js { get; set; }

        [Inject] NavigationManager NavManager { get; set; }


        [Inject] public IShoppingCartService ShoppingCartService { get; set; }

        [Inject] public IProductService ProductService { get; set; }

        [Inject] public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }


        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }

        private bool firstRenderCompleted = false;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !firstRenderCompleted)
            {
                firstRenderCompleted = true;

                try
                {
                    ShoppingCartItems = (List<CartItemDto>)await ManageCartItemsLocalStorageService.GetCollection();
                    foreach (var item in ShoppingCartItems)
                    {
                        var productDetails = await ProductService.GetItem(item.ProductId);
                        item.AvailableQuantity = productDetails.Qty;
                    }

                    CartChanged();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }

                StateHasChanged();
            }
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.DeleteItem(id);
                await RemoveCartItem(id);
                CartChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto { CartItemId = id, Qty = qty };

                    var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);

                    await UpdateItemTotalPrice(returnedUpdateItemDto);

                    CartChanged();

                    await MakeUpdateQtyButtonVisible(id, false);
                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(x => x.Id == id);
                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async Task UpdateQty_Input(int id)
        {
            await MakeUpdateQtyButtonVisible(id, true);
        }

        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

        private async Task UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }

            await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
        }

        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(x => x.TotalPrice).ToString("C", new CultureInfo("en-US"));
        }

        private void SetTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(x => x.Qty);
        }

        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }

        private async Task RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);
            ShoppingCartItems.Remove(cartItemDto);

            await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
        }

        private async Task RemoveCartItems()
        {
            await ShoppingCartService.DeleteItems();
            ShoppingCartItems.Clear();
            await ManageCartItemsLocalStorageService.RemoveCollection();
        }


        private void CartChanged()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }

        protected async Task Checkout_Click()
        {
            try
            {
                if (ShoppingCartItems.Count == 0)
                {
                    ErrorMessage = "Your cart is empty";
                    return;
                }

                var url = await ShoppingCartService.Checkout(ShoppingCartItems);

                NavManager.NavigateTo(url);
                await ManageCartItemsLocalStorageService.RemoveCollection();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}