using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Infrastructure.ExternalApis;
using Microsoft.Extensions.DependencyInjection;

namespace DataNotificationOne.Infrastructure.DependencyInjection
{
    public static class Installer
    {

        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services)
        {
            
            services.AddScoped<IAlphaVantageDailyConsumer, ExternalApis.AlphaVantageDailyConsumer>();


            return services;
        }
    }
}
