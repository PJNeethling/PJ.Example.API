using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PJ.Example.Abstractions.Repositories;
using PJ.Example.Repository.Password_Manager;
using PJ.Example.Repository.Password_Manager.Infrastructure;

namespace PJ.Example.Repository
{
    public static class ConfigureRepositoryServices
    {
        public static IServiceCollection ConfigureRepository(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure(ConfigureEncryptionOptions(configuration))
                .AddScoped<ILoginRepository, LoginRepository>()
                .AddScoped<IPassword, Password>()
                .AddScoped<IAccountRepository, AccountRepository>();
        }

        private static Action<EncryptionOptions> ConfigureEncryptionOptions(IConfiguration configuration)
        {
            return options =>
            {
                var section = configuration.GetSection("EncryptionOptions");
                section.Bind(options);
            };
        }
    }
}