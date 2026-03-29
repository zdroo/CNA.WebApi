using CNA.Application.Behavior;
using FluentValidation;

namespace CNA.Application.Catalog.CartOperations.Validators
{
    public class GetCartByUserIdValidator : AbstractValidator<GetCartByUserId.Query>
    {
        public GetCartByUserIdValidator()
        {
            RuleFor(x => x.UserId)
                .RequiredId();
        }
    }
}
