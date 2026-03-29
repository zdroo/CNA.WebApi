using CNA.Application.Behavior;
using FluentValidation;

namespace CNA.Application.Catalog.CartOperations.Validators;
public class ClearCartValidator : AbstractValidator<ClearCart.Command>
{
    public ClearCartValidator()
    {
        RuleFor(x => x.UserId)
            .RequiredId();
    }
}