using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HelperDbContext>();
            if (dbContext.Database.IsRelational())
                await dbContext.Database.MigrateAsync(cancellationToken);
        }
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
    
}
