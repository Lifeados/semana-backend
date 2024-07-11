using FluentValidation;
using Product.Domain.Models.ProductAggregate.UseCases.Create;

namespace Product.Domain.Models.ProductAggregate.Validator;

public class CreateProductInputValidator : AbstractValidator<CreateProductInput>
{
    public CreateProductInputValidator()
    {
        RuleFor(x => x.EstablishmentId)
            .NotNull()
            .GreaterThan(0);
        
        RuleFor(x => x.Code)
            .NotNull().MinimumLength(3);
        
        RuleFor(x => x.Name)
            .NotNull().MinimumLength(3);
        
        RuleFor(x => x.Description)
            .NotNull().MinimumLength(20);
    }
}