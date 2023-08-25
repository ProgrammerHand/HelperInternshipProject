using Helper.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Offer.Exceptions
{
    public class PaymentDateTooLateException : CustomException
    {
        public PaymentDateTooLateException() : base("Given date less than 5 days before realistation date")
        {
        }
    }
}
