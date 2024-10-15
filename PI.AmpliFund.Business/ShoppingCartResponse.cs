namespace PI.AmpliFund.Business;

public class ShoppingCartResponse
{
    public Guid ShoppingCartId { get; set; }
    public IEnumerable<ShoppingCartItemResponse> Items { get; set; } = null!;
    
    public decimal Discount { get; set; }
}