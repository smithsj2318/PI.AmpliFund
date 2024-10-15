namespace PI.AmpliFund.Data;


public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ShoppingCartContext _context;

    public ShoppingCartRepository(ShoppingCartContext shoppingCartContext)
    {
        _context = shoppingCartContext;
    }
    
    public ApplicationUser RetrieveApplicationUser(string applicationUserName)
    {
        return _context.ApplicationUser
            .FirstOrDefault(au => au.ApplicationUserName == applicationUserName)!;
    }

    public ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart)
    {
        _context.ShoppingCart.Add(shoppingCart);
        _context.SaveChanges();
        
        return shoppingCart;
    }
}
