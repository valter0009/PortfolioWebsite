using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Api.Data;
using PortfolioWebsite.Api.Entities;
using PortfolioWebsite.Api.Repositories.Contracts;
using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly PortfolioWebsiteDbContext _portfolioWebsiteDbContext;
        private readonly IAuthRepository _authRepository;

        public ShoppingCartRepository(
            PortfolioWebsiteDbContext portfolioWebsiteDbContext,
            IAuthRepository authRepository)
        {
            this._portfolioWebsiteDbContext = portfolioWebsiteDbContext;

            this._authRepository = authRepository;
        }


        private async Task<bool> CartItemExists(int productId)
        {
            var userCart = await GetOrCreateCart();
            return await this._portfolioWebsiteDbContext.CartItems
                .AnyAsync(c => c.CartId == userCart.Id && c.ProductId == productId);
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            var userCart = await GetOrCreateCart();

            if (await CartItemExists(cartItemToAddDto.ProductId) != false) return null;

            var item = await (from product in _portfolioWebsiteDbContext.Products
                              where product.Id == cartItemToAddDto.ProductId
                              select new CartItem
                              {
                                  CartId = userCart.Id,
                                  ProductId = product.Id,
                                  Qty = cartItemToAddDto.Qty,
                              }).SingleOrDefaultAsync();

            if (item == null) return null;

            var result = await this._portfolioWebsiteDbContext.CartItems.AddAsync(item);
            await this._portfolioWebsiteDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await _portfolioWebsiteDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                _portfolioWebsiteDbContext.CartItems.Remove(item);
                await _portfolioWebsiteDbContext.SaveChangesAsync();
            }

            return item;
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this._portfolioWebsiteDbContext.Carts
                          join cartItem in this._portfolioWebsiteDbContext.CartItems
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
            var userId = _authRepository.GetUserIdFromClaims();
            return await (from cart in _portfolioWebsiteDbContext.Carts
                          join cartItem in _portfolioWebsiteDbContext.CartItems
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
            var item = await _portfolioWebsiteDbContext.CartItems.FindAsync(id);
            if (item != null)
            {
                item.Qty = cartItemQtyUpdate.Qty;
                await _portfolioWebsiteDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }


        public async Task DeleteUserCart(string userId)
        {
            var cart = await _portfolioWebsiteDbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _portfolioWebsiteDbContext.CartItems.RemoveRange(cart.CartItems);

                _portfolioWebsiteDbContext.Carts.Remove(cart);

                await _portfolioWebsiteDbContext.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetOrCreateCart()
        {
            var userId = _authRepository.GetUserIdFromClaims();
            var cart = await _portfolioWebsiteDbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _portfolioWebsiteDbContext.Carts.Add(cart);
                await _portfolioWebsiteDbContext.SaveChangesAsync();
            }

            return cart;
        }

        public async Task UpdateProductInventory(string userId)
        {
            var cartItems = await _portfolioWebsiteDbContext.CartItems
                .Where(ci => ci.Cart.UserId == userId)
                .ToListAsync();

            foreach (var item in cartItems)
            {
                var product = await _portfolioWebsiteDbContext.Products
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

            await _portfolioWebsiteDbContext.SaveChangesAsync();
        }

        public async Task OnSuccessfulOrder(string userId)
        {
            await UpdateProductInventory(userId);
            await DeleteUserCart(userId);
        }
    }
}