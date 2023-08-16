using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Helper.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Tests.Integration
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<HelperDbContext>));

                        services.Remove(descriptor);
                        services.AddDbContext<HelperDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        });
                    });
                });
            var httpClient = appFactory.CreateClient();
        }
    }
}