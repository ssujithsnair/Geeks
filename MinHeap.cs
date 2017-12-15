using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    /*
     * A quick look over the above algorithm suggests that the running time is O(nlg(n)), 
     * since each call to Heapify costs O(lg(n)) and Build-Heap makes O(n) such calls.
     * This upper bound, though correct, is not asymptotically tight. Heapify takes different time for each node, which is O(h)
     * A heap of size n has at most n/(2^h+1) nodes with height h.
     * Hence Proved that the Time complexity for Building a Binary Heap is O(n)
     */
    class MinHeap
    {
        int size;
        int[] arr;
        readonly int maxsize;
        public MinHeap(int capacity)
        {
            maxsize = capacity;
            arr = new int[maxsize];
            size = 0;
        }

        public MinHeap(int[] inputArr)
        {
            size = maxsize = inputArr.Length;
            arr = inputArr;
            int i = (size - 1) / 2;
            while (i >= 0)
                Heapify(i--);
        }

        int parent(int i) { return (i - 1) / 2; }
        int left(int i) { return 2 * i + 1; }
        int right(int i) { return 2 * i + 2; }
        public int GetMin() { return arr[0]; }
        public void insert(int k)
        {
            if (maxsize == size)
                return;
            // First insert the new key at the end
            int i = size;
            arr[i] = k;
            size++;

            FixMinHeap(i);
        }

        private void FixMinHeap(int i)
        {
            // Fix the min heap property if it is violated
            while (i != 0 && arr[parent(i)] > arr[i])
            {
                Misc.Swap(ref arr[i], ref arr[parent(i)]);
                i = parent(i);
            }
        }

        // Decreases value of key at index 'i' to newval.  It is assumed that
        // newval is smaller than arr[i].
        public void Decrease(int i, int newval)
        {
            arr[i] = newval;
            FixMinHeap(i);
        }

        public int ReplaceMin(int val)
        {
            int root = arr[0];
            arr[0] = val;
            Heapify(0);
            return root;
        }

        public int ExtractMin()
        {
            if (size == 0)
                return int.MinValue;
            if (size == 1)
            {
                size--;
                return arr[0];
            }

            // Store the minimum value, and remove it from heap
            int root = arr[0];
            arr[0] = arr[--size];
            Heapify(0);
            return root;
        }
        // This function deletes key at index i. It first reduced value to minus
        // infinite, then calls extractMin()
        public void Delete(int i)
        {
            Decrease(i, int.MinValue);
            ExtractMin();
        }
        
        // A recursive method to heapify a subtree with root at given index
        // This method assumes that the subtrees are already heapified
        public void Heapify(int i)
        {
            int l = left(i);
            int r = right(i);
            int smallest = i;
            if (l < size && arr[l] < arr[i])
                smallest = l;
            if (r < size && arr[r] < arr[smallest])
                smallest = r;
            if (smallest == i)
                return;
            Misc.Swap(ref arr[i], ref arr[smallest]);
            Heapify(smallest);
        }
    }

    class MinHeapNode
    {
        public int v;
        public int dist;
        public MinHeapNode(int v, int dist)
        {
            this.v = v;
            this.dist = dist;
        }
        public static bool operator <(MinHeapNode a, MinHeapNode b)
        {
            return a.dist < b.dist;
        }
        public static bool operator >(MinHeapNode a, MinHeapNode b)
        {
            return a.dist > b.dist;
        }
    }
    
    class MinHeapWithNode
    {

        int size;
        MinHeapNode[] arr;
        readonly int maxsize;
        public MinHeapWithNode(int capacity)
        {
            maxsize = capacity;
            arr = new MinHeapNode[maxsize];
            size = 0;
        }

        //public MinHeapWithNode(int[] inputArr)
        //{
        //    size = maxsize = inputArr.Length;
        //    arr = inputArr;
        //    int i = (size - 1) / 2;
        //    while (i >= 0)
        //        Heapify(i--);
        //}

        int parent(int i) { return (i - 1) / 2; }
        int left(int i) { return 2 * i + 1; }
        int right(int i) { return 2 * i + 2; }
        public MinHeapNode GetMin() { return arr[0]; }
        public void insert(MinHeapNode k)
        {
            if (maxsize == size)
                return;
            // First insert the new key at the end
            int i = size;
            arr[i] = k;
            size++;

            FixMinHeap(i);
        }
        
        public bool IsEmpty()
        {
            return size == 0;
        }

        private void FixMinHeap(int i)
        {
            // Fix the min heap property if it is violated
            while (i != 0 && arr[parent(i)] > arr[i])
            {
                Misc.Swap(ref arr[i].dist, ref arr[parent(i)].dist);
                i = parent(i);
            }
        }

        // Decreases value of key at index 'i' to newval.  It is assumed that
        // newval is smaller than arr[i].
        public void Decrease(int i, MinHeapNode newval)
        {
            arr[i] = newval;
            FixMinHeap(i);
        }

        public MinHeapNode ReplaceMin(MinHeapNode val)
        {
            var root = arr[0];
            arr[0] = val;
            Heapify(0);
            return root;
        }

        public MinHeapNode ExtractMin()
        {
            if (size == 0)
                return null;
            if (size == 1)
            {
                size--;
                return arr[0];
            }

            // Store the minimum value, and remove it from heap
            var root = arr[0];
            arr[0] = arr[--size];
            Heapify(0);
            return root;
        }
        // This function deletes key at index i. It first reduced value to minus
        // infinite, then calls extractMin()
        public void Delete(int i)
        {
            Decrease(i, new MinHeapNode(-1, int.MinValue));
            ExtractMin();
        }

        // A recursive method to heapify a subtree with root at given index
        // This method assumes that the subtrees are already heapified
        public void Heapify(int i)
        {
            int l = left(i);
            int r = right(i);
            int smallest = i;
            if (l < size && arr[l] < arr[i])
                smallest = l;
            if (r < size && arr[r] < arr[smallest])
                smallest = r;
            if (smallest == i)
                return;
            Misc.Swap(ref arr[i].dist, ref arr[smallest].dist);
            Heapify(smallest);
        }
    }
}
