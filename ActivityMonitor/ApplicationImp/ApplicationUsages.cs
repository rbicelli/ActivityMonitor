using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityMonitor.Application
{
    public class ApplicationUsages : List<ApplicationUsage>
    {



        public TimeSpan TotalUsageTime()
        {

            return this.Aggregate(TimeSpan.Zero, (subtotal, t) => subtotal.Add(t.Total));

        }

    }
}