using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Services.Contracts
{
	public interface IManageCartItemsLocalStorageService
	{
		Task<IEnumerable<CartItemDto>> GetCollection();
		Task SaveCollection(List<CartItemDto> cartItemsDtos);
		Task RemoveCollection();
	}
}
