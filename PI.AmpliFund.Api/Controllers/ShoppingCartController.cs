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
    
    [HttpPut("{shoppingCartId}")]
    public IActionResult Put(Guid shoppingCartId, [FromBody] UpdateShoppingCartPayload payload)
    {
        var result = _shoppingCartService.UpdateShoppingCart(shoppingCartId, payload);
        if (result.IsInvalid())
        {
            return BadRequest(result.ValidationErrors);
        }

        if (result.IsNotFound())
        {
            return NotFound();
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("{shoppingCartId}")]
    public IActionResult Get(Guid shoppingCartId)
    {
        var result = _shoppingCartService.RetrieveShoppingCart(shoppingCartId);
        if (result.IsNotFound())
        {
            return NotFound();
        }
        
        return Ok(result.Value);
    }
}