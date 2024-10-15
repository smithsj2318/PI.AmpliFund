using Ardalis.Result;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PI.AmpliFund.Api.Controllers;
using PI.AmpliFund.Business;
using Xunit;

namespace PI.AmpliFund.Tests.Api;

public class ShoppingCartControllerTests
{
    private readonly ShoppingCartController _sut;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly Fixture _fixture;
    
    private readonly CreateShoppingCartPayload _arbitaryPayload;

    public ShoppingCartControllerTests()
    {
        _fixture = new Fixture();
        _shoppingCartService = Substitute.For<IShoppingCartService>();
        
        _sut = new ShoppingCartController(_shoppingCartService);
        
        _arbitaryPayload = _fixture.Create<CreateShoppingCartPayload>();
    }

    [Fact]
    public void Post_Creates_ShoppingCart_Test()
    {
        var expectedPayload = _fixture.Create<CreateShoppingCartPayload>();
        
        _sut.Post(expectedPayload);
        
        _shoppingCartService.Received(1).CreateShoppingCart(expectedPayload);
    }

    [Fact]
    public void Post_Handles_Invalid_Test()
    {
        var expectedMessages = _fixture.CreateMany<ValidationError>().ToList();
        var cartResponse = Result<CreateShoppingCartResponse>.Invalid(expectedMessages);
        _shoppingCartService.CreateShoppingCart(Arg.Any<CreateShoppingCartPayload>())
                            .Returns(cartResponse);
        
        var result = _sut.Post(_arbitaryPayload);

        using (new AssertionScope())
        {
            result.Should().NotBeNull()
                           .And.BeOfType<BadRequestObjectResult>()
                               .Which.Value.Should().BeEquivalentTo(expectedMessages);
        }
    }
}