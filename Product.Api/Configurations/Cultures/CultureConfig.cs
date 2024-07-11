namespace Product.Api.Configurations.Cultures;

public static class CultureConfig
{
    public static void UseSupportedCultures(this IApplicationBuilder app, IConfiguration configuration)
    {
        var supportedCultures = configuration.GetSection("SupportedCultures").Get<SupportedCultures>();
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures.Cultures[0])
            .AddSupportedCultures(supportedCultures.Cultures)
            .AddSupportedUICultures(supportedCultures.Cultures);

        app.UseRequestLocalization(localizationOptions);
    }

    public static void AddLocalizationIStringLocalizer(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "");
    }
}
