using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;

namespace Helper.Application.Inquiry.Queries
{
    public class GetInquiry : IQuery<InquiryDto>
    {
        public Guid Id { get; private set; }

        public GetInquiry(Guid id)
        {
            Id = id;
        }
    }
}
