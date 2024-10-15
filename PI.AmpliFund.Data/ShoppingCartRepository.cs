using Microsoft.EntityFrameworkCore;

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

    public ShoppingCart RetrieveShoppingCart(Guid shoppingCartId)
    {
        return _context.ShoppingCart //.Include(s => s.ShoppingCartItems)
                                    .FirstOrDefault(s => s.ShoppingCartId == shoppingCartId)!;
    }

    public Product RetrieveProduct(string payloadProductSku)
    {
        return _context.Product.FirstOrDefault(p => p.ProductSku == payloadProductSku)!;
    }

    public ShoppingCartItem CreateShoppingCartItem(ShoppingCartItem newCartItem)
    {
        _context.ShoppingCartItem.Add(newCartItem);
        _context.SaveChanges();

        return newCartItem;
    }
    
    public void DeleteShoppingCartItem(ShoppingCartItem cartItem)
    {
        _context.ShoppingCartItem.Remove(cartItem);
        _context.SaveChanges();
    }
}
