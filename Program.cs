using System;
using System.Threading;

namespace Lab2_AaDS
{
    class Program
    {

        static void Main(string[] args)
        {
            int nTests = 10;
            for (int i = 10; i < 100001; i *= 10)
            {
                int[] array = new int[i];

                var qsortTime = TimeMeasurementUtility.MeasureExecutionTime(() =>
                {
                    array.RandFill();
                    array.QuickSort();
                },
                nTests, true);
                Console.WriteLine($"QuickSort execution time for {i} elements (Minutes:seconds:milliseconds): " + ((int)Math.Floor(qsortTime.TotalMinutes)).ToString("D2") + ":" +  ((int)Math.Floor(qsortTime.TotalSeconds)).ToString("D2") + ":" + ((int)Math.Floor(qsortTime.TotalMilliseconds)).ToString("D4"));


                var bubbleSortTime = TimeMeasurementUtility.MeasureExecutionTime(() =>
                {
                    array.RandFill();
                    array.BubbleSort();
                },
                nTests, true);
                Console.WriteLine($"BubbleSort execution time for {i} elements (Minutes:seconds:milliseconds): " + ((int)Math.Floor(bubbleSortTime.TotalMinutes)).ToString("D2") + ":" +  ((int)Math.Floor(bubbleSortTime.TotalSeconds)).ToString("D2") + ":" + ((int)Math.Floor(bubbleSortTime.TotalMilliseconds)).ToString("D4"));

                Console.WriteLine();
            }
        }
    }
}
