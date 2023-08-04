
namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed class SolutionVariants
    {
        public Variants Variant { get; private set; }

        public SolutionVariants(Variants variant)
        {
            Variant = variant;
        }

    }
}
