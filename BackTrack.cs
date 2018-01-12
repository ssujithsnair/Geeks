using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    class BackTrack
    {
        public static void Test()
        {
            var b = new BackTrack();
            b.patternMatch("GeeksForGeeks", "GfG", 13, 3);
            b.RemoveInvalidParanthesis("((()))(");
        }
        
        private static bool IsParanthesis(char c)
        {
            return c == '(' || c == ')';
        }
        private static bool IsValidString(string s)
        {
            int count = 0;
            foreach (char c in s)
            {
                if (c == '(')
                    count++;
                else if (c == ')')
                    count--;
                if (count < 0)
                    return false;
            }
            return count == 0;
        }

        public void RemoveInvalidParanthesis(string str)
        {
            Queue<string> q = new Queue<string>();
            HashSet<string> map = new HashSet<string>();
            q.Enqueue(str);
            map.Add(str);
            bool deep = true;
            while (q.Count > 0)
            {
                str = q.Dequeue();
                if (IsValidString(str))
                {
                    Console.WriteLine(str);
                    deep = false;
                }
                if (!deep)
                    continue;
                for (int i = 0; i < str.Length; i++)
                {
                    if (!IsParanthesis(str[i]))
                        continue;
                    string sub = str.Substring(0, i) + str.Substring(i + 1);
                    if (!map.Contains(sub))
                    {
                        q.Enqueue(sub);
                        map.Add(sub);
                    }
                }
            }
        }

        bool patternMatchUtil(string str, int n, int i,
                    string pat, int m, int j,
                    Dictionary<char, string> map)
        {
            // If both string and pattern reach their end
            if (i == n && j == m)
                return true;

            // If either string or pattern reach their end
            if (i == n || j == m)
                return false;

            // read next character from the pattern
            char ch = pat[j];

            // if character is seen before
            if (map.ContainsKey(ch))
            {
                string s = map[ch];
                int len = s.Length;

                if (i + len > str.Length)
                    return false;
                // consider next len characters of str
                string subStr = str.Substring(i, len);

                // if next len characters of string str
                // don't match with s, return false
                if (subStr != s)
                    return false;

                // if it matches, recurse for remaining characters
                return patternMatchUtil(str, n, i + len, pat, m,
                                                    j + 1, map);
            }

            // If character is seen for first time, try out all
            // remaining characters in the string
            for (int len = 1; len <= n - i; len++)
            {
                // consider substring that starts at position i
                // and spans len characters and add it to map
                map[ch] = str.Substring(i, len);

                // see if it leads to the solution
                if (patternMatchUtil(str, n, i + len, pat, m,
                                                  j + 1, map))
                    return true;

                // if not, remove ch from the map
                map.Remove(ch);
            }

            return false;
        }

        // A wrapper over patternMatchUtil()function
        bool patternMatch(string str, string pat, int n, int m)
        {
            if (n < m)
                return false;

            // create an empty hashmap
            Dictionary<char, string> map = new Dictionary<char, string>();

            // store result in a boolean variable res
            bool res = patternMatchUtil(str, n, 0, pat, m, 0, map);

            // if solution exists, print the mappings
            foreach (var key in map.Keys)
                Console.WriteLine(key + "->" + map[key]); ;

            // return result
            return res;
        }
    }
}
