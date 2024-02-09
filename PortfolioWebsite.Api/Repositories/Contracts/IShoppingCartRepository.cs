using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAdd);

        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdate);

        Task<CartItem> DeleteItem(int id);

        Task DeleteUserCart(string userId);

        Task OnSuccessfulOrder();
        Task<CartItem> GetItem(int id);
        Task<Cart> GetOrCreateCart();
        Task<IEnumerable<CartItem>> GetItems();

    }
}
