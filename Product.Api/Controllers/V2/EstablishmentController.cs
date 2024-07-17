using System.Net;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Controllers.V2.Dtos;
using Product.Api.Dtos;
using Product.Domain.Models.EstablishmentAggregate.Entities;
using Product.Domain.Models.EstablishmentAggregate.UseCases.Create;
using Product.Domain.Models.EstablishmentAggregate.UseCases.Update;
using Product.Domain.Notifier;
using Product.Domain.Repository;

namespace Product.Api.Controllers.V2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/establishments")]
public class EstablishmentController(IRepository repository, INotifierMessage notifierMessage, ISender sender)
    : ProductBaseController(notifierMessage)
{
    /// <summary>
    ///     Retorna um estabelecimento pelo id
    /// </summary>
    /// <param name="establishmentId"></param>
    /// <returns></returns>
    [HttpGet("{establishmentId:int}")]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.BadRequest)]
    public IActionResult GetAll(int establishmentId)
    {
        var establishment = repository.QueryAsNoTracking<Establishment>()
            .FirstOrDefault(x => x.Id == establishmentId);

        if (establishment is null)
        {
            notifierMessage.Add(string.Format("{0} not found", "establishment"));
            return CustomResponse(HttpStatusCode.BadRequest, null);
        }

        return CustomResponse(HttpStatusCode.OK, establishment);
    } 

    /// <summary>
    ///     Retorna todos os estabelecimentos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.BadRequest)]
    public IActionResult GetAll()
    {
        var establishments = repository.QueryAsNoTracking<Establishment>()
            .Where(x => x.IsActive)
            .ToList();

        return establishments.Count > 0
            ? CustomResponse(HttpStatusCode.OK, establishments)
            : CustomResponse(HttpStatusCode.NoContent, null);
    }

    /// <summary>
    ///     Criar um estabelecimento
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEstablishmentInput input,
        CancellationToken cancellationToken)
    {
        var output = await sender.Send(input, cancellationToken);
        return CustomResponse(HttpStatusCode.Created, output);
    }

    /// <summary>
    ///     Atualiza um estabelecimento
    /// </summary>
    /// <param name="establishmentId"></param>
    /// <param name="updateEstablishmentDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{establishmentId:int}")]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create(int establishmentId,
        [FromBody] UpdateEstablishmentDto updateEstablishmentDto, CancellationToken cancellationToken)
    {
        var input = new UpdateEstablishmentInput(establishmentId, updateEstablishmentDto.Name);
        var output = await sender.Send(input, cancellationToken);
        return CustomResponse(HttpStatusCode.OK, output);
    }
}