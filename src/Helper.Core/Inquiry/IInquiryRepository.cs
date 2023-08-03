using Helper.Core.Inquiry;
using Helper.Core.ValueObjects;

namespace Helper.Core.Inquiry
{
    internal interface IInquiryRepository
    {
        Task AddAsync(Inquiry inquiry);
        Task UpdateAsync(Inquiry inquiry);
        Task<Inquiry> GetByIdAsync(InquiryId inquiry);
    }
}
