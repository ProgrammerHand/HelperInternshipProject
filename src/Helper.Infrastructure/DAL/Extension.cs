using Helper.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Infrastructure.DAL
{
    internal static class Extension
    {
        private const string OptionsSectionName = "msServer";

        public static IServiceCollection AddLocalDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MSServerOptions>(configuration.GetRequiredSection(OptionsSectionName));
            var localDbOptions = configuration.GetOptions<MSServerOptions>(OptionsSectionName);
            services.AddDbContext<HelperDbContext>(x => x.UseSqlServer(localDbOptions.ConnectionString));
            services.AddHostedService<DatabaseAutoMigration>();
            return services;
        }
    }
}
