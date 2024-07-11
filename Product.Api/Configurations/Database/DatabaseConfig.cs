using Microsoft.EntityFrameworkCore;
using Product.Data;

namespace Product.Api.Configurations.Database;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("App");
        services.AddDbContext<DataContext>(
            builder => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        var dataContext = services.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}
