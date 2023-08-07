using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Application.DTO
{
    public sealed record InquirySolutionVariantsDto
    {
        public string[] SolutionVariants => Enum.GetNames(typeof(Variants));
    }
}
