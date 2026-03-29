using CNA.Application.Behavior;
using FluentValidation;

namespace CNA.Application.Catalog.CartOperations.Validators;

public class CartCheckoutValidator : AbstractValidator<CartCheckout.Command>
{
    public CartCheckoutValidator()
    {
        RuleFor(x => x.UserId).RequiredId();

        RuleFor(x => x.ShippingContactId)
            .RequiredId();

        RuleFor(x => x.CartItemIds)
            .NotEmpty()
            .WithMessage("At least one cart item must be selected for checkout.");
    }
}
