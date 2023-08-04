using Helper.Application.Abstractions;
using Helper.Application.DTO;
using Helper.Application.Queries;

namespace Helper.Infrastructure.DAL.Handlers
{
    public class GetInquiriesHandler : IQueryHandler<GetInquiries, List<InquiryDto>>
    {
        public async Task<List<InquiryDto>> HandleAsync(GetInquiries querry)
        {
            //DB Interaction
            throw new NotImplementedException();
        }
    }
}
