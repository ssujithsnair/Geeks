using Marshal = System.Runtime.InteropServices.Marshal;

namespace Geeks.Bit
{
	public static class Bitwise
	{
		static int INT_SIZE = 32;
		//int n = sizeof(arr) / sizeof(arr[0]); //C++
		//int n = Marshal.SizeOf(arr) / Marshal.SizeOf(arr[0]); //C#

		// element that appears once
		// http://www.geeksforgeeks.org/find-the-element-that-appears-once/
		//Time Complexity: O(n)		Auxiliary Space: O(1)
        public static int ElementThatAppearsOnce(int[] arr)
		{
			int n = arr.Length;

			// Initialize result
			int result = 0;

			int x, sum;

			// Iterate through every bit
			for (int i = 0; i < INT_SIZE; i++)
			{
				// Find sum of set bits at ith position in all
				// array elements
				sum = 0;
				x = (1 << i);
				for (int j = 0; j < n; j++)
				{
					if ((arr[j] & x) != 0)
						sum++;
				}

				// The bits with sum not multiple of 3, are the
				// bits of element with single occurrence.
				if ((sum % 3) == 1)
					result |= x;
			}

			return result;
		}

		// Detect if two integers have opposite signs
		// http://www.geeksforgeeks.org/detect-if-two-integers-have-opposite-signs/
		public static bool oppositeSigns(int x, int y)
		{
			// XOR of x and y will be negative number number if x and y have opposite signs
			return ((x ^ y) >> 31) != 0;
			//return ((x ^ y) < 0);
			//return (x < 0)? (y >= 0): (y < 0);
		}

        //http://www.geeksforgeeks.org/count-total-set-bits-in-all-numbers-from-1-to-n/
        //Given a positive integer n, count the total number of set bits in binary representation of all numbers from 1 to n.
		public static int countSetBits(int n)
		{
			int x, sum =0;

			// Iterate through every bit
			for (int i = 0; i < INT_SIZE; i++)
			{
				// Find sum of set bits at ith position in all
				// array elements
				x = (1 << i);
				for (int j = 1; j <= n; j++)
				{
					if ((j & x) != 0)
						sum++;
				}
			}
			return sum;
		}

        public static int SwapBits(int x, int p1, int p2, int n) 
        {
            // xor contains xor of two sets
            int xor = ((x >> p1) ^ (x >> p2)) & ((1 << n) - 1);

            //To swap two sets, we need to again XOR the xor with original sets
            return x ^ ((xor << p1) | (xor << p2));
        }
	}
}
