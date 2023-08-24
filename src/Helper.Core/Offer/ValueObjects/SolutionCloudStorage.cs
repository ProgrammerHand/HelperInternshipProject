using Helper.Core.Exceptions;
using Helper.Core.Offer.Exceptions;

namespace Helper.Core.Offer.ValueObjects
{
    public sealed record SolutionCloudStorage
    {
        public string Value { get; }

        public SolutionCloudStorage(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new StorageLinkEmptyException();
            }
            Value = value;
            
        }

        public SolutionCloudStorage()
        {
            
        }
    }
}
