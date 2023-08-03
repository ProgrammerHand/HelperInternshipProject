using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.ValueObjects
{
    public sealed class Description
    {
        public string Body { get; set; }

        public Description(string body)
        {
            Body = body;
         }
    }
}
