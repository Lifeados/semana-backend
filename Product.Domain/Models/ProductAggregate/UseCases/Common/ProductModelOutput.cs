namespace Product.Domain.Models.ProductAggregate.UseCases.Common;

public record ProductModelOutput(
    int Id,
    int EstablishmentId,
    string Code,
    string Name,
    string Description,
    bool IsSignature,
    bool IsActive
)
{
    public static ProductModelOutput FromEntity(Entities.Product product)
    {
        return new ProductModelOutput(product.Id, product.EstablishmentId, product.Code, product.Name,
            product.Description, product.IsSignature, product.IsActive);
    }
}