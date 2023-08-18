using Helper.Core;
using Helper.Core.Exceptions;
using Helper.Core.Inquiry.Exceptions;

namespace Helper.Application.Inquiry.Commands
{
    public static class InquiryParamsValidator
    {
        public static void Validate(CreateInquiry command, IClockCustom clock)
        {
            if (string.IsNullOrEmpty(command.Description) || string.IsNullOrWhiteSpace(command.Description))
                throw new NoDescriptionGivenException();
            if (command.Start <= clock.Now)
                throw new StartDateInPastException();
            if (command.End < command.Start)
                throw new EndBeforeStartException();
            if (command.Start < clock.Now.AddDays(7))
                throw new StartDateTooEarly();
        }
    }
}
