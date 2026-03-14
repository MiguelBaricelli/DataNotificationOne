using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Interfaces.Infra.Repository;
using DataNotificationOne.Infrastructure.ExternalApis;
using DataNotificationOne.Infrastructure.ExternalApis.Brapi;
using DataNotificationOne.Infrastructure.ExternalApis.Email;
using DataNotificationOne.Infrastructure.Redis;
using DataNotificationOne.Infrastructure.Repository;
using DataNotificationOne.Infrastructure.Repository.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DataNotificationOne.Infrastructure.DependencyInjection
{
    public static class Installer
    {

        public static IServiceCollection AddDependencyInjection(
        this IServiceCollection services, IConfiguration configuration)
        {
            // Outros serviços
            services.AddScoped<IAlphaVantageDailyConsumer, AlphaVantageDailyConsumer>();
            services.AddScoped<IAlphaVantageWeeklyConsumer, AlphaVantageWeeklyConsumer>();
            services.AddScoped<IAlphaVantageOverviewConsumer, AlphaVantageOverviewConsumer>();
            services.AddScoped<IAlphaVantageGeneralConsumer, AlphaVantageGeneralConsumer>();
            services.AddScoped<ISendGridIntegration, SendGridIntegration>();
            services.AddScoped<IBrApiIntegrationConsumer, BrApiIntegrationConsumer>();
            services.AddScoped<IBrApiRepository, BrApiRepository>();
            

            // Redis
            var connectionString = configuration.GetConnectionString("RedisConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("String de conexão com Redis não encontrada");

            // Singleton: one shared connection across the app lifetime
            services.AddSingleton<IRedisIntegration>(sp =>
                new RedisIntegration(connectionString));

            // RedisRepository now resolves IRedisIntegration cleanly — no string needed
            services.AddScoped<ICacheRepository, RedisRepository>();

            return services;
        }
    }
  }

