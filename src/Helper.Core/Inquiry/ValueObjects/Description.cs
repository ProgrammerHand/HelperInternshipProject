using Helper.Core.Exceptions;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace((value)))
                throw new NoDescriptionGivenException();
            Value = value;
        }

        public static implicit operator string(Description data) => data.Value;

        public static implicit operator Description(string body) => new(body);
    }
}
