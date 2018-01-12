using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    public class ReverseComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return Comparer<T>.Default.Compare(y, x);
        }
    }

    static class Misc
    {
        public static void Swap(ref int x, ref int y)
        {
            if (x == y) // MUST since x/y points to address
                return;
            x = x + y;
            y = x - y;
            x = x - y;
        }

        //https://leetcode.com/problems/trapping-rain-water/solution/
        /*Initialize left pointer to 0 and right pointer to size-1
        While < left<right, do:
            If height[left] is smaller than height[right]
                If height[left]>=left_maxheight[left]>=left_max, update left_maxleft_max
                Else add left_max−height[left]left_max−height[left] to ans
                Add 1 to left.
            Else
                If height[right]>=right_maxheight[right]>=right_max, update right_maxright_max
                Else add right_max−height[right]right_max−height[right] to ans
                Subtract 1 from right.
         * */
        public static void RainWater()
        {
            int[] arr = new int[] { 1, 3, 2, 4, 1, 3, 1, 4, 5, 2, 2, 1, 4, 2, 2 };
            int left = 0, right = arr.Length-1, result = 0, leftmax = 0, rightmax = 0;
            while (left < right)
            {
                if (arr[left] < arr[right])
                {
                    if (arr[left] >= leftmax)
                        leftmax = arr[left];
                    else
                        result += leftmax - arr[left];
                    left++;
                }
                else
                {
                    if (arr[right] >= rightmax)
                        rightmax = arr[right];
                    else
                        result += rightmax - arr[right];
                    right--;
                }
            }

        }

        /*
         * To find lattice points, we basically need to find values of (x, y) which satisfy the equation x2 + y2 = r2.
         * For any value of (x, y) that satisfies the above equation we actually have total 4 different combination which
         * that satisfy the equation. For example if r = 5 and (3, 4) is a pair which satisfies the equation, 
         * there are actually 4 combinations (3, 4) , (-3,4) , (-3,-4) , (3,-4). There is an exception though, 
         * in case of (0, r) or (r, 0) there are actually 2 points as there is no negative 0.
         */
        public static int LatticePoints(int r)
        {
            if (r < 0)
                return 0;

            // Initialize result as 4 for (r, 0), (-r. 0),
            // (0, r) and (0, -r)
            int p = 4;
            for (int i = 1; i < r; i++)
            {
                int y = r*r-i*i;
                int sqrt = (int)Math.Sqrt(y);

                // checking whether square root is an integer
                // or not. Count increments by 4 for four 
                // different quadrant values
                if (sqrt*sqrt == y)
                    p += 4;
            }
            return p;
        }

        public static void TestMinHeap()
        {
            MinHeap h = new MinHeap(11);
            h.insert(3);
            h.insert(2);
            h.Delete(1);
            h.insert(15);
            h.insert(5);
            h.insert(4);
            h.insert(45);
            Console.WriteLine("extracte Min = " + h.ExtractMin());
            Console.WriteLine("Min = " + h.GetMin());
            h.Decrease(2, 1);
            Console.WriteLine("Min = " + h.GetMin());
        }
        
    }
}
