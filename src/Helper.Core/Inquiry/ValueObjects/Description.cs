using Helper.Application.Exceptions;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed class Description
    {
        public string Body { get; set; }

        public Description(string body)
        {
            if (string.IsNullOrEmpty(body) || string.IsNullOrWhiteSpace((body)))
                throw new NoDescriptionGivenException();
            Body = body;
        }

        public static implicit operator string(Description data) => data.Body;

        public static implicit operator Description(string body) => new(body);
    }
}
