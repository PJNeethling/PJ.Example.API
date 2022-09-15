﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PJ.Example.Database.Abstractions;

namespace PJ.Example.Database.EF
{
    public static class ConfigureEFServices
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbOptions = new DbOptions();
            DbOption(configuration).Invoke(dbOptions);

            return services.AddDbContext<IDatabase, Database>(options => options.UseSqlServer(dbOptions.DbConnectionString), ServiceLifetime.Scoped);
        }

        private static Action<DbOptions> DbOption(IConfiguration configuration)
        {
            return options =>
            {
                configuration.GetSection("DbOptions").Bind(options);
            };
        }
    }
}