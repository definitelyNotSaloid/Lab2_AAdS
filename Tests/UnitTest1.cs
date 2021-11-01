using NUnit.Framework;
using Lab2_AaDS;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_QuickSort()
        {
            int[] array = new int[10000];

            array.RandFill();
            array.QuickSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 20);
            array.QuickSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 2);
            array.QuickSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 1);
            array.QuickSort();
            Assert.IsTrue(array.IsSorted());
        }

        [Test]
        public void Test_BubbleSort()
        {
            int[] array = new int[10000];

            array.RandFill();
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 20);
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 2);
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 1);
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());
        }

        [Test]
        public void Test_CountingSort()
        {
            byte[] array = new byte[10000];

            array.RandFill();
            array.CountingSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 2);
            array.CountingSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 1);
            array.CountingSort();
            Assert.IsTrue(array.IsSorted());
        }

        [Test]
        public void Test_BinarySearch()
        {
            int[] array = new int[10000];
            array.RandFill();
            array[0] = 999;
            array[1] = -999;
            for (int i = 0; i < 10000; i++)
            {
                if (array[i] == -1)
                    array[i] = 0;
            }
            Array.Sort(array);

            Assert.IsTrue(array.BinarySearch(999) != -1);
            Assert.IsTrue(array.BinarySearch(-999) != -1 && array[array.BinarySearch(-999)]==-999);     //double search, but who cares? This is a test, not an actual program
            Assert.IsTrue(array.BinarySearch(-1) == -1);
        }

        [Test]
        public void Test_BestSortEver()
        {
            int[] array = new int[10];

            array.RandFill();
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 20);
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 2);
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());

            array.RandFill(0, 1);
            array.BubbleSort();
            Assert.IsTrue(array.IsSorted());
        }

        [Test]
        public void Test_Exceptions()
        {
            int[] array = new int[10];
            Assert.Throws<ArgumentOutOfRangeException>(() => array.QuickSort(12, 15));
            Assert.Throws<ArgumentOutOfRangeException>(() => array.QuickSort(2, 15));
            Assert.Throws<ArgumentOutOfRangeException>(() => array.QuickSort(-2, 15));
            Assert.Throws<ArgumentException>(() => array.QuickSort(5, 2));

            int[] arrayForBogo = new int[10000];
            arrayForBogo.RandFill();
            Assert.Throws<TimeoutException>(() => arrayForBogo.BestSortEver(true));         
        }
    }
}