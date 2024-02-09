using PortfolioWebsite.Models.DTOs;

namespace PortfolioWebsite.Api.Repositories.Contracts
{
    public interface IPaymentRepository
    {
        string CreateCheckoutSession(List<CartItemDto> cartItems);

        Task FulfillOrder(HttpRequest request);
    }
}
