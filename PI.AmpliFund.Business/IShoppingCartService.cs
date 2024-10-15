using Ardalis.Result;

namespace PI.AmpliFund.Business;

public interface IShoppingCartService
{
    Result<ShoppingCartResponse> CreateShoppingCart(CreateShoppingCartPayload payload);
    Result<ShoppingCartResponse> UpdateShoppingCart(Guid shoppingCartId, UpdateShoppingCartPayload payload);
    Result<ShoppingCartResponse> RetrieveShoppingCart(Guid shoppingCartId);
}