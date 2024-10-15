using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using PI.AmpliFund.Data;

namespace PI.AmpliFund.Business;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IValidator<CreateShoppingCartPayload> _validator;
    private readonly IShoppingCartRepository _repository;

    public ShoppingCartService(IValidator<CreateShoppingCartPayload> validator,
                               IShoppingCartRepository shoppingCartRepository)
    {
        _validator = validator;
        _repository = shoppingCartRepository;
    }

    public Result<CreateShoppingCartResponse> CreateShoppingCart(CreateShoppingCartPayload payload)
    {
        var validationResult = _validator.Validate(payload);
        if (!validationResult.IsValid)
        {
            return Result<CreateShoppingCartResponse>.Invalid(validationResult.AsErrors());
        }
        
        var cartOwner = _repository.RetrieveApplicationUser(payload.ApplicationUserName);
        if (cartOwner is null)
        {
            return Result<CreateShoppingCartResponse>.Invalid(new ValidationError("User Not Found"));
        }
        
        var cartToCreate = new ShoppingCart
        {
            Owner = cartOwner
        };
        var newlyCreatedCart = _repository.CreateShoppingCart(cartToCreate);

        var response = new CreateShoppingCartResponse
        {
            ShoppingCartId = newlyCreatedCart.ShoppingCartId
        };
        return Result<CreateShoppingCartResponse>.Success(response);

    }
}