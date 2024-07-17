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
            notifierMessage.Add(string.Format(DomainValidationResource.NotFound, "Product"));
            return false;
        }

        repository.Remove(product);

        if (!await repository.CommitAsync(cancellationToken))
        {
            notifierMessage.Add(DomainValidationResource.ErrorOnDelete);
            return false;
        }

        return true;
    }
}