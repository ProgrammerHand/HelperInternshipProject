using Helper.Core.Inquiry.Exceptions;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record Description
    {
        public string Value { get; set; }

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
