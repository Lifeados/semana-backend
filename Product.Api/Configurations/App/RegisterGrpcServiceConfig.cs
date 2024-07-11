using Product.Api.Grpc.Services;

namespace Product.Api.Configurations.App;

public static class RegisterGrpcServiceConfig
{
    public static void RegisterGrpcService(this WebApplication app)
    {
        app.UseGrpcWeb(new GrpcWebOptions
        {
            DefaultEnabled = true
        });
        
        app.MapGrpcService<ProductServiceGrpc>();
    }
}