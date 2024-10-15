namespace PI.AmpliFund.Data;

public class ShoppingCartItem
{
    public Guid ShoppingCartItemId { get; set; }
    public byte[] RowVersion { get; set; }

    public Guid ShoppingCartId { get; set; }
    public virtual ShoppingCart ShoppingCart { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
}