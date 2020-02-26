using System;
using Microsoft.Extensions.DependencyInjection;
using Rx.Http;
using Rx.Http.Extensions;

namespace Lavyn.Business.Consumers
{
    public static class Injector
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddHttpClient<RxHttpClient>();
            services.AddRxHttpLogging<RxHttpDefaultLogging>();
            
            services.AddConsumer<FirebaseMessagingConsumer>(http =>
            {
                http.BaseAddress = new Uri(@"https://fcm.googleapis.com/");
            });
            
            return services;
        }
    }
}