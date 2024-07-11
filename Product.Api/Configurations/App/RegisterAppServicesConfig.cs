using Product.Api.Configurations.Elastic;
using Product.Data;
using Product.Domain.Communication;
using Product.Domain.Models.ProductAggregate.Queries;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.SeedWorks;
using Product.Domain.Validator;

namespace Product.Api.Configurations.App;

public static class RegisterAppServicesConfig
{
    public static void AddAppServices(this IServiceCollection services)
    {
        #region Data

        services.AddScoped<IRepository, DataContext>();
        services.AddScoped<IRepositoryCache, RedisContext>();

        #endregion

        #region Domain.Communication

        services.AddScoped<IMediatorHandler, MediatorHandler>();

        #endregion

        #region Domain.Models

        services.AddScoped<IProductQuery, ProductQuery>();

        #endregion

        #region Domain.Notifier

        services.AddScoped<INotifierMessage, NotifierMessage>();

        #endregion

        #region Domain.Validator

        services.AddScoped<IValidatorGeneric, ValidatorFactory>();

        #endregion

        #region Domain.Elastic

        services.AddScoped<IElasticTransaction, ElasticTransaction>();

        #endregion
    }
}