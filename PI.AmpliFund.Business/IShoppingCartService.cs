using Ardalis.Result;

namespace PI.AmpliFund.Business;

public interface IShoppingCartService
{
    Result<CreateShoppingCartResponse> CreateShoppingCart(CreateShoppingCartPayload payload);
}