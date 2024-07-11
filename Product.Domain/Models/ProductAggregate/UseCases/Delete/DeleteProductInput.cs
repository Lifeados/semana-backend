using MediatR;

namespace Product.Domain.Models.ProductAggregate.UseCases.Delete;

public record DeleteProductInput(int ProductId, int EstablishmentId) : IRequest<bool>;