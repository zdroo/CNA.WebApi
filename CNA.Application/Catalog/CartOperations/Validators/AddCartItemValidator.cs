using CNA.Application.Behavior;
using FluentValidation;

namespace CNA.Application.Catalog.CartOperations;

public class AddCartItemValidator : AbstractValidator<AddCartItem.Command>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.UserId)
            .RequiredId();

        RuleFor(x => x.ProductVariantId)
            .RequiredId();
    }
}