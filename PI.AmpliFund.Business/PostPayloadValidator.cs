using FluentValidation;

namespace PI.AmpliFund.Business;

public class PostPayloadValidator : AbstractValidator<CreateShoppingCartPayload>
{
    public PostPayloadValidator()
    {
        RuleFor(x => x.ApplicationUserName)
            .NotEmpty()
            .EmailAddress();
    }
}