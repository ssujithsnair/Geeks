using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    static class Search
    {
        //http://www.geeksforgeeks.org/binary-search/
        public static int BinarySearch(int[] arr, int x)
        {
            //return BinarySearchRecur(arr, 0, arr.Length -1, x);
            return BinarySearchIterative(arr, 0, arr.Length - 1, x);
        }

        // O(log n)
        // Auxiliary Space: O(Logn) recursion call stack space.
        private static int BinarySearchRecur(int[] arr, int L, int R, int x)
        {
            if (R < L)
                return -1;
            int m = L + (R - L) / 2;
            if (arr[m] == x)
                return m;
            if (arr[m] > x)
                return BinarySearchRecur(arr, L, m - 1, x);
            else
                return BinarySearchRecur(arr, m + 1, R, x);
        }

        // O(log n)
        // Auxiliary Space: O(1)
        private static int BinarySearchIterative(int[] arr, int L, int R, int x)
        {
            while (L <= R)
            {
                int m = L + (R - L) / 2;
                if (arr[m] == x)
                    return m;
                if (arr[m] > x)
                    R = m-1;
                else
                    L = m+1;
            }
            return -1;
        }
        //Jump Search takes O(√ n) time
        /*
         * The Interpolation Search is an improvement over Binary Search for instances,
         * where the values in a sorted array are uniformly distributed. Binary Search always goes to middle element to check.
         * On the other hand interpolation search may go to different locations according the value of key being searched.
         * The idea of formula is to return higher value of pos when element to be searched is closer to arr[hi]. And
           smaller value when closer to arr[lo]
           pos = lo + [ (x-arr[lo])*(hi-lo) / (arr[hi]-arr[Lo]) ]
         * Time Complexity : If elements are uniformly distributed, then O (log log n)). In worst case it can take upto O(n).
           Auxiliary Space : O(1)
         */

        /*
         * Exponential search involves two steps:
            Find range where element is present
            Do Binary Search in above found range.
         * The idea is to start with subarray size 1 compare its last element with x, then try size 2, then 4
         * and so on until last element of a subarray is not greater.
           Once we find an index i (after repeated doubling of i), we know that the element must be present between
         * i/2 and i (Why i/2? because we could not find a greater value in previous iteration)
         */
        public static int ExponentialSearch(int[] arr, int x)
        {
            int n = arr.Length;
            if (arr[0] == x)
                return 0;
            int i = 1;
            while (i < n && arr[i] <= x)
                i = i * 2;

            // i can become > n after doubling, so take min
            return BinarySearchIterative(arr, i / 2, Math.Min(i, n), x);
        }
        public static int ternarySearch(int[] arr, int l, int r, int x)
        {
            if (r >= l)
            {
                int mid1 = l + (r - l) / 3;
                int mid2 = mid1 + (r - l) / 3;

                // If x is present at the mid1
                if (arr[mid1] == x) return mid1;

                // If x is present at the mid2
                if (arr[mid2] == x) return mid2;

                // If x is present in left one-third
                if (arr[mid1] > x) return ternarySearch(arr, l, mid1 - 1, x);

                // If x is present in right one-third
                if (arr[mid2] < x) return ternarySearch(arr, mid2 + 1, r, x);

                // If x is present in middle one-third
                return ternarySearch(arr, mid1 + 1, mid2 - 1, x);
            }
            // We reach here when element is not present in array
            return -1;
        }

    }
}
