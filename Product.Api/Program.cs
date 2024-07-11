using Asp.Versioning.ApiExplorer;
using Product.Api.Configurations.App;
using Product.Api.Configurations.Cultures;
using Product.Api.Configurations.Database;
using Product.Api.Configurations.FluentValidation;
using Product.Api.Configurations.HealthCheck;
using Product.Api.Configurations.Mediator;
using Product.Api.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddAppServices();

builder.Services.AddFluentValidationValidators();

builder.Services.AddMediator();

builder.Services.AddLocalizationIStringLocalizer();

builder.Services.AddVersionedSwagger();

builder.Services.AddHealthCheckConfiguration(builder.Configuration);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddAllElasticApm();

builder.Services.AddGrpc(x =>
{
    x.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ApplyMigrations();

app.UseHttpsRedirection();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseVersionedSwagger(provider);

app.UseSupportedCultures(builder.Configuration);

app.UseHealthCheckConfiguration();

app.RegisterGrpcService();

if (app.Environment.IsEnvironment("Test"))
    app.MapControllers().AllowAnonymous();
else
    app.MapControllers();

app.Run();

public partial class Program;