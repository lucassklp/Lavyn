using Lavyn.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Lavyn.Persistence.Repository
{
    public static class Injector
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddPersistence();

            services.AddScoped(typeof(Crud<,>));
            services.AddScoped<TransactionScope>();

            services.AddTransient<AbstractRepository<User>, UserRepository>();
            services.AddTransient<AbstractRepository<Room>, RoomRepository>();
            services.AddTransient<AbstractRepository<Message>, MessageRepository>();


            services.AddTransient<UserRepository>();
            services.AddTransient<RoomRepository>();
            services.AddTransient<MessageRepository>();

            return services;
        }
    }
}
