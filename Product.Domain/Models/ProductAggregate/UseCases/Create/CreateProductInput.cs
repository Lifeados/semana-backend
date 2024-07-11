using MediatR;
using Product.Domain.Models.ProductAggregate.UseCases.Common;

namespace Product.Domain.Models.ProductAggregate.UseCases.Create;

public record CreateProductInput(
    int EstablishmentId,
    string Code,
    string Name,
    string Description,
    bool IsSignature
) :  IRequest<ProductModelOutput>;