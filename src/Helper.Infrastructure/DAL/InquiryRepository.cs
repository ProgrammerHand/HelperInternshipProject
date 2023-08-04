using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Infrastructure.DAL
{
    public class InquiryRepository : IInquiryRepository
    {
        public Task AddAsync(Inquiry inquiry)
        {
            return Task.FromResult("dupa");
        }

        public Task<Inquiry> GetByIdAsync(InquiryId inquiry)
        {
            return Task.FromResult(Inquiry.CreateInquiry(null,null,null));
        }

        public Task UpdateAsync(Inquiry inquiry)
        {
            return Task.FromResult("dupa");
        }
    }
}
