using FluentValidation;
using Product.Domain.Models.ProductAggregate.Validator;

namespace Product.Api.Configurations.FluentValidation;

public static class FluentValidationConfig
{
    public static void AddFluentValidationValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateProductInputValidator>();
    }
}
