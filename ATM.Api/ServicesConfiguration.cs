using ATM.Api.Service.Interfaces;
using ATM.Api.Services;

namespace ATM.Api
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICardService, CardService>();
        }
    }
}
