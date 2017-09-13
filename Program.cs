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
        static void Main(string[] args)
        {
            //Bit();
            DyP();
        }
    }
}
