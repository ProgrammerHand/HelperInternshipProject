using Helper.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Commands.Handlers
{
    public sealed class SetFeasibilitNoteHandler : ICommandHandler<FeasibilityNote>
    {
        public async Task HandleAsync(FeasibilityNote command)
        {
            //TODO: Repository interaction 
        }
    }
}
