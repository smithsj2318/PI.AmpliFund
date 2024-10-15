namespace PI.AmpliFund.Data;

public class Product
{
    public Guid ProductId { get; set; }
    public byte[] RowVersion { get; set; }
    public string ProductSku { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
}