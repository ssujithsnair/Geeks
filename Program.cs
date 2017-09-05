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
		static void Main(string[] args)
		{
            int x = 5;
            var p1 = x & (-x);
			int [] arr = { 3, 3, 2, 3 };
			var p = System.Runtime.InteropServices.Marshal.SizeOf(arr[0]);
            int val = Bitwise.ElementThatAppearsOnce(arr);

			val = Bitwise.countSetBits(17);

            val = Bitwise.SwapBits(47, 1, 5, 3);
        }
	}
}
