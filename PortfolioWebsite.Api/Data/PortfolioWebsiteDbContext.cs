using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Api.Entities;

namespace PortfolioWebsite.Api.Data
{
	public class PortfolioWebsiteDbContext : DbContext
	{
		public PortfolioWebsiteDbContext(DbContextOptions<PortfolioWebsiteDbContext> options) : base(options)
		{

		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Products
			//Beauty Category
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 1,
				Name = "Suntari - Black T-Shirt",
				Description = "Merch for my band called Suntari",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/shirts/tr:w-800/suntarishirt2.jpg?updatedAt=1707594734174",
				Price = 25,
				Qty = 1000,
				CategoryId = 1

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 2,
				Name = "Inger Cowboy - Black T-Shirt",
				Description = "Merch for my other band called Inger Cowboy",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/shirts/tr:w-800/INGERTR%C3%96JA.jpg?updatedAt=1707594733912",
				Price = 25,
				Qty = 1000,
				CategoryId = 1

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 3,
				Name = "Los Mucus - Black T-Shirt",
				Description = "Merch for my third band called Los Mucus",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/shirts/tr:w-800/mucusshirt.jpg?updatedAt=1707594733635",
				Price = 25,
				Qty = 1000,
				CategoryId = 1

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 4,
				Name = "Obeshimi - Sticker",
				Description = "A cool sticker to stick on anything you want",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Gallery/Images/tr:w-800/GalleryImage%20(33).jpg?updatedAt=1705912745773",
				Price = 3,
				Qty = 1000,
				CategoryId = 2

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 5,
				Name = "Los Mucus - Sticker",
				Description = "A cool Los Mucus sticker to stick on anything you want",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/stickers/tr:w-800/stickerredversion2.jpg?updatedAt=1707594734695",
				Price = 3,
				Qty = 1000,
				CategoryId = 2

			});
			//Electronics Category
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 6,
				Name = "Los Mucus - Sticker",
				Description = "A cool Los Mucus sticker to stick on anything you want",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/stickers/tr:w-800/sticker.jpg?updatedAt=1707594734694",
				Price = 3,
				Qty = 1000,
				CategoryId = 2

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 7,
				Name = "Los Mucus - Sticker",
				Description = "A cool Los Mucus sticker to stick on anything you want",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/stickers/tr:w-800/Mucusskull.jpg?updatedAt=1707594734689",
				Price = 3,
				Qty = 1000,
				CategoryId = 2

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 8,
				Name = "Birds - Print",
				Description = "A 400x200 Print",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/birds.jpg?updatedAt=1707594856304",
				Price = 40,
				Qty = 1000,
				CategoryId = 3

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 9,
				Name = "Ascension - Print",
				Description = "A 400x200 Print",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/sun.jpg?updatedAt=1707594855618",
				Price = 40,
				Qty = 1000,
				CategoryId = 3

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 10,
				Name = "Explosion - Print",
				Description = "A 400x200 Print",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/explosion.jpg?updatedAt=1707594855179",
				Price = 40,
				Qty = 1000,
				CategoryId = 3

			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 11,
				Name = "Mother - Print",
				Description = "A 300x200 Print",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/holy.jpg?updatedAt=1707594853722",
				Price = 40,
				Qty = 1000,
				CategoryId = 3
			});
			//Furniture Category
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 12,
				Name = "Devotion - Print",
				Description = "A 400x200 Print",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/Devotion%20Gray.jpg?updatedAt=1707594734206",
				Price = 40,
				Qty = 1000,
				CategoryId = 3
			});

			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 13,
				Name = "No Tobacco - Print",
				Description = "A 400x200 Print",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/prints/tr:w-800/No%20Tobaccoshirt2.png?updatedAt=1707594733986",
				Price = 40,
				Qty = 1000,
				CategoryId = 3
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 14,
				Name = "Los Mucus / Flat Cap - Poster",
				Description = "A poster from a gig at Flat Cap, Helsingborg",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/Flatcap.jpg?updatedAt=1707594735646",
				Price = 20,
				Qty = 1000,
				CategoryId = 4
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 15,
				Name = "Los Mucus / Grand - Poster",
				Description = "A poster from a gig at Grand, Malmö",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/GRANDversion2.jpg?updatedAt=1707594734910",
				Price = 20,
				Qty = 1000,
				CategoryId = 4
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 16,
				Name = "Los Mucus / Flat Cap - Poster",
				Description = "A poster from a gig at Flat Cap, Helsingborg",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/Flatcap2.jpg?updatedAt=1707594734836",
				Price = 20,
				Qty = 1000,
				CategoryId = 4
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 17,
				Name = "Los Mucus / Hemgården - Poster",
				Description = "A poster from a gig at Hemgården, Lund",
				ImageURL = "https://ik.imagekit.io/valter/StefanPortfolio/Shop/posters/tr:w-800/Posterklar.jpg?updatedAt=1707594734268",
				Price = 20,
				Qty = 1000,
				CategoryId = 4
			});




			//Add Product Categories
			modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
			{
				Id = 1,
				Name = "Shirts"
			});
			modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
			{
				Id = 2,
				Name = "Stickers"
			});
			modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
			{
				Id = 3,
				Name = "Prints"
			});
			modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
			{
				Id = 4,
				Name = "Posters"
			});
		}


		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<User> Users { get; set; }
	}


}
