using DataNotificationOne.Infrastructure.ExternalApis;
using DataNotificationOne.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataNotificationOne.Infrastructure.DependencyInjection
{
    public static class Installer
    {

        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services)
        {
            
            services.AddScoped<IAlphaVantageClient, AlphaVantageClient>();


            return services;
        }
    }
}
