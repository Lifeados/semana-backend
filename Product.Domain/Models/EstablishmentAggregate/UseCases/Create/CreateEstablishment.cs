using MediatR;
using Product.Domain.Models.EstablishmentAggregate.Entities;
using Product.Domain.Models.EstablishmentAggregate.UseCases.Common;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.Resources.DomainValidation;

namespace Product.Domain.Models.EstablishmentAggregate.UseCases.Create;

public class CreateEstablishment(IRepository repository, INotifierMessage notifierMessage)
    : IRequestHandler<CreateEstablishmentInput, EstablishmentModelOutput>
{
    public async Task<EstablishmentModelOutput> Handle(CreateEstablishmentInput request, CancellationToken cancellationToken)
    {
        var establishment = new Establishment(request.Name);

        await repository.AddAsync(establishment, cancellationToken);

        if (!await repository.CommitAsync(cancellationToken))
        {
            notifierMessage.Add(string.Format(DomainValidationResource.ErrorOnCreate, "establishment"));
            return null;
        }

        return EstablishmentModelOutput.FromEntity(establishment);
    }
}