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
            return lcs(s1, s2, s1.Length, s2.Length);

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
                for (int j = 0; j < n; j++)
                    if (j == 0 || i == 0)
                        L[i, j] = 0;
                    else if (s1[i - 1] == s2[j - 1])
                        L[i, j] = 1 + L[i - 1, j - 1];
                    else
                        L[i, j] = Math.Max(L[i, j - 1], L[i - 1, j]);
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
                    i--; j--; index++;
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
    }
}
