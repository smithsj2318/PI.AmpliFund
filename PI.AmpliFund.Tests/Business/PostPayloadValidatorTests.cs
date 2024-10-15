using FluentValidation.TestHelper;
using PI.AmpliFund.Business;
using Xunit;

namespace PI.AmpliFund.Tests.Business;

public class PostPayloadValidatorTests
{
    private readonly PostPayloadValidator _sut = new();
    
    
    [Fact]
    public void Validate_Should_Validate_Empty_UserName_Test()
    {
        var postPayload = new CreateShoppingCartPayload();
        
        var result = _sut.TestValidate(postPayload);

        result.ShouldHaveValidationErrorFor(p => p.ApplicationUserName);
    }
    
    [Fact]
    public void Validate_Should_Validate_Bad_UserName_Test()
    {
        var postPayload = new CreateShoppingCartPayload
        {
            ApplicationUserName = "bad"
        };
        
        var result = _sut.TestValidate(postPayload);

        result.ShouldHaveValidationErrorFor(p => p.ApplicationUserName);
    }
    
    [Fact]
    public void Validate_Should_Validate_Good_UserName_Test()
    {
        var postPayload = new CreateShoppingCartPayload
        {
            ApplicationUserName = Faker.Internet.Email()
        };
        
        var result = _sut.TestValidate(postPayload);

        result.ShouldNotHaveValidationErrorFor(p => p.ApplicationUserName);
    }
}