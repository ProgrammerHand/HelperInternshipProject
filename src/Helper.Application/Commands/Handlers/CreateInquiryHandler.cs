using Helper.Application.Abstractions;
using Helper.Application.Commands;

namespace Helper.Application.Commands.Handlers
{
    public sealed class CreateInquiryHandler : ICommandHandler<CreateInquiry>
    {
        public async Task HandleAsync(CreateInquiry command)
        {
            //TODO: Repository interaction 
        }
    }
}
