namespace PI.AmpliFund.Data;

public interface IShoppingCartRepository
{
    ApplicationUser RetrieveApplicationUser(string applicationUserName);
    ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart);
    ShoppingCart RetrieveShoppingCart(Guid shoppingCartId);
    Product RetrieveProduct(string payloadProductSku);
    ShoppingCartItem CreateShoppingCartItem(ShoppingCartItem newCartItem);
    void DeleteShoppingCartItem(ShoppingCartItem cartItem);
    void SaveChanges();
}