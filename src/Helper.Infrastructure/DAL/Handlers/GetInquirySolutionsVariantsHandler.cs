using Helper.Application.Abstractions;
using Helper.Application.DTO;
using Helper.Application.Queries;

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
