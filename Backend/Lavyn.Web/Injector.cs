using Lavyn.Domain;
using Lavyn.Business;
using Lavyn.Web.Authentication;
using Lavyn.Web.Authentication.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lavyn.Web
{
    public static class Injector
    {
        public static void AddWebControllers(this IServiceCollection services)
        {
            services.AddBusiness();

            services.AddScoped<IAuthenticator<ICredential>, JwtAuthenticator>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
