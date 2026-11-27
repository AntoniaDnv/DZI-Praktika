using DziPraktika.Services.Fakes;

namespace DziPraktika.Services;

public static class ServiceRegistration
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        var useFake = configuration.GetValue<bool>("UseFakeServices");
        CurrentUser.DevFallbackEnabled = useFake;

        services.AddSingleton<FakeStore>();

        return services;
    }
}
