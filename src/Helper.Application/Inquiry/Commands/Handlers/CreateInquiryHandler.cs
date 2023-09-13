using Helper.Application.Abstraction.Commands;
using Helper.Application.Integrations;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.User;
using Helper.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Helper.Application.Inquiry.Commands.Handlers
{
    public sealed class CreateInquiryHandler : ICommandHandler<CreateInquiry>
    {

        private readonly IClockCustom _clock;
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMailSendingClient _mailclient;
        private readonly IUrlHelper _urlHelper;

        public CreateInquiryHandler(IClockCustom clock, IInquiryRepository inquiryRepo, IUserRepository userRepo, 
            IMailSendingClient mailclient, IUrlHelperFactory urlHelperFactory, Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor actionContextAccessor)
        {
            _clock = clock;
            _inquiryRepo = inquiryRepo;
            _userRepo = userRepo;
            _mailclient = mailclient;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);

        }

        public async Task HandleAsync(CreateInquiry command)
        {
            InquiryParamsValidator.Validate(command, _clock);
            var author = await _userRepo.GetByIdAsync(command.AuthorId);
            var inquiry = Core.Inquiry.Inquiry.CreateInquiry(new Description(command.Description),
                new RealisationDate(command.Start, command.End, command.SolutionVariant, _clock),
                new SolutionVariant(command.SolutionVariant), author);
            await _inquiryRepo.AddAsync(inquiry);
            var url = _urlHelper.Action("GetInquiry", "Inquiry", new { inquiryId = inquiry.Id.Value }, "https", "localhost:7105") ;
            var mailData = new MailDto
            {
                ReciverEmail = inquiry.Author.Email,
                ReciverName = "User",
                Subject = "Your inquiry sucsesfully registerd",
                Content = $"Dear user your Inquiry was registered, please use this url to check Inquiry status: {url}"
            };
            //await _mailclient.SendMailAsync(mailData);

        }
    }
}
