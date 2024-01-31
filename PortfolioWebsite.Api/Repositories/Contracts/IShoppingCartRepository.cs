using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Repositories.Contracts
{
	public interface IShoppingCartRepository
	{
		Task<CartItem> AddItem(CartItemToAddDto cartItemToAdd);

		Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdate);

		Task<CartItem> DeleteItem(int id);

		Task<CartItem> GetItem(int id);

		Task<IEnumerable<CartItem>> GetItems(int userId);
	}
}
