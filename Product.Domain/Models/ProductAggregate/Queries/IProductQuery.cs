using Product.Domain.Models.ProductAggregate.Queries.Dtos;

namespace Product.Domain.Models.ProductAggregate.Queries;

public interface IProductQuery
{
    Task<string> GetById(int product);
    IReadOnlyCollection<GetAllProductDto> GetAll(CancellationToken cancellationToken);
}