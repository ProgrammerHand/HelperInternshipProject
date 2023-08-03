using Helper.Application.Abstractions;

namespace Helper.Application.Commands.Handlers
{
    public sealed class RejectInquiryHandler : ICommandHandler<RejectInquiry>
    {
        public async Task HandleAsync(RejectInquiry command)
        {
            //TODO: Repository interaction 
        }
    }
}
