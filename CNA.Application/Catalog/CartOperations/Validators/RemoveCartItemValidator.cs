using CNA.Application.Behavior;
using FluentValidation;

namespace CNA.Application.Catalog.CartOperations.Validators;
public class RemoveCartItemValidator : AbstractValidator<RemoveCartItem.Command>
{
    public RemoveCartItemValidator()
    {
        RuleFor(x => x.UserId)
            .RequiredId();

        RuleFor(x => x.CartItemId)
            .RequiredId();
    }
}
