using System;
using System.Diagnostics;

namespace Lab2_AaDS
{
    public static class TimeMeasurementUtility
    {
        public static TimeSpan MeasureExecutionTime(Action action, int nTests=1, bool useApproxValue=false)
        {
            if (nTests < 1)
                throw new ArgumentException("Number of tests must be positive");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            for (int i=0;i<nTests;i++)
            {
                action();
            }
            stopwatch.Stop();
            return useApproxValue ? stopwatch.Elapsed/nTests : stopwatch.Elapsed;
        }

    }
}
