using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using PI.AmpliFund.Business;

namespace PI.AmpliFund.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingCartController: ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] CreateShoppingCartPayload payload)
    {
        var result = _shoppingCartService.CreateShoppingCart(payload);
        if (result.IsInvalid())
        {
            return BadRequest(result.ValidationErrors);
        }
        
        return Ok(result.Value);
    }
}