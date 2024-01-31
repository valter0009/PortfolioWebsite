using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Api.Data;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;

namespace PortfolioWebsite.Api.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly PortfolioWebsiteDbContext portfolioWebsiteDbContext;

		public ProductRepository(PortfolioWebsiteDbContext portfolioWebsiteDbContext)
		{
			this.portfolioWebsiteDbContext = portfolioWebsiteDbContext;
		}
		public async Task<IEnumerable<ProductCategory>> GetCategories()
		{
			var categories = await this.portfolioWebsiteDbContext.ProductCategories.ToListAsync();
			return categories;
		}

		public async Task<ProductCategory> GetCategory(int id)
		{
			var category = await portfolioWebsiteDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
			return category;
		}

		public async Task<Product> GetItem(int id)
		{
			var product = await portfolioWebsiteDbContext.Products.FindAsync(id);
			return product;
		}

		public async Task<IEnumerable<Product>> GetItems()
		{
			var products = await this.portfolioWebsiteDbContext.Products.ToListAsync();
			return products;
		}
	}
}
