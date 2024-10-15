namespace PI.AmpliFund.Data;

public class ShoppingCart
{
    public Guid ShoppingCartId { get; set; }
    public byte[] RowVersion { get; set; }
    public Guid ApplicationUserId { get; set; }
    public virtual ApplicationUser Owner { get; set; }
    
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
}