using Helper.Core.Exceptions;

namespace Helper.Core.Offer.Exceptions
{
    public class StorageLinkEmptyException : CustomException
    {
        public StorageLinkEmptyException() : base("Link to storage is empty")
        {
            
        }
    }
}
