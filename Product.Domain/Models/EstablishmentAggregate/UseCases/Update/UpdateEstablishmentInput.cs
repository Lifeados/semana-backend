using MediatR;
using Product.Domain.Models.EstablishmentAggregate.UseCases.Common;

namespace Product.Domain.Models.EstablishmentAggregate.UseCases.Update;

public record UpdateEstablishmentInput(int Id, string Name) : IRequest<EstablishmentModelOutput>;