namespace PI.AmpliFund.Business;

public class UpdateShoppingCartPayload
{
    public string ProductSku { get; set; } = null!;
    public int Quantity { get; set; }
}