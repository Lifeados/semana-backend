using MediatR;
using Product.Domain.Models.ProductAggregate.UseCases.Common;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.Resources.DomainValidation;
using Product.Domain.Validator;

namespace Product.Domain.Models.ProductAggregate.UseCases.Update;

public class UpdateProduct(IRepository repository, INotifierMessage notifierMessage, IValidatorGeneric validatorFactory)
    : IRequestHandler<UpdateProductInput, ProductModelOutput>
{
    public async Task<ProductModelOutput> Handle(UpdateProductInput request, CancellationToken cancellationToken)
    {
        var validatorResult = await validatorFactory.GetValidator<UpdateProductInput>()
            .ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
        {
            notifierMessage.AddRange(validatorResult.Errors?.Select(e => e.ErrorMessage)
                .ToList());
            return null;
        }

        var product = repository.Query<Entities.Product>()
            .FirstOrDefault(x => x.Id == request.Id);

        if (product is null)
        {
            notifierMessage.Add(string.Format(DomainValidationResource.NotFound, "product"));
            return null;
        }

        product.Update(request.Code, request.Name, request.Description, request.IsSignature);

        var isSuccess = await repository.CommitAsync(cancellationToken);
        if (!isSuccess)
        {
            notifierMessage.Add(DomainValidationResource.ErrorOnUpdate);
            return null;
        }

        return ProductModelOutput.FromEntity(product);
    }
}