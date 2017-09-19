using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    static class Misc
    {
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
        
    }
}
