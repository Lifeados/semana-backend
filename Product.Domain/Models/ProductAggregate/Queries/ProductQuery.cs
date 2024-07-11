using System.Text;
using System.Text.Json;
using Product.Domain.Models.ProductAggregate.Queries.Dtos;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.Resources.DomainValidation;

namespace Product.Domain.Models.ProductAggregate.Queries;

public class ProductQuery(
    IRepository repository,
    IRepositoryCache repositoryCache,
    INotifierMessage notifierMessage) : IProductQuery
{
    public IReadOnlyCollection<GetAllProductDto> GetAll(CancellationToken cancellationToken)
    {
        return repository.QueryAsNoTracking<Entities.Product>()
            .Where(x => x.IsActive)
            .Select(x => new GetAllProductDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            }).ToList();
    }

    public async Task<string> GetById(int productId)
    {
        var productCache = await repositoryCache.GetValueFromKey(productId.ToString(), 1);
        if (productCache is not null)
            return productCache;

        var product = repository.QueryAsNoTracking<Entities.Product>()
            .Select(x => new GetAllProductDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                IsSignature = x.IsSignature
            })
            .FirstOrDefault(x => x.Id == productId);
        if (product is null)
        {
            notifierMessage.Add(string.Format(DomainValidationResource.NotFound, "Product"));
            return null;
        }
        
        await SaveProductDtoCache(product);
        return "";
    }

    #region Private methos

    private GetAllProductDto GetProductByString(string payload)
    {
        try
        {
            return JsonSerializer.Deserialize<GetAllProductDto>(payload);
        }
        catch
        {
            return null;
        }
    }

    private async Task SaveProductDtoCache(GetAllProductDto getAllProductDto)
    {
        await repositoryCache.SetValueKey(getAllProductDto.Id.ToString(),
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(getAllProductDto)), 1);
    }

    #endregion
}