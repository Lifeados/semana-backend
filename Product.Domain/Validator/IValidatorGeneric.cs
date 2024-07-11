using FluentValidation;

namespace Product.Domain.Validator;

public interface IValidatorGeneric
{
    IValidator<T> GetValidator<T>();
}
