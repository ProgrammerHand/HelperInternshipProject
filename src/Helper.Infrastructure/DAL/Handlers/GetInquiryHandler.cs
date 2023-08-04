using Helper.Application.Abstractions;
using Helper.Application.DTO;
using Helper.Application.Queries;

namespace Helper.Infrastructure.DAL.Handlers
{
    public class GetInquiryHandler : IQueryHandler<GetInquiry, InquiryDto>
    {
        public async Task<InquiryDto> HandleAsync(GetInquiry query)
        {
            //DB Interaction
            throw new NotImplementedException();
        }
    }
}
