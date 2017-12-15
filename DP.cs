using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    static class DP
    {
        //http://www.geeksforgeeks.org/longest-increasing-subsequence/
        // O(N^2)
        public static int LongestIncreasingSubsequence(int[] arr)
        {
            int n = arr.Length;
            int[] lis = new int[n];
            for (int i = 0; i < n; i++)
                lis[i] = 1;

            for (int i = 1; i < n; i++)
                for (int j = 0; j < i; j++)
                    if (arr[i] > arr[j] && lis[i] < lis[j] + 1)
                        lis[i] = lis[j] + 1;
            int max = 0;
            foreach (var s in lis)
                if (max < s)
                    max = s;

            return max;
        }
        public static bool FindSubSequence(string text, string pattern)
        {
            var i = 0;
            foreach (var s in text)
            {
                if (s == pattern[i] && ++i == pattern.Length)
                    return true;
            }
            return false;
        }
        //http://www.geeksforgeeks.org/longest-common-subsequence/
        public static int LongestCommonSubsequence(string s1, string s2)
        {
            // recursive approach O(2^n)
            //return lcs(s1, s2, s1.Length, s2.Length);

            // Tabulated implementation using Dynamic programming O(mn)
            return lcs1(s1, s2, s1.Length, s2.Length);
        }
        private static int lcs(string s1, string s2, int m, int n)
        {
            /*
             * If last characters of both sequences match (or X[m-1] == Y[n-1]) then
                L(X[0..m-1], Y[0..n-1]) = 1 + L(X[0..m-2], Y[0..n-2])

                If last characters of both sequences do not match (or X[m-1] != Y[n-1]) then
                L(X[0..m-1], Y[0..n-1]) = MAX ( L(X[0..m-2], Y[0..n-1]), L(X[0..m-1], Y[0..n-2])
             */
            if (m == 0 || n == 0)
                return 0;
            if (s1[m - 1] == s2[n - 1])
                return 1 + lcs(s1, s2, m - 1, n - 1);
            else
                return Math.Max(lcs(s1, s2, m, n - 1), lcs(s1, s2, m - 1, n));
        }

        private static int lcs1(string s1, string s2, int m, int n)
        {
            int[,] L = new int[m + 1, n + 1];// two dimensional array
            //int [][]L = new int[m+1][];// array of arrays

            /* Following steps build L[m+1][n+1] in bottom up fashion. Note
         that L[i][j] contains length of LCS of X[0..i-1] and Y[0..j-1] */
            for (int i = 0; i <= m; i++)
                for (int j = 0; j <=n; j++)
                    if (j == 0 || i == 0)
                        L[i, j] = 0;
                    else if (s1[i - 1] == s2[j - 1])
                        L[i, j] = 1 + L[i - 1, j - 1];
                    else
                        L[i, j] = Math.Max(L[i-1, j], L[i, j-1]);
            PrintLCS(L, s1, s2, m, n);
            return L[m, n];
        }

        private static void PrintLCS(int[,] L, string s1, string s2, int m, int n)
        {
            int index=L[m,n], initIndex = L[m, n];
            char[] lcs = new char[index];
            int i = m, j = n;
            while (i > 0 && j > 0)
            {
                if (s1[i - 1] == s2[j - 1])
                {
                    lcs[index - 1] = s1[i - 1];
                    i--; j--; index--;
                }
                else if (L[i - 1, j] > L[i, j - 1])
                    i--;
                else
                    j--;
            }
            Console.Write(lcs);
        }

        //Time Complexity: O(n), Extra Space: O(1)
        // Recursion is O(2^n) - fib(n-1)+fib(n-2)
        // Dynamic programming - tabulated implementation stores all sums F[n+1] - space O(n)
        public static int Fibonacci(int n)
        {
            int a = 0, b = 1, c = 0, i;
            if (n == 0)
                return a;
            for (i = 2; i <= n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }
            return b;
        }

        private static int min (int x, int y, int z)
        {
            return Math.Min(x, Math.Min(y, z));
        }

        //http://www.geeksforgeeks.org/dynamic-programming-set-5-edit-distance/
        /*
         * Given two strings str1 and str2 and below operations that can performed on str1. 
         * Find minimum number of edits (operations) required to convert ‘str1’ into ‘str2’.
         */
        public static int EditDistance(string s1, string s2)
        {
            //return EditDistRecur(s1, s2, s1.Length, s2.Length);
            return EditDistDP(s1, s2, s1.Length, s2.Length);
        }

        // Time complexity - exponential
        private static int EditDistRecur(string s1, string s2, int m, int n)
        {
            // If first string is empty, the only option is to
            // insert all characters of second string into first
            if (m == 0)
                return n;

            // If second string is empty, the only option is to
            // remove all characters of first string
            if (n == 0)
                return m;

            // If last characters of two strings are same, nothing
            // much to do. Ignore last characters and get count for
            // remaining strings.
            if (s1[m - 1] == s2[n - 1])
                return EditDistRecur(s1, s2, m - 1, n - 1);

            // If last characters are not same, consider all three
            // operations on last character of first string, recursively
            // compute minimum cost for all three operations and take
            // minimum of three values.
            return 1 + min(EditDistRecur(s1, s2, m, n - 1), // Insert
                            EditDistRecur(s1, s2, m - 1, n), // Remove
                            EditDistRecur(s1, s2, m - 1, n - 1)); // Replace
        }

        //Time Complexity: O(m x n) - Auxiliary Space: O(m x n)
        private static int EditDistDP(string s1, string s2, int m, int n)
        {
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0)
                        dp[i, j] = j;
                    else if (j == 0)
                        dp[i, j] = i;
                    else if (s1[i - 1] == s2[j - 1])
                        dp[i, j] = dp[i - 1, j - 1];
                    else
                        dp[i, j] = 1 + min(dp[i, j - 1],
                                            dp[i - 1, j],
                                            dp[i - 1, j - 1]);
                }
            return dp[m, n];
        }
        //http://www.geeksforgeeks.org/dynamic-programming-set-6-min-cost-path/
        /*
         * Given a cost matrix cost[][] and a position (m, n) in cost[][], write a function that returns cost of
         * minimum cost path to reach (m, n) from (0, 0). 
         * Each cell of the matrix represents a cost to traverse through that cell.
         */
        public static int CostPath(int[,] Cost, int m, int n)
        {
            return CostPathDP(Cost, m, n);
            //return CostPathRecur(Cost, m, n);
        }

        // Cost - exponential
        private static int CostPathRecur(int[,] Cost, int m, int n)
        {
            if (m < 0 || n < 0)
                return int.MaxValue; // High value is key. should be max_int. it avoids taking -ve paths
            if (m == 0 && n == 0)
                return Cost[m, n];
            return Cost[m, n] + min(CostPathRecur(Cost, m - 1, n - 1),
                                CostPathRecur(Cost, m-1, n),
                                CostPathRecur(Cost, m, n-1));
        }

        // O (m*n)
        private static int CostPathDP(int[,] Cost, int m, int n)
        {
            int[,] tCost = new int[m + 1, n + 1];
            int i, j;
            tCost[0, 0] = Cost[0, 0];
            for (i = 1; i <= m; i++)
                tCost[i, 0] = tCost[i - 1, 0] + Cost[i, 0];
            for (j = 1; j <= n; j++)
                tCost[0, j] = tCost[0, j - 1] + Cost[0, j];
            for (i = 1; i <= m; i++)
                for (j = 1; j <= n; j++)
                    tCost[i, j] = Cost[i, j] + min(tCost[i - 1, j - 1], tCost[i, j - 1], tCost[i - 1, j]);
            return tCost[m, n];
        }

        // http://www.geeksforgeeks.org/dynamic-programming-set-7-coin-change/
        /*
         * Given a value N, if we want to make change for N cents, and we have infinite supply of each of S = { S1, S2, .. , Sm}
         * valued coins, how many ways can we make the change? The order of coins doesn’t matter.
         */
        public static int CoinChange(int[] S, int n)
        {
            //return CoinChangeRecur(S, S.Length, n);
            return CoinChangeDP(S, S.Length, n);
        }

        private static int CoinChangeRecur(int[] S, int m, int n)
        {
            // If n is 0 then there is 1 solution (do not include any coin)
            if (n == 0)
                return 1;
            // If n is less than 0 then no solution exists
            if (n < 0)
                return 0;
            // If there are no coins and n is greater than 0, then no solution exist
            if (m <= 0 && n > 0)
                return 0;
            // count is sum of solutions (i) including S[m-1] (ii) excluding S[m-1]
            return CoinChangeRecur(S, m - 1, n) + CoinChangeRecur(S, m, n - S[m - 1]);
        }

        // O(m*n)
        private static int CoinChangeDP(int[] S, int m, int n)
        {
            if (m <= 0)
                return 0;

            // We need n+1 rows as the table is consturcted in bottom up manner using 
            // the base case 0 value case (n = 0)
            int[,] table = new int[n + 1, m];
            int i, j, x, y;

            // Fill the enteries for 0 value case (n = 0)
            for (i = 0; i < m; i++)
                table[0, i] = 1;

            for (i = 1; i <= n; i++)
                for (j = 0; j < m; j++)
                {
                    // Count of solutions excluding S[j]
                    x = j > 0 ? table[i, j - 1] : 0;

                    // Count of solutions including S[j]
                    y = i - S[j] >= 0 ? table[i - S[j], j] : 0;
                    table[i, j] = x + y;
                }
            return table[n, m - 1];
        }
        //http://www.geeksforgeeks.org/dynamic-programming-set-11-egg-dropping-puzzle/
        //https://en.wikipedia.org/wiki/Dynamic_programming#Egg_dropping_puzzle
        /*
         * When we drop an egg from a floor x, there can be two cases (1) The egg breaks (2) The egg doesn’t break.
           1) If the egg breaks after dropping from xth floor, then we only need to check for floors lower than x with remaining eggs;
         * so the problem reduces to x-1 floors and n-1 eggs
           2) If the egg doesn’t break after dropping from the xth floor, then we only need to check for floors higher than x;
         * so the problem reduces to k-x floors and n eggs.
         * eggDrop(n, k) = 1 + min{max(eggDrop(n - 1, x - 1), eggDrop(n, k - x)): 
                 x in {1, 2, ..., k}}
         * 
         */
        public static int EggDrop(int n, int k)
        {
            //return EggDropRecur(n, k);
            return EggDropDP(n, k);
        }
        private static int EggDropRecur(int n, int k)
        {
            // If there are no floors, then no trials needed. OR if there is
            // one floor, one trial needed.
            if (k == 0 || k == 1)
                return k;
            
            // We need k trials for one egg and k floors
            if (n == 1)
                return k;
            int min = int.MaxValue, result;

            // Consider all droppings from 1st floor to kth floor and
            // return the minimum of these values plus 1.
            for (int x = 1; x <= k; x++)
            {
                result = Math.Max(EggDropRecur(n - 1, x - 1), EggDropRecur(n, k - x));
                if (result < min)
                    min = result;
            }
            return min + 1;
        }

        /*
         * Notice that the this solution takes O(nk^2) time with a DP solution. 
         * This can be improved to O(nk*log k) time by binary searching on the optimal x in the above recurrence, 
         * since W(n-1,x-1) is increasing in x while W(n,k-x) is decreasing in x, 
         * thus a local minimum of max(W(n-1,x-1),W(n,k-x)) is a global minimum. 
         * Also, by storing the optimal x for each cell in the DP table and referring to its value for the previous cell, 
         * the optimal x for each cell can be found in constant time, improving it to O(nk) time.
         */
        private static int EggDropDP(int n, int k)
        {
            /* A 2D table where entery eggFloor[i][j] will represent minimum
                number of trials needed for i eggs and j floors. */
            int[,] trials = new int[n + 1, k + 1];
            // We need one trial for one floor and0 trials for 0 floors
            for (int i = 0; i <= n; i++)
            {
                trials[i, 0] = 0;
                trials[i, 1] = 1;
            }

            // We always need j trials for one egg and j floors.
            for (int j = 1; j <= k; j++)
            {
                trials[1, j] = j;
            }
            int result;
            // Fill rest of the entries in table using optimal substructure
            // property
            for (int i=2; i<=n; i++)
                for (int j=2; j<=k;j++)
                {
                    trials[i, j] = int.MaxValue;
                    for (int x = 1; x <= j; x++)
                    {
                        result = 1 + Math.Max(trials[i - 1, x - 1], trials[i, j - x]);
                        if (result < trials[i, j])
                            trials[i, j] = result;
                    }
                }

            return trials[n, k];
        }
        public static int TilingProblemNByM(int n, int m)
        {
            int[] count = new int[n + 1];
            count[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                if (i > m)
                    count[i] = count[i - 1] + count[i - m];
                else if (i < m)
                    count[i] = 1;
                else
                    count[i] = 2;
            }
            return count[n];
        }

    }
}