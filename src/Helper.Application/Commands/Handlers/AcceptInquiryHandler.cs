using Helper.Application.Abstractions;
using System;
using Helper.Application.Abstractions;
using Helper.Application.Commands;

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
