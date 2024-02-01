using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Api.Data;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly PortfolioWebsiteDbContext portfolioWebsiteDbContext;

        public ShoppingCartRepository(PortfolioWebsiteDbContext portfolioWebsiteDbContext)
        { this.portfolioWebsiteDbContext = portfolioWebsiteDbContext; }


        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.portfolioWebsiteDbContext.CartItems
                .AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in portfolioWebsiteDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty,
                                  }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await this.portfolioWebsiteDbContext.CartItems.AddAsync(item);
                    await this.portfolioWebsiteDbContext.SaveChangesAsync();

                    return result.Entity;
                }
            }

            return null;
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await portfolioWebsiteDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                portfolioWebsiteDbContext.CartItems.Remove(item);
                await portfolioWebsiteDbContext.SaveChangesAsync();
            }
            return item;

        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.portfolioWebsiteDbContext.Carts
                          join cartItem in this.portfolioWebsiteDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in portfolioWebsiteDbContext.Carts
                          join cartItem in portfolioWebsiteDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdate)
        {
            var item = await portfolioWebsiteDbContext.CartItems.FindAsync(id);
            if (item != null)
            {
                item.Qty = cartItemQtyUpdate.Qty;
                await portfolioWebsiteDbContext.SaveChangesAsync();
                return item;
            }
            return null;

        }


        public async Task DeleteItems(int userId)
        {

            var userCart = await portfolioWebsiteDbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

            if (userCart != null)
            {

                var cartItems = portfolioWebsiteDbContext.CartItems.Where(ci => ci.CartId == userCart.Id);


                portfolioWebsiteDbContext.CartItems.RemoveRange(cartItems);


                await portfolioWebsiteDbContext.SaveChangesAsync();
            }
        }


    }
}
