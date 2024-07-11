using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Product.Api.Configurations.HealthCheck;

public static class HealthCheckConfig
{
    public static void AddHealthCheckConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("App")!, name: "DB SqlServer")
            .AddRedis(configuration.GetConnectionString("Redis")!, name: "DB Redis")
            .AddApplicationStatus("Api");

        services
            .AddHealthChecksUI(setup =>
            {
                setup.SetEvaluationTimeInSeconds(50);
            })
            .AddInMemoryStorage();
    }

    public static void UseHealthCheckConfiguration(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();

        endpoints.MapHealthChecks("/_HealthCheck", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        endpoints.MapHealthChecksUI(setup =>
        {
            setup.UIPath = "/_HealthCheck-ui";
        });
    }
}
