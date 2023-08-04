using Helper.Application.Abstractions;

namespace Helper.Application.Commands.Handlers
{
    public sealed class AcceptInquiryHandler : ICommandHandler<AcceptInquiry>
    {
        public async Task HandleAsync(AcceptInquiry command)
        {
            //TODO: Repository interaction 
        }
    }
}
