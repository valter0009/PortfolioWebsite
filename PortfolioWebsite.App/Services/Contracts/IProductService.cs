using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.App.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();

        Task<ProductDto> GetItem(int id);

        Task<ProductDto> DeleteItem(int id);
    }
}
