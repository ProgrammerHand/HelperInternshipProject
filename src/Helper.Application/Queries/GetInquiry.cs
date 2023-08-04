using Helper.Application.Abstractions;
using Helper.Application.DTO;

namespace Helper.Application.Queries
{
    public class GetInquiry : IQuery<InquiryDto>
    {
        public Guid Id { get; private set; }
    }
}
