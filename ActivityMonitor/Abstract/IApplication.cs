using System;
using System.Drawing;
using ActivityMonitor.Application;

namespace ActivityMonitor.Collections
{
    public interface IApplication
    {
        string Path { get; }
        string Name { get; }
        TimeSpan TotalUsageTime { get; }
        ApplicationUsages Usage { get; }
        ApplicationDetails Details { get; }
        Icon Icon { get; set; }

    }
}