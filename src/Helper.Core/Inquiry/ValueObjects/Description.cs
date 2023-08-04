using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Inquiry.ValueObjects
{
    public sealed class Description
    {
        public string Body { get; set; }

        public Description(string body)
        {
            // throw exceprion if body null
            Body = body;
        }
    }
}
