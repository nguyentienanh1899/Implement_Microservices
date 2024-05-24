using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using EventBus.Messages.IntegrationEvents.Interfaces;
using Infrastructure.Common;
using Infrastructure.Configurations;
using Infrastructure.Extensions;
using Inventory.Grpc.Protos;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Configurations;

namespace Basket.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services.AddScoped<IBasketRepository, BasketRepository>()
            .AddTransient<ISerializeService, SerializeService>();

        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusSettings = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();
            services.AddSingleton(eventBusSettings);

            var cacheSettings = configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>();
            services.AddSingleton(eventBusSettings);

            var grpcSettings = configuration.GetSection(nameof(GrpcSettings)).Get<GrpcSettings>();
            services.AddSingleton(grpcSettings);

            return services;
        }
        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.GetOptions<CacheSettings>("CacheSettings");
            if(string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentException("Redis Conection string is not configured.");
            }

            //Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = settings.ConnectionString;
            });
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
            if(settings == null || string.IsNullOrEmpty(settings.HostAddress)) {
                throw new ArgumentNullException("EventBusSettings is not configures.");
            }

            var mqConnection = new Uri(settings.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(cf =>
            {
                cf.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                });

                //Publish submit order message
                cf.AddRequestClient<IBasketCheckoutEvent>();
            });
        }

        public static IServiceCollection ConfigureGrpcServices(this IServiceCollection services)
        {
            var settings = services.GetOptions<GrpcSettings>(nameof(GrpcSettings));
            services.AddGrpcClient<StockProtoService.StockProtoServiceClient>(x=>x.Address = new Uri(settings.StockUrl));
            services.AddScoped<StockItemGrpcService>();
            return services;
        }
    }
}
