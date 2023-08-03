using Helper.Application.Abstractions;

namespace Helper.Application.Commands.Handlers
{
    public sealed class RejectInquiryHandler : ICommandHandler<RejectInquiry>
    {
        public async Task HandleAsync(RejectInquiry command)
        {
            //TODO: Repository interaction 

            if (command.decision is false)
                throw new ArgumentException($"{command.inquiriId} is rejected");
        }
    }
}
