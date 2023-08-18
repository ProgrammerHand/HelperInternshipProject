using Helper.Application.Abstraction.Queries;
using Helper.Application.DTO;
using Helper.Application.Inquiry.Queries;

namespace Helper.Infrastructure.DAL.Handlers
{
    internal class GetInquirySolutionsVariantsHandler : IQueryHandler<GetInquirySolutionVariants, InquirySolutionVariantsDto>
    {
        public async Task<InquirySolutionVariantsDto> HandleAsync(GetInquirySolutionVariants querry)
        {
            return new InquirySolutionVariantsDto();
        }
    }
}
