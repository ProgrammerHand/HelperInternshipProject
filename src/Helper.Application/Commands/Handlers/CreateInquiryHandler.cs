using Helper.Application.Abstractions;
using Helper.Core;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Application.Commands.Handlers
{
    public sealed class CreateInquiryHandler : ICommandHandler<CreateInquiry>
    {

        private readonly IClockCustom _clock;
        private readonly IInquiryRepository _inquiryRepo;

        public CreateInquiryHandler(IClockCustom clock, IInquiryRepository inquiryRepo)
        {
            _clock = clock;
            _inquiryRepo = inquiryRepo;
        }


        public async Task HandleAsync(CreateInquiry command)
        {
            InquiryParamsValidator.Validate(command, _clock);

            var inquiry = Inquiry.CreateInquiry(new Description(command.Description),
                new RealisationDate (command.Start, command.End, command.SolutionVariant),
                new SolutionVariant(command.SolutionVariant));

            await _inquiryRepo.AddAsync(inquiry);


        }
    }
}
