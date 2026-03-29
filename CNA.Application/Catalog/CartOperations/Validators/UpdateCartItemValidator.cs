using CNA.Application.Behavior;
using FluentValidation;

namespace CNA.Application.Catalog.CartOperations.Validators
{
    public class UpdateCartItemValidator : AbstractValidator<RemoveCartItem.Command>
    {
        public UpdateCartItemValidator()
        {
            RuleFor(x => x.UserId)
            .RequiredId();

            RuleFor(x => x.CartItemId)
                .RequiredId();
        }
    }
}
