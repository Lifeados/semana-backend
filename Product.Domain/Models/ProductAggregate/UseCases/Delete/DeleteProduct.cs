using MediatR;
using Microsoft.VisualBasic;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.Resources.DomainValidation;

namespace Product.Domain.Models.ProductAggregate.UseCases.Delete;

public class DeleteProduct(IRepository repository, INotifierMessage notifierMessage)
    : IRequestHandler<DeleteProductInput, bool>
{
    public async Task<bool> Handle(DeleteProductInput request, CancellationToken cancellationToken)
    {
        var product = repository.Query<Entities.Product>()
            .FirstOrDefault(x => x.Id == request.ProductId && x.EstablishmentId == request.EstablishmentId);

        if (product is null)
        {
            notifierMessage.Add(Strings.Format(DomainValidationResource.NotFound, "Product"));
            return false;
        }

        repository.Remove(product);

        var isSuccess = await repository.CommitAsync(cancellationToken);
        if (!isSuccess)
        {
            notifierMessage.Add(DomainValidationResource.ErrorOnDelete);
            return false;
        }

        return true;
    }
}