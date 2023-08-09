using Helper.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Infrastructure.DAL
{
    internal static class Extension
    {
        private const string OptionsSectionName = "ConnectionStrings";

        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServerOptions>(configuration.GetRequiredSection(OptionsSectionName));
            var DbOptions = configuration.GetOptions<ServerOptions>(OptionsSectionName);
            services.AddDbContext<HelperDbContext>(x => x.UseSqlServer(DbOptions.ConnectionString));
            services.AddHostedService<DatabaseAutoMigration>();
            return services;
        }
    }
}
