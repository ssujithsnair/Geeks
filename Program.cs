using Geeks.Bit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    class Program
    {
        static void Bit()
        {
            int x = 5;
            var p1 = x & (-x);
            int[] arr = { 3, 3, 2, 3 };
            var p = System.Runtime.InteropServices.Marshal.SizeOf(arr[0]);
            int val = Bitwise.ElementThatAppearsOnce(arr);

            val = Bitwise.countSetBits(17);

            val = Bitwise.SwapBits(47, 1, 5, 3);
            val = Bitwise.Smallest(2, 5, 1);
            Bitwise.changeToZero();
            val = Bitwise.NextHigherNumberWithSameBits(156);
            uint valu = Bitwise.reverseBits(1);
            Bitwise.BinaryRep(5);
            bool valb = Bitwise.IsPaliandrome(9);
        }
        static void DyP()
        {
            var valb = DP.FindSubSequence("nematode knowledge", "nano");
            var vals = DP.LongestCommonSubsequence("nematode knowledge", "npaarefnror");
            var val = DP.LongestIncreasingSubsequence(new int[] { 10, 22, 9, 33, 21, 50, 41, 60 });
            val = DP.EditDistance("sunday", "saturday");
            int [,]Cost = new int[,] { {1, 2, 3}, {4, 8, 2}, {1, 5, 3} };
            val = DP.CostPath(Cost,2,2);
            val = DP.CoinChange(new int[] { 2,5,3,6 }, 0);
            val = DP.EggDrop(2, 36);
        }

        static void SearchP()
        {
            int []arr = {2, 3, 4, 10, 40,50};
            int val = Search.BinarySearch(arr, 40);
            val = Search.ExponentialSearch(arr, 50);
            val = Search.ternarySearch(arr, 0, arr.Length - 1, 50);
        }
        static void MiscP()
        {
            int val = Misc.LatticePoints(5);
            Misc.TestMinHeap();
        }
        static void SortP()
        {
            int[] arr = { 64, 25, 12, 22, 11 };
            Sort.SelectionSort(arr);//
            Sort.MergeSort(arr, 0, arr.Length - 1);
            Sort.InsertionSortSingleLinkedList();
            //arr = new int[] {10, 7, 8, 9, 1, 5};
            arr = new int[]{ 64, 25, 12, 22, 11 };
            //Sort.QuickSort(arr);
            //Array.Reverse(arr);
            //arr = new int[] { 1, 4, 2, 4, 2, 4, 1, 2, 4,8, 1, 2, 2, 2, 2, 4, 1, 4, 4, 4 };
            Sort.QuickSort(arr);
            arr = new int[] { 99, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 5, 1 };
            Sort.BucketSort(arr);
            arr = new int[] { 10, 12, 20, 30, 25, 40, 32, 31, 35, 50, 60 };
            Sort.FindMinimumLengthToSort(arr);
            Sort.MergeSortSingleLinkedList();
        }
        static void Main(string[] args)
        {
            //Bit();
            //DyP();
            //SearchP();
            SortP();
            //MiscP();
        }
    }
}
