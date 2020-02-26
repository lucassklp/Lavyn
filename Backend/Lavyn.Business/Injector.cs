using Lavyn.Business.Mapping;
using Lavyn.Business.Providers;
using Microsoft.Extensions.DependencyInjection;
using Lavyn.Persistence.Repository;
using Lavyn.Business.Validation;

namespace Lavyn.Business
{
    public static class Injector
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddValidators();
            services.AddMappers();
            services.AddTransient<LoginServices>();
            services.AddTransient<UserServices>();
            services.AddTransient<RoomServices>();

            services.AddTransient<AuthenticatedUserProvider>();
            services.AddTransient(x => x.GetService<AuthenticatedUserProvider>().GetAuthenticatedUser());

            
            return services;
        }
    }
}
