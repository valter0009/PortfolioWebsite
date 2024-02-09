namespace PortfolioWebsite.Api.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
