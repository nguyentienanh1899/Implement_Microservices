using Contracts.Sagas.OrderManager;
using Saga.Orchestrator.HttpRepository;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Saga.Orchestrator.OrderManager;
using Saga.Orchestrator.Services;
using Saga.Orchestrator.Services.Interfaces;
using Shared.DTOs.Basket;

namespace Saga.Orchestrator.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IPaymentOrderService, PaymentOrder>();
            services.AddTransient<ISagaOrderManager<PaymentOrderDto, OrderResponse>, SagaOrderManager>();
            return services;
        }

        public static IServiceCollection ConfigureHttpRepository(this IServiceCollection services)
        {
            services.AddScoped<IOrderHttpRepository, OrderHttpRepository>()
                    .AddScoped<IInventoryHttpRepository, InventoryHttpRepository>()
                    .AddScoped<IBasketHttpRepository, BasketHttpRepository>();
            return services;
        }

        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            ConfigureOrderHttpClient(services);
            ConfigureBasketHttpClient(services);
            ConfigureInventoryHttpClient(services);
        }

        private static void ConfigureOrderHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<IOrderHttpRepository, OrderHttpRepository>("OrdersAPI", (serviceProvider, clientHttp) =>
            {
                clientHttp.BaseAddress = new Uri("http://localhost:5005/api/v1/");
            });

            services.AddScoped(serviceProvider => serviceProvider.GetService<IHttpClientFactory>().CreateClient("OrdersAPI"));
        }

        private static void ConfigureBasketHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<IBasketHttpRepository, BasketHttpRepository>("BasketsAPI", (serviceProvider, clientHttp) =>
            {
                clientHttp.BaseAddress = new Uri("http://localhost:5004/api/v1/");
            });

            services.AddScoped(serviceProvider => serviceProvider.GetService<IHttpClientFactory>().CreateClient("BasketsAPI"));
        }

        private static void ConfigureInventoryHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<IInventoryHttpRepository, InventoryHttpRepository>("InventoryAPI", (serviceProvider, clientHttp) =>
            {
                clientHttp.BaseAddress = new Uri("http://localhost:5006/api/v1/");
            });

            services.AddScoped(serviceProvider => serviceProvider.GetService<IHttpClientFactory>().CreateClient("InventoryAPI"));
        }
    }
}
