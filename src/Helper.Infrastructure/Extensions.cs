using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Events;
using Helper.Application.Abstraction.Queries;
using Helper.Application.Integrations;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.Solution;
using Helper.Core.User;
using Helper.Core.Utility;
using Helper.Infrastructure;
using Helper.Infrastructure.DAL;
using Helper.Infrastructure.DAL.Repositories;
using Helper.Infrastructure.Dispatchers;
using Helper.Infrastructure.Exceptions;
using Helper.Infrastructure.Integrations;
using Helper.Infrastructure.JWT;
using Helper.Infrastructure.Logging;
using Helper.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<DatabaseAutoMigration>();
            services.AddHostedService<BackgroundRabbitMQ>();
            services.AddControllers();
            services.AddSingleton<ExceptionMiddleware>();;
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSecurity();
            services.AddSingleton<IClockCustom, ClockCustom>(); 
            //services.AddScoped<IPdfGenerator, PdfGenerator>();
            services.AddScoped<IMailSendingClient, MailSendingSmtp>();
            services.AddScoped<IGoogleDriveClient, GoogleDriveClient>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISolutionRepository, SolutionRepository>();
            services.AddScoped<IRabbitMqClient, RabbitMqClient>();
            services.AddDb(configuration);

            var infrastructureAssembly = typeof(AppOptions).Assembly;

            services.Scan(s => s.FromAssemblies(infrastructureAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddAuth(configuration);
            services.AddCustomCommandLogging();

            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            //app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger();
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "MySpot API";
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}