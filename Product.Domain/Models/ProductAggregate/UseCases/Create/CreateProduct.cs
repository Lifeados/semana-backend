using MediatR;
using Product.Domain.Models.ProductAggregate.UseCases.Common;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.Resources.DomainValidation;
using Product.Domain.Validator;

namespace Product.Domain.Models.ProductAggregate.UseCases.Create;

public class CreateProduct(IRepository repository, INotifierMessage notifierMessage, IValidatorGeneric validatorFactory)
    : IRequestHandler<CreateProductInput, ProductModelOutput>
{
    public async Task<ProductModelOutput> Handle(CreateProductInput request, CancellationToken cancellationToken)
    {
        var validatorResult = await validatorFactory.GetValidator<CreateProductInput>()
            .ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
            notifierMessage.AddRange(validatorResult.Errors?.Select(e => e.ErrorMessage)
                .ToList());

        var productCode = repository.QueryAsNoTracking<Entities.Product>()
            .FirstOrDefault(x => x.Code == request.Code);

        if (productCode is not null)
        {
            notifierMessage.Add(string.Format(DomainValidationResource.AlreadyExists, $"product {request.Code}"));
            return null;
        }

        var product = new Entities.Product(
            request.EstablishmentId,
            request.Code,
            request.Name,
            request.Description,
            request.IsSignature
        );

        await repository.AddAsync(product, cancellationToken);
        var isSuccess = await repository.CommitAsync(cancellationToken);
        if (!isSuccess)
        {
            notifierMessage.Add(DomainValidationResource.ErrorOnCreate);
            return null;
        }

        return ProductModelOutput.FromEntity(product);
    }
}