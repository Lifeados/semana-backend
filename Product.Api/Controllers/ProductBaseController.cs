using System.Net;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Dtos;
using Product.Domain.Notifier;

namespace Product.Api.Controllers;

[ApiController]
public abstract class ProductBaseController(INotifierMessage notifierMessage) : ControllerBase
{
    protected ActionResult CustomResponse(HttpStatusCode httpStatusCode, object data)
    {
        if (!notifierMessage.IsValid())
        {
            var messages = notifierMessage.GetMessages().ToArray();
            var resultFail = new CustomResponse
            {
                Success = false,
                Status = (int)HttpStatusCode.BadRequest,
                Data = null,
                Messages = messages
            };
            return BadRequest(resultFail);
        }

        var resultSuccess = new CustomResponse
        {
            Success = true,
            Status = (int)httpStatusCode,
            Data = data,
            Messages = null
        };

        return httpStatusCode switch
        {
            HttpStatusCode.Created => Created(string.Empty, resultSuccess),
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.OK => Ok(resultSuccess),
            _ => Ok(),
        };
    }
}