using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Client.Services.Contracts
{
	public interface IManageProductsLocalStorageService
	{
		Task<IEnumerable<ProductDto>> GetCollection();

		Task RemoveCollection();




	}
}
