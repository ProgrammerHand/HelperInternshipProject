using Helper.Application.Abstraction.Commands;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace Helper.Infrastructure.Logging.Decorators
{
    internal sealed class LoggingCommandDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        private readonly ILogger<LoggingCommandDecorator<TCommand>> _logger;
        private readonly ICommandHandler<TCommand> _commandHandler;

        public LoggingCommandDecorator(ICommandHandler<TCommand> commandHandler, ILogger<LoggingCommandDecorator<TCommand>> logger)
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        public async Task HandleAsync (TCommand command) 
        {
            var commandName = typeof(TCommand).Name;
            _logger.LogInformation("Started handling a command: {CommandName}...", commandName);
            await _commandHandler.HandleAsync(command);
            _logger.LogInformation("Ended handling a command: {CommandName}...", commandName);
        }
    }
}
