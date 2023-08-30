﻿using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User;
using Helper.Core.Utility;

namespace Helper.Application.Inquiry.Commands.Handlers
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
            var inquiry = Core.Inquiry.Inquiry.CreateInquiry(new Description(command.Description),
                new RealisationDate(command.Start, command.End, command.SolutionVariant, _clock),
                new SolutionVariant(command.SolutionVariant), author);

            await _inquiryRepo.AddAsync(inquiry);


        }
    }
}
