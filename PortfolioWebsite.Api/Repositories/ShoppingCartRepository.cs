using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Api.Data;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;
using System.Security.Claims;

namespace PortfolioWebsite.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly PortfolioWebsiteDbContext portfolioWebsiteDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ShoppingCartRepository(
            PortfolioWebsiteDbContext portfolioWebsiteDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            this.portfolioWebsiteDbContext = portfolioWebsiteDbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId() => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<bool> CartItemExists(int productId)
        {
            var userCart = await GetOrCreateCart();
            return await this.portfolioWebsiteDbContext.CartItems
                .AnyAsync(c => c.CartId == userCart.Id && c.ProductId == productId);
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            var userCart = await GetOrCreateCart();
            if (await CartItemExists(cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in portfolioWebsiteDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = userCart.Id,
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

        public async Task<IEnumerable<CartItem>> GetItems()
        {
            return await (from cart in portfolioWebsiteDbContext.Carts
                          join cartItem in portfolioWebsiteDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == GetUserId()
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


        public async Task DeleteUserCart(string userId)
        {

            var cart = await portfolioWebsiteDbContext.Carts
                                .Include(c => c.CartItems)
                                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                portfolioWebsiteDbContext.CartItems.RemoveRange(cart.CartItems);

                portfolioWebsiteDbContext.Carts.Remove(cart);

                await portfolioWebsiteDbContext.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetOrCreateCart()
        {
            var cart = await portfolioWebsiteDbContext.Carts.FirstOrDefaultAsync(c => c.UserId == GetUserId());
            if (cart == null)
            {
                cart = new Cart { UserId = GetUserId() };
                portfolioWebsiteDbContext.Carts.Add(cart);
                await portfolioWebsiteDbContext.SaveChangesAsync();
            }
            return cart;
        }
        public async Task UpdateProductInventory(string userId)
        {
            var cartItems = await portfolioWebsiteDbContext.CartItems
                                .Where(ci => ci.Cart.UserId == userId)
                                .ToListAsync();

            foreach (var item in cartItems)
            {
                var product = await portfolioWebsiteDbContext.Products
                                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product != null && product.Qty >= item.Qty)
                {
                    product.Qty -= item.Qty;
                }
                else
                {
                    item.Qty = product.Qty;
                    product.Qty = 0;
                }
            }

            await portfolioWebsiteDbContext.SaveChangesAsync();
        }
        public async Task OnSuccessfulOrder()
        {
            var userId = GetUserId();
            await UpdateProductInventory(userId);
            await DeleteUserCart(userId);
        }


    }
}
