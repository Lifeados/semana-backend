using MediatR;
using Product.Domain.Models.EstablishmentAggregate.UseCases.Common;

namespace Product.Domain.Models.EstablishmentAggregate.UseCases.Create;

public record CreateEstablishmentInput(string Name) : IRequest<EstablishmentModelOutput>;
