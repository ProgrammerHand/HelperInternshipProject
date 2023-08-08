using Microsoft.Extensions.Hosting;

namespace Helper.Infrastructure.DAL
{
    public sealed class DatabaseAutoMigration : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DatabaseAutoMigration(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
