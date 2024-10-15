using Ardalis.Result;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using PI.AmpliFund.Business;
using PI.AmpliFund.Data;
using Xunit;

namespace PI.AmpliFund.Tests.Business;

public class ShoppingCartServiceTests
{
    private readonly ShoppingCartService _sut;
    private readonly IValidator<CreateShoppingCartPayload> _validator;
    private readonly Fixture _fixture;
    
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly CreateShoppingCartPayload _arbitraryPayload;

    public ShoppingCartServiceTests()
    {
        _fixture = new Fixture();
        
        _validator = Substitute.For<IValidator<CreateShoppingCartPayload>>();
        _validator.Validate(Arg.Any<CreateShoppingCartPayload>())
                  .Returns(new ValidationResult());
        
        _shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
        _shoppingCartRepository.RetrieveApplicationUser(Arg.Any<string>())
                               .Returns(_fixture.Create<ApplicationUser>());
        _shoppingCartRepository.CreateShoppingCart(Arg.Any<ShoppingCart>())
                               .Returns(_fixture.Create<ShoppingCart>());

        _sut = new ShoppingCartService(_validator, _shoppingCartRepository);
        
        _arbitraryPayload = _fixture.Create<CreateShoppingCartPayload>();

    }

    [Fact]
    public void CreateShoppingCart_Validates_Payload_Test()
    {
        var payload = _fixture.Create<CreateShoppingCartPayload>();
        
        
        _sut.CreateShoppingCart(payload);

        
        _validator.Received().Validate(payload);
    }
    
    [Fact]
    public void CreateShoppingCart_Handles_Invalid_Payload_Test()
    {

        var validationFailure = new ValidationResult(_fixture.CreateMany<ValidationFailure>());
        _validator.Validate(Arg.Any<CreateShoppingCartPayload>())
                  .Returns(validationFailure);
        
        
        var result = _sut.CreateShoppingCart(_arbitraryPayload);

        
        result.IsInvalid().Should().BeTrue();
        //
        //Really should be testing the error messages here, but want to complete.
        //
    }

    [Fact]
    public void CreateShoppingCart_Checks_For_User_Existence_Test()
    {
        var payload = _fixture.Create<CreateShoppingCartPayload>();
        
        
        _sut.CreateShoppingCart(payload);
        
        
        _shoppingCartRepository.Received().RetrieveApplicationUser(payload.ApplicationUserName);
    }
    
    [Fact]
    public void CreateShoppingCart_Handles_User_Not_Found_Test()
    {
        _shoppingCartRepository.RetrieveApplicationUser(Arg.Any<string>())
                               .Returns((ApplicationUser)null!);
        
        
        var result = _sut.CreateShoppingCart(_arbitraryPayload);

        
        result.IsInvalid().Should().BeTrue();
    }

    [Fact]
    public void CreateShoppingCart_Creates_New_Cart_Test()
    {
        var expectedUser = _fixture.Create<ApplicationUser>();
        _shoppingCartRepository.RetrieveApplicationUser(Arg.Any<string>())
                               .Returns(expectedUser);        
        
        
        _sut.CreateShoppingCart(_arbitraryPayload);
        
        
        _shoppingCartRepository.Received().CreateShoppingCart(Arg.Is<ShoppingCart>(s => s.Owner == expectedUser));
    }

    [Fact]
    public void CreateShoppintCart_Returns_New_Cart_Test()
    {
        var expectedCart = _fixture.Create<ShoppingCart>();
        _shoppingCartRepository.CreateShoppingCart(Arg.Any<ShoppingCart>())
                               .Returns(expectedCart);


        var result = _sut.CreateShoppingCart(_arbitraryPayload);


        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.ShoppingCartId.Should().Be(expectedCart.ShoppingCartId);
        }
    }
}