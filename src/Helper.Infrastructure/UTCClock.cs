using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Core;

namespace Helper.Infrastructure
{
    public class UTCClock : IClockCustom
    {
        public DateTime Now => DateTime.UtcNow;

    }
}
