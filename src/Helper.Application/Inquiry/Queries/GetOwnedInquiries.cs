using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Core.User;

namespace Helper.Application.Inquiry.Queries
{
    public class GetOwnedInquiries : IQuery<List<InquiryDto>>
    {
        public UserId AuthorId { get; private set; }

        public GetOwnedInquiries(Guid id)
        {
            AuthorId = id;
        }
    }
}
