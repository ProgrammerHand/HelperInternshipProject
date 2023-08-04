using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Application.DTO
{
    public class InquirySolutionVariantsDto
    {
        public string[] SolutionVariants => Enum.GetNames(typeof(Variants));
     }
}
