using FluentValidation;
using Product.Domain.Models.ProductAggregate.UseCases.Create;
using Product.Domain.Models.ProductAggregate.UseCases.Update;

namespace Product.Domain.Models.ProductAggregate.Validator;

public class UpdateProductInputValidator : AbstractValidator<UpdateProductInput>
{
    public UpdateProductInputValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(0);
        
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