using Helper.Application.Abstractions;
using Helper.Core;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User;

namespace Helper.Application.Commands.Handlers
{
    public sealed class CreateInquiryHandler : ICommandHandler<CreateInquiry>
    {

        private readonly IClockCustom _clock;
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IUserRepository _userRepo;

        public CreateInquiryHandler(IClockCustom clock, IInquiryRepository inquiryRepo, IUserRepository userRepo)
        {
            _clock = clock;
            _inquiryRepo = inquiryRepo;
            _userRepo = userRepo;
        }


        public async Task HandleAsync(CreateInquiry command)
        {
            InquiryParamsValidator.Validate(command, _clock);
            var author = await _userRepo.GetByIdAsync(command.AuthorId);
            var inquiry = Inquiry.CreateInquiry(new Description(command.Description),
                new RealisationDate (command.Start, command.End, command.SolutionVariant, _clock),
                new SolutionVariant(command.SolutionVariant), author);

            await _inquiryRepo.AddAsync(inquiry);


        }
    }
}
