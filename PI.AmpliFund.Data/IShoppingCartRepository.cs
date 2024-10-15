namespace PI.AmpliFund.Data;

public interface IShoppingCartRepository
{
    ApplicationUser RetrieveApplicationUser(string applicationUserName);
    
    ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart);
}