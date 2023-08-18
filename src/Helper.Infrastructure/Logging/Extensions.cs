using Helper.Application.Abstraction.Commands;
using Helper.Infrastructure.Logging.Decorators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Infrastructure.Logging
{
    internal static class Extensions
    {
         public static IServiceCollection AddCustomCommandLogging(this IServiceCollection services) 
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandDecorator<>));
            return services;
        }
    }
}
