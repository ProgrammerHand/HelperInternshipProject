using Helper.Application.Abstractions;

namespace Helper.Application.Abstraction
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
