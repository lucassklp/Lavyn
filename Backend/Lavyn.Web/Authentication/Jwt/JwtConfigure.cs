using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Lavyn.Web.Authentication.Jwt
{
    public static class JwtConfigure
    {
        public static void ConfigureJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build();
            });
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = JwtTokenDefinitions.IssuerSigningKey,
                    ValidAudience = JwtTokenDefinitions.Audience,
                    ValidIssuer = JwtTokenDefinitions.Issuer,
                    ValidateIssuerSigningKey = JwtTokenDefinitions.ValidateIssuerSigningKey,
                    ValidateLifetime = JwtTokenDefinitions.ValidateLifetime,
                    ClockSkew = JwtTokenDefinitions.ClockSkew
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chat")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

            });
        }
    }
}
