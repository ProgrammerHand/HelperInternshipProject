
namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record SolutionVariant
    {
        public Variants Value { get; }

        public SolutionVariant(Variants variant)
        {
            Value = variant;
        }

        public static implicit operator Variants(SolutionVariant data) => data.Value;

        public static implicit operator SolutionVariant(Variants value) => new(value);

    }
}
