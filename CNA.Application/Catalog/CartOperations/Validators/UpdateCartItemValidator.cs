using CNA.Application.Behavior;
using FluentValidation;

namespace CNA.Application.Catalog.CartOperations.Validators
{
    public class UpdateCartItemValidator : AbstractValidator<RemoveCartItem.Command>
    {
        public UpdateCartItemValidator()
        {
            RuleFor(x => x)
            .Must(x => x.UserId.HasValue && x.UserId.Value != Guid.Empty ||
                       x.SessionId.HasValue && x.SessionId.Value != Guid.Empty)
            .WithMessage("Either a valid UserId or a valid SessionId must be provided.");

            RuleFor(x => x.CartItemId)
                .RequiredId();
        }
    }
}
