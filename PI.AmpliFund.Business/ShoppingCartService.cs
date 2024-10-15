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

    public Result<ShoppingCartResponse> CreateShoppingCart(CreateShoppingCartPayload payload)
    {
        var validationResult = _validator.Validate(payload);
        if (!validationResult.IsValid)
        {
            return Result<ShoppingCartResponse>.Invalid(validationResult.AsErrors());
        }
        
        var cartOwner = _repository.RetrieveApplicationUser(payload.ApplicationUserName);
        if (cartOwner is null)
        {
            return Result<ShoppingCartResponse>.Invalid(new ValidationError("User Not Found"));
        }
        
        var cartToCreate = new ShoppingCart
        {
            Owner = cartOwner
        };
        
        var newlyCreatedCart = _repository.CreateShoppingCart(cartToCreate);
        return CreateResponse(newlyCreatedCart);

    }

    public Result<ShoppingCartResponse> UpdateShoppingCart(Guid shoppingCartId, UpdateShoppingCartPayload payload)
    {
        //
        //Need better validation here.
        //
        var cart = _repository.RetrieveShoppingCart(shoppingCartId);
        if (cart is null)
        {
            return Result<ShoppingCartResponse>.NotFound();
        }

        
        if (cart.ShoppingCartItems.Any(i => i.Product.ProductSku == payload.ProductSku))
        {
            var existingCartItem = cart.ShoppingCartItems.First(i => i.Product.ProductSku == payload.ProductSku);
            if (payload.Quantity <= 0)
            {
                _repository.DeleteShoppingCartItem(existingCartItem);
            }
            else
            {
                existingCartItem.Quantity += payload.Quantity;
                _repository.SaveChanges();
            }
            
            return CreateResponse(cart);
        }
        
        var product = _repository.RetrieveProduct(payload.ProductSku);
        if (product is null)
        {
            return Result<ShoppingCartResponse>.Invalid(new ValidationError("Product Not Found"));
        }
        
        var newCartItem = new ShoppingCartItem
        {
            ShoppingCart = cart,
            Product = product,
            Quantity = payload.Quantity
        };
        
        _repository.CreateShoppingCartItem(newCartItem);
        
        return CreateResponse(cart);
    }

    public Result<ShoppingCartResponse> RetrieveShoppingCart(Guid shoppingCartId)
    {
        var cart = _repository.RetrieveShoppingCart(shoppingCartId);
        if (cart is null)
        {
            return Result<ShoppingCartResponse>.NotFound();
        }
        
        return CreateResponse(cart);
    }

    private static Result<ShoppingCartResponse> CreateResponse(ShoppingCart cart)
    {
        var response = new ShoppingCartResponse
        {
            ShoppingCartId = cart.ShoppingCartId,
            Discount = cart.Owner.StoreMembership.Discount,
            Items = cart.ShoppingCartItems.Select(i => new ShoppingCartItemResponse
            {
                ShoppingCartItemId = i.ShoppingCartItemId,
                ProductSku = i.Product.ProductSku,
                Quantity = i.Quantity,
                Price = i.Product.Price,
                Description = i.Product.Description
            }).ToList()
        };
        return Result<ShoppingCartResponse>.Success(response);
    }
}