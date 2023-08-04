using Helper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Commands
{
    public static class InquiryParamsValidator
    {
        public static void Validate(CreateInquiry command, IClockCustom clock)
        {

            if (command.Description == null)
                throw new ArgumentNullException();
            //if (command.RequestedCompletionDate <= clock.Now.Date)
                //throw new ArgumentNullException();
        }
    }
}
