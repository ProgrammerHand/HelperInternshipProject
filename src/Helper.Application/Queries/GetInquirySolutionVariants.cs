using Helper.Application.Abstractions;
using Helper.Application.DTO;

namespace Helper.Application.Queries
{
    public class GetInquirySolutionVariants : IQuery<InquirySolutionVariantsDto>
    {
        public int Id { get; set; }
    }
}
