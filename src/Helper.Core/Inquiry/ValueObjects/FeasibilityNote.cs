using Helper.Core.Inquiry.Exceptions;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed record FeasibilityNote
    {
        public string Value { get; set; }

        public FeasibilityNote(string body)
        {
            if (string.IsNullOrEmpty(body) || string.IsNullOrWhiteSpace((body)))
                throw new NoFeasibilityNoteWasGivenException();
            Value = body;
        }

        public static implicit operator string(FeasibilityNote data) => data.Value;

        public static implicit operator FeasibilityNote(string body) => new(body);
    }
}
