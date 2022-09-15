using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PJ.Example.Abstractions.Services;
using PJ.Example.Abstractions.Services.JwtService;
using PJ.Example.Domain.Abstractions.Services;
using PJ.Example.Domain.Jwt;

namespace PJ.Example.Domain
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure(ConfigureJwtAuthenticationServicesAndOptions(services, configuration));

            return services.AddScoped<IJwtService, JwtService>()
                .AddScoped<ILoginService, LoginService>()
                .AddScoped<IAccountService, AccountService>();
        }

        private static Action<JwtOptions> ConfigureJwtAuthenticationServicesAndOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("JWT");
            return options => section.Bind(options);
        }
    }
}