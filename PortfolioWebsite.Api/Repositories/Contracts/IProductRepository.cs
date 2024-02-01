using PortfolioWebsite.Api.Entities;

namespace PortfolioWebsite.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();

        Task<IEnumerable<ProductCategory>> GetCategories();

        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);
        Task<IEnumerable<Product>> GetItemsByCategory(int id);

        Task<Product> DeleteItem(int id);
    }
}
