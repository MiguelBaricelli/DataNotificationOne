using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Infrastructure.ExternalApis;
using DataNotificationOne.Infrastructure.ExternalApis.Email;
using Microsoft.Extensions.DependencyInjection;

namespace DataNotificationOne.Infrastructure.DependencyInjection
{
    public static class Installer
    {

        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services)
        {
            
            services.AddScoped<IAlphaVantageDailyConsumer, AlphaVantageDailyConsumer>();
            services.AddScoped<IAlphaVantageWeeklyConsumer, AlphaVantageWeeklyConsumer>();
            services.AddScoped<IAlphaVantageOverviewConsumer, AlphaVantageOverviewConsumer>();
            services.AddScoped<IAlphaVantageGeneralConsumer, AlphaVantageGeneralConsumer>();
            services.AddScoped<ISendGridIntegration, SendGridIntegration>();


            return services;
        }
    }
}
