using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Inquiry.Queries;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetInquiryHandler : IQueryHandler<GetInquiry, InquiryDto>
    {
        private readonly HelperDbContext _context;

        public GetInquiryHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<InquiryDto> HandleAsync(GetInquiry query)
        {
            var inquiryId = new InquiryId(query.Id);
            var inquiry = await _context.Inquiries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == inquiryId);
            return inquiry?.AsDto();
        }
    }
}
