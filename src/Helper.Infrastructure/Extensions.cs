﻿using Helper.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Helper.Core.Inquiry;
using Helper.Infrastructure.DAL;
using Helper.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Helper.Infrastructure.DAL.Repositories;
using Helper.Core.User;
using Helper.Infrastructure.Security;
using Helper.Infrastructure.JWT;
using Helper.Infrastructure.Logging;
using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Infrastructure.Dispatchers;

namespace Helper.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSingleton<ExceptionMiddleware>();
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.AddHttpContextAccessor();
            services.AddSecurity();

            services.AddSingleton<IClockCustom, ClockCustom>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

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