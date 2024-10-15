namespace PI.AmpliFund.Business;

public class ShoppingCartItemResponse
{
    public Guid ShoppingCartItemId { get; set; }
    public string ProductSku { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}