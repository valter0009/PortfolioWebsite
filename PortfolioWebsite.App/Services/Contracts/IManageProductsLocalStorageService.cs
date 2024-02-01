using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.App.Services.Contracts
{
    public interface IManageProductsLocalStorageService
    {
        Task<IEnumerable<ProductDto>> GetCollection();

        Task RemoveCollection();




    }
}
