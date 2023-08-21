using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Core.Inquiry
{
    public interface IInquiryRepository
    {
        Task AddAsync(Inquiry inquiry);
        Task UpdateAsync(Inquiry inquiry);
        Task DeleteInquiry(Inquiry inquiry);
        Task<Inquiry> GetByIdAsync(InquiryId inquiry);
    }
}
