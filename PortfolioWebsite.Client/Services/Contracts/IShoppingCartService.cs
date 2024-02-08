using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems();
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);

        Task<CartItemDto> DeleteItem(int id);

        Task<bool> DeleteItems();

        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);

        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQty);

        Task<int> GetItemsCount();

        public Task<string> Checkout(List<CartItemDto> cartItems);
    }
}
