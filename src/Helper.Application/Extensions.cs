using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(typeof(ICommandHandler<>).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(s => s.FromAssemblies(typeof(IEventHandler<>).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

    }
}