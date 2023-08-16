using Helper.Application.Abstractions;
using Helper.Core.Inquiry;
using Helper.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Commands.Handlers
{
    public sealed class ChangeAuthorHandler : ICommandHandler<ChangeAuthor>
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IUserRepository _userRepo;

        public ChangeAuthorHandler(IInquiryRepository inquiryRepo, IUserRepository userRepo)
        {
            _inquiryRepo = inquiryRepo;
            _userRepo = userRepo;
        }
        public async Task HandleAsync(ChangeAuthor command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiryId);
            var newAuthor = await _userRepo.GetByIdAsync(command.NewAuthorId);
            inquiry.ChangeAuthor(newAuthor);
            await _inquiryRepo.UpdateAsync(inquiry);
        }
    }
}
