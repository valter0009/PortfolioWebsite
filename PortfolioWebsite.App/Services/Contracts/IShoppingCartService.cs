﻿using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.App.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> DeleteItem(int id);
        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);

        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQty);

        public Task<string> Checkout(List<CartItemDto> cartItems);
    }
}
