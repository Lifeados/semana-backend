using Product.Domain.Models.EstablishmentAggregate.Entities;

namespace Product.Domain.Models.EstablishmentAggregate.UseCases.Common;

public record EstablishmentModelOutput(int Id, string Name, bool IsActive)
{
    public static EstablishmentModelOutput FromEntity(Establishment establishment)
    {
        return new EstablishmentModelOutput(establishment.Id, establishment.Name, establishment.IsActive);
    }
}