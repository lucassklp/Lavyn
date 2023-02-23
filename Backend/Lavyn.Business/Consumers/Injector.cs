using Microsoft.Extensions.DependencyInjection;
using Rx.Http.Extensions;
using Rx.Http.Logging;

namespace Lavyn.Business.Consumers
{
    public static class Injector
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.UseRxHttp();
            services.AddRxHttpLogger<RxHttpDefaultLogger>();
            services.AddTransient<FirebaseMessagingConsumer>();

            return services;
        }
    }
}