using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab2_AaDS
{
    public class ArrayRange
    {
        public readonly int left;
        public readonly int right;
        public readonly int[] array;
        public readonly int size;
        public ArrayRange(
            int[] array, 
            int left, 
            int right)
        {
            if (left < 0 || left>array.Length)
                throw new ArgumentOutOfRangeException("Left border is out of range");

            if (right < 0 || right > array.Length)
                throw new ArgumentOutOfRangeException("Right border is out of range");

            if (right < left)
                throw new ArgumentException("Right border cant be less then left border");

            this.left = left;
            this.right = right;
            this.array = array;
            size = right - left;
        }

        public int this[int index]
        {
            get => array[left + index];
            set => array[left + index] = value;
        }
    }

    public static class ArrayUtility
    {
        private static Random _rng = new Random();

        public static void RandFill(this int[] array, int minValue = -10000000, int maxValue = 10000000)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = _rng.Next(minValue, maxValue);
            }
        }

        public static void RandFill(this byte[] array, byte minValue = 0, byte maxValue = 255)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)_rng.Next(minValue, maxValue);
            }
        }

        public static bool IsSorted(this int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                    return false;
            }

            return true;
        }
        public static bool IsSorted(this byte[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                    return false;
            }

            return true;
        }

        //returns index of requested element if it present, -1 otherwise. Only for sorted arrays
        public static int BinarySearch(this int[] data, int val)
        {
            int size = data.Length;

            if (size == 0)
                return -1;

            int leftBorder = 0;             //included in current subarray
            int rightBorder = size;         //excluded from it

            int center = size / 2;

            while (leftBorder < rightBorder)
            {
                if (data[center] == val)
                    return center;

                if (data[center] > val)
                    rightBorder = center;
                else
                    leftBorder = center + 1;

                center = (leftBorder + rightBorder) / 2;
            }

            return -1;
        }

        public static void BubbleSort(this int[] data)
        {
            int size = data.Length;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        int tmp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = tmp;
                    }
                }
            }
        }

       /* 
        * Not my task.
        public static void InsertionSort(this int[] data)
        {
            int size = data.Length;
            if (size < 2)
                return;

            for (int i = 1; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    int newPos = 0;
                    while (data[newPos] < data[j] && newPos < j)
                    {
                        newPos++;
                    }

                    int tmp = data[newPos];
                    data[newPos] = data[j];
                    data[j] = tmp;
                }
            }
        }*/

        public static void BestSortEver(this int[] data, bool safeMode = true)
        {
            int size = data.Length;
            if (size < 2)
                return;


            int nIterations = 0;

            while (!data.IsSorted())
            {
                for (int i = 0; i < size; i++)
                {
                    int index = _rng.Next(i, size);

                    int tmp = data[index];
                    data[index] = data[i];
                    data[i] = tmp;
                }

                
                if (safeMode)
                {
                    nIterations++;
                    if (nIterations > (int)10e4)
                    {
                        throw new TimeoutException("Too many iterations at " + nameof(BestSortEver));
                    }
                }
            }
        }

        private static int? QuickSort_GetAnchor(this ArrayRange data)           //returns anchor value or null if all of elements are equal
        {
            int maxFound = data[0];
            int minFound = data[0];

            for (int i=1;i<data.size;i++)
            {
                if (data[i] < maxFound && data[i] > minFound)
                    return data[i];

                if (data[i] == maxFound && data[i] != minFound)
                    return data[i];

                if (data[i]<minFound)
                    minFound = data[i];

                if (data[i] > maxFound)
                    maxFound = data[i];
            }
            if (maxFound == minFound)
                return null;

            return maxFound;
        }

        private static IEnumerable<ArrayRange> QuickSort_SeparateAndGetSubarrsToSort(this ArrayRange data)
        {
            if (data.size > 2)
            {
                int? anchorValue = data.QuickSort_GetAnchor();                 //if anchor is the lowest value in array, algorithm will stuck in endless loop
                if (anchorValue != null)
                {
                    int separatorIndex = 0;

                    for (int i = 0; i < data.size; i++)
                    {
                        if (data[i] < anchorValue)
                        {
                            int tmp = data[separatorIndex];
                            data[separatorIndex] = data[i];
                            data[i] = tmp;

                            separatorIndex++;
                        }
                    }

                    if (separatorIndex > 1)
                        yield return new ArrayRange(data.array, data.left, data.left + separatorIndex);
                    if (data.size - separatorIndex > 1)
                        yield return new ArrayRange(data.array, data.left + separatorIndex, data.right);
                }
                
            }

            if (data.size==2)                //its simply faster to sort small ranges "at place"
            {
                if (data[0]>data[1])
                {
                    int tmp = data[0];
                    data[0] = data[1];
                    data[1] = tmp;
                }
            }

        }

        public static void QuickSort(this int[] data, int beginIndex = 0, int endIndex = -1) => (new ArrayRange(data, beginIndex, endIndex != -1 ? endIndex : data.Length)).QuickSort();

        public static void QuickSort(this ArrayRange data)
        {
            bool allRangesSorted = false;

            List<ArrayRange> ranges = new List<ArrayRange>();
            ranges.Add(data);

            while (!allRangesSorted)
            {
                List<ArrayRange> newRanges = new List<ArrayRange>();

                foreach(var range in ranges)
                {
                    foreach (var subArrToBeSorted in range.QuickSort_SeparateAndGetSubarrsToSort())
                    {
                        newRanges.Add(subArrToBeSorted);
                    }
                }

                allRangesSorted = newRanges.Count == 0;
                
                ranges = newRanges;
            }
        }

        

        public static void CountingSort(this byte[] data)
        {
            int[] counters = new int[256];

            foreach (var i in data)
            {
                counters[i]++;
            }

            byte index = 0;
            for (int i = 0; i < data.Length; i++)
            {
                while (counters[index] == 0)
                {
                    index++;
                    if (index > 255)
                        throw new Exception("Something went absolutely wrong at " + nameof(CountingSort) + " (indexator overflow)");
                }

                data[i] = index;
            }
        }
    }
}