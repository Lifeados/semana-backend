using MediatR;
using Product.Domain.Models.EstablishmentAggregate.Entities;
using Product.Domain.Models.EstablishmentAggregate.UseCases.Common;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.Resources.DomainValidation;

namespace Product.Domain.Models.EstablishmentAggregate.UseCases.Update;

public class UpdateEstablishment(IRepository repository, INotifierMessage notifierMessage) : IRequestHandler<UpdateEstablishmentInput, EstablishmentModelOutput>
{
    public async Task<EstablishmentModelOutput> Handle(UpdateEstablishmentInput request, CancellationToken cancellationToken)
    {
        var establishment = repository.Query<Establishment>()
            .FirstOrDefault(x => x.Id == request.Id);

        if (establishment is null)
        {
            notifierMessage.Add(string.Format(DomainValidationResource.NotFound, "Establishment"));
            return null;
        }
        
        establishment.Update(request.Name);

        if (!await repository.CommitAsync(cancellationToken))
        {
            notifierMessage.Add(string.Format(DomainValidationResource.ErrorOnUpdate));
            return null;
        }

        return EstablishmentModelOutput.FromEntity(establishment);
    }
}