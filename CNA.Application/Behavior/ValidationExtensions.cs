using FluentValidation;

namespace CNA.Application.Behavior
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, Guid> RequiredId<T>(
            this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
