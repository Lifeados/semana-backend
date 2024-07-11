using Product.Domain.Communication;

namespace Product.Api.Configurations.Mediator;

public static class MediatorConfig
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(MediatorHandler)));
    }
}
