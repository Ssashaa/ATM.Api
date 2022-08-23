using ATM.Api.Services;
using ATM.Api.Services.Interfaces;

namespace ATM.Api.Configuration;

public static class ServicesCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IAtmService, AtmService>();
        services.AddSingleton<IBankService, BankService>();
        services.AddSingleton<IAtmEventBroker, AtmEventBroker>();
    }
}
