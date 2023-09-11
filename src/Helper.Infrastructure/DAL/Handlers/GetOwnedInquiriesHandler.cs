using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Inquiry.Queries;
using Microsoft.EntityFrameworkCore;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetOwnedInquiriesHandler : IQueryHandler<GetOwnedInquiries, List<InquiryDto>>
    {
        private readonly HelperDbContext _context;
        public GetOwnedInquiriesHandler(HelperDbContext DbContext) => _context = DbContext;
        public async Task<List<InquiryDto>> HandleAsync(GetOwnedInquiries querry)
        {
            //DB Interaction
            var entities = await _context.Inquiries.Include(x => x.Author).Where(x => x.Author.Id == querry.AuthorId).ToListAsync();
            var inquiries = new List<InquiryDto>();
            foreach (var entity in entities)
            {
                inquiries.Add(entity.AsDto());
            }
            return inquiries;
        }
    }
}
