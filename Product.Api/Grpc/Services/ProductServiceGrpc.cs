using Grpc.Core;
using Product.Api.Protos;

namespace Product.Api.Grpc.Services;

public class ProductServiceGrpc : ProductProtoService.ProductProtoServiceBase
{
    public override Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetByIdResponse
        {
            Id = 1,
            Code = "Code",
            Name = "name",
            IsSignature = true
        });
    }
}