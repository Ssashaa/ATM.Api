using ATM.Api.Services;
using ATM.Api.Service.Interfaces;

namespace ATM.Api.Configuration
{
    public static class ServicesCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IAtmService, AtmService>();
        }
    }
}
