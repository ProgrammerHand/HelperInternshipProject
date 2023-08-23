using Helper.Core.Exceptions;

namespace Helper.Application.Exceptions
{
    public sealed class InquiryDoesntExistException : CustomException
    {
        public InquiryDoesntExistException() : base("Inquiry doesnt exist")
        {   
        }
    }
}
