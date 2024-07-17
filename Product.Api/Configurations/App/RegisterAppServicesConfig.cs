using Product.Data;
using Product.Domain.Communication;
using Product.Domain.Notifier;
using Product.Domain.Repository;
using Product.Domain.Validator;

namespace Product.Api.Configurations.App;

public static class RegisterAppServicesConfig
{
    public static void AddAppServices(this IServiceCollection services)
    {
        #region Data

        services.AddScoped<IRepository, DataContext>();

        #endregion

        #region Domain.Communication

        services.AddScoped<IMediatorHandler, MediatorHandler>();

        #endregion

        #region Domain.Models
        

        #endregion

        #region Domain.Notifier

        services.AddScoped<INotifierMessage, NotifierMessage>();

        #endregion

        #region Domain.Validator

        services.AddScoped<IValidatorGeneric, ValidatorFactory>();

        #endregion
    }
}