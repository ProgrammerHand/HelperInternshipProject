using Helper.Application.Abstraction.Commands;
using Helper.Application.Integrations;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User;
using Helper.Core.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Application.Inquiry.Commands.Handlers
{
    public sealed class CreateInquiryHandler : ICommandHandler<CreateInquiry>
    {

        private readonly IClockCustom _clock;
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMailSendingClient _mailclient;
        private readonly IUrlHelper _urlHelper;

        public CreateInquiryHandler(IClockCustom clock, IInquiryRepository inquiryRepo, IUserRepository userRepo, IMailSendingClient mailclient)
        {
            _clock = clock;
            _inquiryRepo = inquiryRepo;
            _userRepo = userRepo;
            _mailclient = mailclient;
        }

        public async Task HandleAsync(CreateInquiry command)
        {
            InquiryParamsValidator.Validate(command, _clock);
            var author = await _userRepo.GetByIdAsync(command.AuthorId);
            var inquiry = Core.Inquiry.Inquiry.CreateInquiry(new Description(command.Description),
                new RealisationDate(command.Start, command.End, command.SolutionVariant, _clock),
                new SolutionVariant(command.SolutionVariant), author);
            await _inquiryRepo.AddAsync(inquiry);
            var mailData = new MailDto
            {
                ReciverEmail = inquiry.Author.Email,
                ReciverName = "user", // name from User?
                Subject = "Your inquiry sucsesfully registerd",
                Content = $"Dear user your Inquiry was registered, please use this id to check Inquiry status: {inquiry.Id.Value}"
            };
            await _mailclient.SendMailAsync(mailData);

        }
    }
}
