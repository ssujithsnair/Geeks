using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    static class Sort
    {
        // Time Complexity: O(n2) as there are two nested loops.
        // Auxiliary Space: O(1)
        public static void SelectionSort(int[] arr)
        {
            int n = arr.Length;
            int min;
            for (int i = 0; i < n - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[min])
                        min = j;
                }
                Misc.Swap(ref arr[i], ref arr[min]);
            }
        }

        /*
         * Worst and Average Case Time Complexity: O(n*n). Worst case occurs when array is reverse sorted.
            Best Case Time Complexity: O(n). Best case occurs when array is already sorted.
            Auxiliary Space: O(1)
            Boundary Cases: Bubble sort takes minimum time (Order of n) when elements are already sorted.
            Sorting In Place: Yes
         */
        public static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            // repeatedly swapping the adjacent elements if they are in wrong order.
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        Misc.Swap(ref arr[j], ref arr[j + 1]);
                        swapped = true;
                    }
                }
                // IF no two elements were swapped by inner loop, then break
                if (!swapped)
                    break;
            }
        }

        /*
         * Time Complexity: O(n*n)
           Auxiliary Space: O(1)
         * Insertion sort takes maximum time to sort if elements are sorted in reverse order.
         * And it takes minimum time (Order of n) when elements are already sorted
         * Insertion sort is used when number of elements is small. It can also be useful when input array is almost sorted,
         * only few elements are misplaced in complete big array.
         */
        public static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int current = arr[i];
                int j = i - 1;
                /* Move elements of arr[0..i-1], that are greater than key, to one position ahead
                    of their current position */
                while (j >= 0 && arr[j] > current)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = current;
            }
        }

        private static void Merge(int[] arr, int lo, int med, int hi)
        {
            int n1 = med - lo + 1;
            int n2 = hi - med;
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j, k;
            for (i = 0; i < n1; i++)
                L[i] = arr[lo + i];
            for (j = 0; j < n2; j++)
                R[j] = arr[med + 1 + j];

            i = 0; j = 0; k = lo;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = L[i];
                i++; k++;
            }
            while (j < n2)
            {
                arr[k] = R[j];
                j++; k++;
            }
        }

        //Time complexity of Merge Sort is \Theta(nLogn) in all 3 cases (worst, average and best) as merge sort always divides
        // the array in two halves and take linear time to merge two halves.
        // Auxiliary Space: O(n)
        //http://www.geeksforgeeks.org/merge-sort/ CHECK IT
        public static void MergeSort(int[] arr, int lo, int hi)
        {
            if (lo >= hi)
                return;
            int med = lo + (hi - lo) / 2;
            MergeSort(arr, lo, med);
            MergeSort(arr, med + 1, hi);
            Merge(arr, lo, med, hi);
        }

        public static void InsertionSortSingleLinkedList()
        {
            var list = GetLinkedList();
            list.InsertionSortSingle();
            Console.WriteLine("LinkedList After sorting");
            list.printlist();
        }

        public static void MergeSortSingleLinkedList()
        {
            var list = GetLinkedList();
            list.MergeSort();
            Console.WriteLine("LinkedList After sorting");
            list.printlist();
        }

        private static LinkedList GetLinkedList()
        {
            LinkedList list = new LinkedList();
            list.Push(5);
            list.Push(20);
            list.Push(4);
            list.Push(3);
            list.Push(30);
            Console.WriteLine("Linked List before Sorting..");
            list.printlist();
            return list;
        }

        /*
         * http://www.geeksforgeeks.org/quick-sort/ MUST CHECK
         * Worst Case: O(n^2) when the partition process always picks greatest or smallest element as pivot.
         * Best Case: O(nlogn) when the partition process always picks the middle element as pivot.
         * Average Case: O(n log n)
         * Random quicksort pivot: int pivotIdx = start + rand() % (end-start+1);
         */
        public static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int lo, int hi)
        {
            if (hi <= lo)
                return;
            int pivot = SetPivot(arr, lo, hi);
            QuickSort(arr, lo, pivot - 1);
            QuickSort(arr, pivot + 1, hi);
        }

        private static void QuickSortIterative(int[] arr, int l, int h)
        {
            // create auxiliary stack
            int[] stack = new int[h - l + 1];

            // initialize top of stack
            int top = -1;

            // push initial values in the stack
            stack[++top] = l;
            stack[++top] = h;

            // keep popping elements until stack is not empty
            while (top >= 0)
            {
                // pop h and l
                h = stack[top--];
                l = stack[top--];

                // set pivot element at it's proper position
                int p = SetPivot(arr, l, h);

                // If there are elements on left side of pivot,
                // then push left side to stack
                if (p - 1 > l)
                {
                    stack[++top] = l;
                    stack[++top] = p - 1;
                }

                // If there are elements on right side of pivot,
                // then push right side to stack
                if (p + 1 < h)
                {
                    stack[++top] = p + 1;
                    stack[++top] = h;
                }
            }
        }

        private static int SetPivot(int[] arr, int lo, int hi)
        {
            int i = lo;
            int pivot = arr[hi];
            for (int j = lo; j < hi; j++)
            {
                if (arr[j] <= pivot)
                {
                    Misc.Swap(ref arr[i], ref arr[j]);
                    i++;
                }
            }
            Misc.Swap(ref arr[i], ref arr[hi]);
            return i;
        }

        public static void BucketSort(int[] arr)
        {
            int bCount = 10;
            List<int>[] buckets = new List<int>[bCount];
            for (int i = 0; i < bCount; i++)
                buckets[i] = new List<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                var bIndex = arr[i] / bCount;
                buckets[bIndex].Add(arr[i]);
            }
            int j = 0;
            foreach (var bucket in buckets)
            {
                var bArray = bucket.ToArray();
                //BucketSort(bArray); Cannot do bucketsort itself since it will cause recursion overflow. For eg. the first bucket will
                // keep coming back to the first bucket of recursion call 
                InsertionSort(bucket.ToArray());
                foreach (var val in bArray)
                    arr[j++] = val;
            }
        }
        /*
         * A sorting algorithm is said to be stable if two objects with equal keys appear in the same order in sorted output
         * as they appear in the input array to be sorted.
         * Stable: Insertion Sort, Merge Sort, Count Sort
         * Unstable: Quick Sort, Heap Sort
         * any comparison based sorting algorithm which is not stable by nature can be modified to be stable by changing the
         * key comparison operation so that the comparison of two keys considers position as a factor for objects with equal keys.
         */
        /*
         * Minimizing the number of writes is useful when making writes to some huge data set is very expensive,
         * such as with EEPROMs or Flash memory, where each write reduces the lifespan of the memory.
         * Selection Sort makes least number of writes (it makes O(n) swaps).
         * But, Cycle Sort almost always makes less number of writes compared to Selection Sort.  
         * In Cycle Sort, each value is either written zero times, if it’s already in its correct position,
         * or written one time to its correct position.
         */

        // Given an unsorted array arr[0..n-1] of size n, find the minimum length subarray arr[s..e]
        // such that sorting this subarray makes the whole array sorted.
        // If the input array is [10, 12, 20, 30, 25, 40, 32, 31, 35, 50, 60], your program should
        // be able to find that the subarray lies between the indexes 3 and 8.
        public static void FindMinimumLengthToSort(int[] arr)
        {
            int s, e, i;
            int n = arr.Length;

            // a) Scan from left to right and find the first element which is greater than the next element.
            for (s = 0; s < n - 1; s++)
            {
                if (arr[s] > arr[s + 1])
                    break;
            }
            if (s == n - 1)
                return;// Its sorted already

            // Scan from right to left and find the first element (first in right to left order) 
            // which is smaller than the next element (next in right to left order)
            for (e = n - 1; e > 0; e--)
            {
                if (arr[e] < arr[e - 1])
                    break;
            }

            // Find the minimum and maximum values in arr[s..e]
            int max = arr[s], min = arr[s];
            for (i = s + 1; i <= e; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
                else if (arr[i] < min)
                    min = arr[i];
            }

            // Find the first element (if there is any) in arr[0..s-1] which is greater than min, change s to index of this element
            for (i = 0; i < s; i++)
            {
                if (arr[i] > min)
                {
                    s = i;
                    break;
                }
            }

            // Find the last element (if there is any) in arr[e+1..n-1] which is smaller than max, change e to index of this element
            for (i = n - 1; i > e; i--)
            {
                if (arr[i] < max)
                {
                    e = i;
                    break;
                }
            }
            Console.WriteLine("Minimum index is {0} and {1}", s, e);
        }

        // InsertionSort  O(n*k)
        // Heap Sort O(k) + O((n-k)*logK)
        // Create a Min Heap of size k+1 with first k+1 elements. This will take O(k) time
        // One by one remove min element from heap, put it in result array, and add a new element to heap from remaining elements.
        public static void SortNearlySorted(int[] arr, int n, int k)
        {
            var minHeap = new MinHeap(k + 1); // or create a temp arr with k elements and use in constructor
            for (int i = 0; i <= k; i++)
                minHeap.insert(arr[i]);

            for (int i = k + 1, r = 0; r < n; r++, i++)
            {
                if (i < n)
                    arr[r] = minHeap.ReplaceMin(arr[i]);
                else
                    arr[r] = minHeap.ExtractMin();
            }
        }
    }
}
