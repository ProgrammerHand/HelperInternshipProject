using Helper.Application.Exceptions;
using Helper.Application.User.Commands;
using Helper.Infrastructure.DAL;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Json;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Helper.Tests.Integration
{
    //public class AuthenticationTests
    //{
    //    private readonly WebApplicationFactory<Program> appFactory;
    //    private readonly HttpClient httpClient;

    //    public AuthenticationTests()
    //    {
    //        appFactory = new WebApplicationFactory<Program>()
    //            .WithWebHostBuilder(host =>
    //            {
    //                host.ConfigureServices(services =>
    //                {
    //                    var descriptor = services.SingleOrDefault(
    //                        d => d.ServiceType ==
    //                        typeof(DbContextOptions<HelperDbContext>));

    //                    services.Remove(descriptor);
    //                    services.AddDbContext<HelperDbContext>(options =>
    //                    {
    //                        options.UseInMemoryDatabase("InMemoryDB");
    //                    });
    //                });
    //            });
    //        httpClient = appFactory.CreateClient();
    //    }

    //    [Fact]
    //    public async Task Given_email_and_password_when_not_registered_should_register_user_1()
    //    {
    //        //Arrange
    //        var command = new RegisterUser("string", "string");

    //        //Act
    //        var exception = await Record.ExceptionAsync(async () => await httpClient.PostAsJsonAsync("api/Security/register", command));
    //        var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
    //        var dbContext = scope.ServiceProvider.GetService<HelperDbContext>();

    //        //Assert
    //        dbContext.ShouldNotBeNull();
    //        dbContext.Users.Count().ShouldBe(1);
    //        (await dbContext.Users.Where(x => x.Email == "string").FirstOrDefaultAsync()).ShouldNotBeNull();
    //        await dbContext.Database.EnsureDeletedAsync();
    //    }

    //    public record Error(string Code, string Reason);

    //    [Fact]
    //    public async Task Given_email_and_password_when_registered_should_not_register_user2()
    //    {
    //        //Arrange

    //        var command = new RegisterUser("string", "string");
    //        await httpClient.PostAsJsonAsync("api/Security/register", command);

    //        //Act
    //        var response = await httpClient.PostAsJsonAsync("api/Security/register", command);
    //        var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
    //        var dbContext = scope.ServiceProvider.GetService<HelperDbContext>();
    //        var resultMessage = await response.Content.ReadAsStringAsync();
    //        var errorName = JsonConvert.DeserializeObject<Error>(resultMessage);

    //        //Assert
    //        dbContext.ShouldNotBeNull();
    //        (await dbContext.Users.Where(x => x.Email == "string").FirstOrDefaultAsync()).ShouldNotBeNull();

    //        errorName.Code.ShouldBe(new UserAlredyExistException().GetType().Name);
    //        await dbContext.Database.EnsureDeletedAsync();
    //    }
    //}
}