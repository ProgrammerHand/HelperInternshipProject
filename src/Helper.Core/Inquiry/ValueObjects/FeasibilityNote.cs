using Helper.Application.Exceptions;
using Helper.Core.Inquiry.Exceptions;

namespace Helper.Core.Inquiry.ValueObjects
{
    public class FeasibilityNote
    {
        public string Body { get; set; }

        public FeasibilityNote(string body)
        {
            if (string.IsNullOrEmpty(body) || string.IsNullOrWhiteSpace((body)))
                throw new NoFeasibilityNoteWasGivenException();
            Body = body;
        }

        public static implicit operator string(FeasibilityNote data) => data.Body;

        public static implicit operator FeasibilityNote(string body) => new(body);
    }
}
