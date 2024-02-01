using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.App.Services.Contracts
{
    public interface IManageCartItemsLocalStorageService
    {
        Task<IEnumerable<CartItemDto>> GetCollection();
        Task SaveCollection(List<CartItemDto> cartItemsDtos);
        Task RemoveCollection();
    }
}
