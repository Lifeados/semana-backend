using System.Net;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Api.Controllers.V1.Dtos;
using Product.Domain.Models.ProductAggregate.UseCases.Create;
using Product.Domain.Models.ProductAggregate.UseCases.Delete;
using Product.Domain.Models.ProductAggregate.UseCases.Update;
using Product.Domain.Notifier;
using Product.Domain.Repository;

namespace Product.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductController(IRepository repository, INotifierMessage notifierMessage, ISender sender)
    : ProductBaseController(notifierMessage)
{
    /// <summary>
    /// Retorna um produto especifico
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="establishmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetById(int productId, [FromHeader] int establishmentId,
        CancellationToken cancellationToken)
    {
        if (establishmentId == 0)
        {
            notifierMessage.Add("Informe o establishmentId");
            return CustomResponse(HttpStatusCode.BadRequest, null);
        }

        var product = await repository.QueryAsNoTracking<Domain.Models.ProductAggregate.Entities.Product>()
            .FirstOrDefaultAsync(x => x.Id == productId && x.EstablishmentId == establishmentId, cancellationToken);

        return product is not null
            ? CustomResponse(HttpStatusCode.OK, product)
            : CustomResponse(HttpStatusCode.NotFound, null);
    }

    /// <summary>
    /// Retorna todos os produtos
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="skipCount"></param>
    /// <param name="establishmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageSize, [FromQuery] int skipCount,
        [FromHeader] int establishmentId, CancellationToken cancellationToken)
    {
        if (establishmentId == 0)
        {
            notifierMessage.Add("Informe o establishmentId");
            return CustomResponse(HttpStatusCode.BadRequest, null);
        }

        var take = pageSize > 0 ? pageSize : 10;
        var skip = skipCount > 0 ? skipCount : 0;

        var products = await repository.QueryAsNoTracking<Domain.Models.ProductAggregate.Entities.Product>()
            .Where(x => x.EstablishmentId == establishmentId)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return products.Count > 0
            ? CustomResponse(HttpStatusCode.OK, products)
            : CustomResponse(HttpStatusCode.NoContent, null);
    }

    /// <summary>
    /// Cria um produto
    /// </summary>
    /// <param name="createProductDto"></param>
    /// <param name="establishmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto,
        [FromHeader] int establishmentId, CancellationToken cancellationToken)
    {
        var input = new CreateProductInput(
            establishmentId,
            createProductDto.Code,
            createProductDto.Name,
            createProductDto.Description,
            createProductDto.IsSignature
        );
        var output = await sender.Send(input, cancellationToken);
        return output is not null
            ? CustomResponse(HttpStatusCode.Created, output)
            : CustomResponse(HttpStatusCode.UnprocessableEntity, null);
    }

    /// <summary>
    /// Atualiza um produto
    /// </summary>
    /// <param name="updateProductDto"></param>
    /// <param name="establishmentId"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto updateProductDto,
        [FromHeader] int establishmentId, int id, CancellationToken cancellationToken)
    {
        var input = new UpdateProductInput(
            id,
            establishmentId,
            updateProductDto.Code,
            updateProductDto.Name,
            updateProductDto.Description,
            updateProductDto.IsSignature
        );
        var output = await sender.Send(input, cancellationToken);
        return output is not null
            ? CustomResponse(HttpStatusCode.OK, output)
            : CustomResponse(HttpStatusCode.UnprocessableEntity, null);
    }

    /// <summary>
    /// Remove um produto
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="establishmentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> Delete(int productId, [FromHeader] int establishmentId,
        CancellationToken cancellationToken)
    {
        var deleteProductInput = new DeleteProductInput(productId, establishmentId);
        var isSuccess = await sender.Send(deleteProductInput, cancellationToken);
        return isSuccess
            ? CustomResponse(HttpStatusCode.OK, null)
            : CustomResponse(HttpStatusCode.UnprocessableEntity, null);
    }
}