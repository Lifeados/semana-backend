using MediatR;
using Product.Domain.Models.ProductAggregate.UseCases.Common;

namespace Product.Domain.Models.ProductAggregate.UseCases.Update;

public record UpdateProductInput(
    int Id,
    int EstablishmentId,
    string Code,
    string Name,
    string Description,
    bool IsSignature
) :  IRequest<ProductModelOutput>;