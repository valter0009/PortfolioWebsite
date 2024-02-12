using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Api.Data;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;

namespace PortfolioWebsite.Api.Repositories
{
    public class ProductRepository(PortfolioWebsiteDbContext portfolioWebsiteDbContext) : IProductRepository
    {
        private readonly PortfolioWebsiteDbContext _portfolioWebsiteDbContext = portfolioWebsiteDbContext;

        public async Task<Product> DeleteItem(int id)
        {
            var product = await this._portfolioWebsiteDbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                this._portfolioWebsiteDbContext.Products.Remove(product);
                await this._portfolioWebsiteDbContext.SaveChangesAsync();
            }
            return product;
        }


        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this._portfolioWebsiteDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _portfolioWebsiteDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await _portfolioWebsiteDbContext.Products.Include(p => p.ProductCategory).SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this._portfolioWebsiteDbContext.Products.Include(p => p.ProductCategory).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await this._portfolioWebsiteDbContext.Products
                                     .Include(p => p.ProductCategory)
                                     .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }


    }
}
