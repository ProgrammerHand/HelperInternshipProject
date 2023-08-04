using Helper.Application.Abstractions;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
namespace Helper.Application.Commands.Handlers
{
    public sealed class SetFeasibilitNoteHandler : ICommandHandler<SetFeasibilityNote>
    {
        public async Task HandleAsync(SetFeasibilityNote command)
        {
            var entity = Inquiry.CreateInquiry(null, null, null);
            entity.SetFeasibilityNote(new FeasibilityNote(command.Body)); 
            //TODO: Repository interaction 
        }
    }
}
