using System;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace Geeks.Bit
{
    /*
     * (n & (n-1)) - unset the righmost set bit
     * ((1 << n) - 1) - Sets one bit in n lsb bits
     * n & (-n) - Sets only the rightmost bit
     */
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
            int x, sum = 0;

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
        private static uint countSetBitsofInteger(int n)
        {
            /*
             * Subtraction of 1 from a number toggles all the bits (from right to left) till the rightmost
             * set bit(including the righmost set bit). So if we subtract a number by 1 and do bitwise & with itself
             * (n & (n-1)), we unset the righmost set bit.
             */
            uint count = 0;
            while (n > 0)
            {
                n &= (n - 1);
                count++;
            }
            return count;
        }

        //http://www.geeksforgeeks.org/swap-bits-in-a-given-number/
        public static int SwapBits(int x, int p1, int p2, int n)
        {
            /*
             * 1) Move all bits of first set to rightmost side
   set1 =  (x >> p1) & ((1U << n) - 1)
Here the expression (1U << n) - 1 gives a number that 
contains last n bits set and other bits as 0. We do & 
with this expression so that bits other than the last 
n bits become 0.
2) Move all bits of second set to rightmost side
   set2 =  (x >> p2) & ((1U << n) - 1)
3) XOR the two sets of bits
   xor = (set1 ^ set2) 
4) Put the xor bits back to their original positions. 
   xor = (xor << p1) | (xor << p2)
5) Finally, XOR the xor with original number so 
   that the two sets are swapped.
   result = x ^ xor
             */
            // xor contains xor of two sets
            int xor = ((x >> p1) ^ (x >> p2)) & ((1 << n) - 1);

            //To swap two sets, we need to again XOR the xor with original sets
            return x ^ ((xor << p1) | (xor << p2));
        }

        //http://www.geeksforgeeks.org/add-two-numbers-without-using-arithmetic-operators/
        public static int AddWithBits(int x, int y)
        {
            // Sum of two bits can be obtained by performing XOR (^) of the two bits. 
            // Carry bit can be obtained by performing AND (&) of two bits.
            if (y == 0)
                return x;
            return AddWithBits(x ^ y, (x & y) << 1);
        }

        private static int min(int x, int y)
        {
            return y + ((x - y) & ((x - y) >> 31));
        }

        private static int max(int x, int y)
        {
            return x - ((x - y) & ((x - y) >> 31));
        }

        //http://www.geeksforgeeks.org/smallest-of-three-integers-without-comparison-operators/
        public static int Smallest(int x, int y, int z)
        {
            //return min(x, min(y, z));
            if (y / x == 0) { return (y / z == 0) ? y : z; }
            return (x / z == 0) ? x : z;
        }

        public static void changeToZero()
        {
            // C++ int a[2]
            int[] a = { 0, 1 };
            a[a[1]] = a[a[0]];
        }
        public static int NextHigherNumberWithSameBits(int x)
        {
            // right most set bit
            int rightBit = x & (-x);
            // reset the pattern and set next higher bit
            // left part of x will be here
            int higherBit = x + rightBit;

            // isolate the pattern
            int rightPattern = x ^ higherBit;
            // right adjust pattern
            rightPattern = rightPattern / rightBit;
            // correction factor
            rightPattern >>= 2;
            // integrate new pattern (Add [D] and [A])
            return higherBit | rightPattern;
        }
        public static bool isPowerof4(int x)
        {
            if (x == 0)
                return false;
            while (x != 1)
            {
                if (x % 4 != 0) return false;
                x = x / 4;
            }
            return true;
        }
        public static int Multiplywith3point5(int x)
        {
            return x << 1 + x + x >> 1;
        }
        /*
         * http://www.geeksforgeeks.org/compute-modulus-division-by-a-power-of-2-number/
         * Compute n modulo d without division(/) and modulo(%) operators, where d is a power of 2 number.
            Let ith bit from right is set in d. For getting n modulus d, 
         * we just need to return 0 to i-1 (from right) bits of n as they are and other bits as 0.
         */
        public static uint getModulo(uint n, uint d)
        {
            return (n & (d - 1));
        }

        public static int LeftRotate(int x, int n)
        {
            /* In n<<d, last d bits are 0. To put first 3 bits of n at 
            last, do bitwise or of n<<d with n >>(INT_BITS - d) */
            return ((x << n) | (x >> (32 - n)));
            //return ((x >> n) | (x << (32 - n))); Right rotate
        }

        public static void get2NonRepeatingNos(int[] arr)
        {
            int n = arr.Length;
            int xor = 0;
            int x = 0, y = 0;
            foreach (var elem in arr)
            {
                xor ^= elem;
            }
            int rightBit = xor & (-xor);
            foreach (var elem in arr)
            {
                if ((elem & rightBit) != 0)
                    x ^= elem;
                else
                    y ^= elem;
            }
        }
        public static bool CheckOverflow(int a, int b)
        {
            // Best solution: 
            //if (a > INT_MAX - b) return true;

            // Another one:
            // There can be overflow only if signs of two numbers are same, and sign of sum is opposite to the
            // signs of numbers.
            int sum = a + b;
            if (a > 0 && b > 0 && sum < 0)
                return true;
            if (a < 0 && b < 0 && sum > 0)
                return true;
            return false;
        }
        public static uint reverseBits(uint num)
        {
            int reverse_num = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((num & (1 << i)) != 0)
                {
                    reverse_num |= 1 << (31 - i);
                }
            }
            return (uint)reverse_num;
        }
        public static int NextPowerOf2(int n)
        {
            // N is not zero and there is only one bit in n (meaning n is a power of 2)
            if (n != 0 && ((n & (n - 1)) == 0))
            {
                return n;
            }
            int num = 1;
            while (num < n)
            {
                num <<= 1;
            }
            return num;
        }

        public static bool isMultipleof3(int n)
        {
            /* Make no positive if +n is multiple of 3
       then is -n. We are doing this to avoid
       stack overflow in recursion*/
            if (n < 0)
                n = -n;
            if (n == 0)
                return true;
            if (n == 1)
                return false;
            int oddCount = 0, evenCount = 0;
            while (n > 0)
            {
                if ((n & 1) != 0)
                    oddCount++;
                n >>= 1;
                if ((n & 1) != 0)
                    evenCount++;
                n >>= 1;
            }
            return isMultipleof3(oddCount - evenCount);
        }

        public static bool CheckParity(int n)
        {
            // returns true if n has odd parity and false if even parity
            bool parity = false;
            while (n > 0)
            {
                parity = !parity;
                n = n & (n - 1);
            }
            return parity;
        }
        public static int multiplyBySeven(int n)
        {
            // 7n = 8n-n (n <<3 is 8n)
            return ((n << 3) - n);
        }
        public static uint getFirstSetBitPos(int n)
        {
            // Log2 will give zero based index. n&-n will get the first set bit
            return (uint)Math.Log(n & -n, 2);
        }
        public static void BinaryRep(int n)
        {
            if (n > 1)
                BinaryRep(n / 2);
            // Finds odd/even
            Console.Write(n % 2);
        }
        public static uint swapBits(uint x)
        {
            // Get all even bits of x
            uint even_bits = x & 0xAAAAAAAA;

            // Get all odd bits of x
            uint odd_bits = x & 0x55555555;

            even_bits >>= 1;  // Right shift even bits
            odd_bits <<= 1;   // Left shift odd bits

            return (even_bits | odd_bits); // Combine even and odd bits
        }
        public static void Swap(int x, int y)
        {
            x = x + y; // x^y
            y = x - y; // x^y
            x = x - y; // x^y
        }
        public static int swapNibbles(int x)
        {
            return ((x & 0x0F) << 4 | (x & 0xF0) >> 4);
        }
        public static int turnOffK(int n, int k)
        {
            // k must be greater than 0
            if (k <= 0) return n;

            // Do & of n with a number with all set bits except
            // the k'th bit
            return (n & ~(1 << (k - 1)));
        }
        private static bool IsKthBitSet(int x, int k)
        {
            return ((x & (1 << (k - 1))) != 0);
        }
        public static bool IsPaliandrome(int x)
        {
            int left = 32, right = 1;
            while (right < left)
            {
                if (IsKthBitSet(x, left) != IsKthBitSet(x, right))
                    return false;
                left--; right++;
            }
            return true;
        }
        
    }
}
