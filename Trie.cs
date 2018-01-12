using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    class Trie
    {
        const int AlphabetSize = 26;
        TrieNode root = new TrieNode();
        class TrieNode
        {
            public bool IsEndOfNode = false;
            public string PhoneNumber;
            public TrieNode[] Children = new TrieNode[AlphabetSize];
        }

        bool Insert(string key, string phone = null)
        {
            key = key.ToLower();
            TrieNode next = root;
            foreach (var c in key)
            {
                var index = c - 'a';

                // Validate the input character
                if (index < 0 || index >= next.Children.Length)
                    return false;
                if (next.Children[index] == null)
                    next.Children[index] = new TrieNode();
                next = next.Children[index];
            }
            next.IsEndOfNode = true;
            next.PhoneNumber = phone;
            return true;
        }

        private TrieNode GetNode(string key)
        {
            key = key.ToLower();
            TrieNode next = root;
            foreach (var c in key)
            {
                var index = c - 'a';

                // Validate the input character
                if (index < 0 || index >= next.Children.Length)
                    return null;
                if (next.Children[index] == null)
                    return null;
                next = next.Children[index];
            }
            return next;
        }

        // Learn word break printing solution
        bool WordBreak(string split)
        {
            int n = split.Length;
            bool[] match = new bool[n];
            match[0] = true;
            for (int i = 0; i < n; i++)
            {
                if (!match[i])
                    continue;
                TrieNode next = root;
                for (int j = i; j < n; j++)
                {
                    if (next == null)
                        break;
                    next = next.Children[split[j] - 'a'];
                    if (next != null && next.IsEndOfNode)
                        match[j + 1] = true;
                }
            }
            return match[n];
        }

        public Dictionary<string, string> GetMatchingPeople(string namePrefix)
        {
            Dictionary<string, string> people = new Dictionary<string, string>();
            var node = GetNode(namePrefix);
            if (node == null)
                return people;
            GetAllMatchingChildren(namePrefix, node, people);

            return people;
        }

        private void GetAllMatchingChildren(string prefix, TrieNode node, Dictionary<string, string> people)
        {
            if (node.IsEndOfNode)
                people.Add(prefix, node.PhoneNumber);
            for (int i = 0; i < AlphabetSize; i++)
            {
                var next = node.Children[i];
                if (next == null)
                    continue;
                GetAllMatchingChildren(prefix + (char)('a' + i), next, people);
            }

        }
        public bool Search(string fullName)
        {
            var node = GetNode(fullName);
            return node != null && node.IsEndOfNode;
        }

        public string GetPhoneNumber(string fullname)
        {
            var node = GetNode(fullname);
            return node != null && node.IsEndOfNode ? node.PhoneNumber : null;
        }

        public static void Test()
        {
            TestPrefixMatch();
            PhoneBookTest();
        }

        public static void TestPrefixMatch()
        {
            string[] keys = {"the", "a", "there", "answer", "any",
                         "by", "bye", "their"};
            var trie = new Trie();
            foreach (var key in keys)
            {
                trie.Insert(key);
            }
            var b = trie.Search("bye");
            b = trie.Search("answer");
            b = trie.Search("answe");
        }

        private static void PhoneBookTest()
        {
            var trie = new Trie();
            trie.Insert("An", "201-222-1119");
            trie.Insert("Antony", "201-222-1111");
            trie.Insert("Angalo", "201-222-1112");
            trie.Insert("Abraham", "201-222-1113");
            trie.Insert("Abcd", "201-222-1114");
            trie.Insert("Nepali", "201-222-1115");
            trie.Insert("NavGlobalHead", "201-222-1116");
            trie.Insert("ZenLoser", "201-222-1117");
            trie.Insert("SujithTheMan", "201-222-1118");
            var people = trie.GetMatchingPeople("");
        }
    }
    class TrieWithDict
    {
        const int AlphabetSize = 26;
        TrieNode root = new TrieNode();
        class TrieNode
        {
            public bool IsEndOfNode = false;
            public string PhoneNumber;
            public Dictionary<char, TrieNode> Children = new Dictionary<char,TrieNode>();
            public List<string> StartsWith = new List<string>();
        }
        public TrieWithDict() { }
        #region WordSquare
        public TrieWithDict(String[] words) {
            foreach(string w in words) {
                Insert(w, null, true);
            }
        }

        private List<String> findByPrefix(String prefix) {
            List<String> ans = new List<string>();
            var node = GetNode(prefix);
            if (node == null)
                return ans;
            ans.AddRange(node.StartsWith);
            return ans;
        }

        public List<List<String>> wordSquares(String[] words)
        {
            List<List<String>> ans = new List<List<string>>();
            if (words == null || words.Length == 0)
                return ans;
            int len = words[0].Length;
            TrieWithDict trie = new TrieWithDict(words);
            List<String> ansBuilder = new List<string>();
            foreach (String w in words)
            {
                ansBuilder.Add(w);
                search(len, ans, ansBuilder);
                ansBuilder.RemoveAt(ansBuilder.Count - 1);
            }
            return ans;
        }

        private void search(int len, List<List<String>> ans,
                List<String> ansBuilder) {
        if (ansBuilder.Count == len) {
            ans.Add(new List<string>(ansBuilder));
            return;
        }

        int idx = ansBuilder.Count;
        StringBuilder prefixBuilder = new StringBuilder();
        foreach(String s in ansBuilder)
            prefixBuilder.Append(s[idx]);
        List<String> startWith = findByPrefix(prefixBuilder.ToString());
        foreach (String sw in startWith) {
            ansBuilder.Add(sw);
            search(len, ans, ansBuilder);
            ansBuilder.RemoveAt(ansBuilder.Count - 1);
        }
    }
        #endregion
        bool Insert(string key, string phone=null, bool starts=false)
        {
            //key = key.ToLower();
            TrieNode next = root;
            foreach (var c in key)
            {
                TrieNode node;
                if (!next.Children.ContainsKey(c))
                    next.Children.Add(c, node = new TrieNode());
                else
                    node = next.Children[c];
                node.StartsWith.Add(key);
                next = node;
            }
            next.IsEndOfNode = true;
            next.PhoneNumber = phone;
            return true;
        }

        private TrieNode GetNode(string key)
        {
            key = key.ToLower();
            TrieNode next = root;
            foreach (var c in key)
            {
                if (!next.Children.ContainsKey(c))
                    return null;
                next = next.Children[c];
            }
            return next;
        }
        void WordBreak(string str, string[] dict)
        {
            WordBreak(str, dict, "");
        }
        void WordBreak(string str, string[] dict, string result)
        {
            for (int i = 1; i <= str.Length; i++)
            {
                string prefix = str.Substring(0, i);
                if (dict.Contains(prefix))
                {
                    if (i == str.Length)
                    {
                        Console.WriteLine(result + prefix);
                        return;
                    }
                    WordBreak(str.Substring(i, str.Length - i), dict, result + prefix + " ");
                }
            }
        }
        // Learn word break printing solution
        bool WordBreak(string split)
        {
            int n = split.Length;
            bool[] match = new bool[n+1];
            match[0] = true;
            for (int i = 0; i < n; i++)
            {
                if (!match[i])
                    continue;
                TrieNode next = root;
                for (int j = i; j < n; j++)
                {
                    if (next == null)
                        break;
                    if (next.Children.ContainsKey(split[j]))
                    {
                        next = next.Children[split[j]];
                        if (next.IsEndOfNode)
                            match[j + 1] = true;
                    }
                }
            }
            if (!match[n])
                return false;
            string str = string.Empty;
            bool started = false;
            for (int i = 1; i < n; i++)
            {
                str += split[i-1];
                if (match[i] && !match[i + 1])
                {
                    Console.WriteLine(str);
                    str = string.Empty;
                }
            }
            return true;
        }

        public Dictionary<string, string> GetMatchingPeople(string namePrefix)
        {
            Dictionary<string, string> people = new Dictionary<string, string>();
            var node = GetNode(namePrefix);
            if (node == null)
                return people;
            GetAllMatchingChildren(namePrefix, node, people);

            return people;
        }

        private void GetAllMatchingChildren(string prefix, TrieNode node, Dictionary<string, string> people)
        {
            if (node.IsEndOfNode)
                people.Add(prefix, node.PhoneNumber);
            foreach(var key in node.Children.Keys)
            {
                var next = node.Children[key];
                if (next == null)
                    continue;
                GetAllMatchingChildren(prefix + key, next, people);
            }

        }
        public bool Search(string fullName)
        {
            var node = GetNode(fullName);
            return node != null && node.IsEndOfNode;
        }

        public string GetPhoneNumber(string fullname)
        {
            var node = GetNode(fullname);
            return node != null && node.IsEndOfNode ? node.PhoneNumber : null;
        }

        int m, n;
        private bool IsValidNeighbor(int i, int j, bool[,] marked)
        {
            return ( i>=0 && j >=0&& i < m && j < n && !marked[i,j]);
        }

        private void SearchBoggle(TrieNode trie, bool[,] marked, char[,] boggle, int i, int j, string prefix)
        {
            if (trie.IsEndOfNode)
                Console.WriteLine(prefix);
            
            marked[i, j] = true;

            foreach (char c in trie.Children.Keys)
            {
                TrieNode next = trie.Children[c];
                ///var neighbors = GetNeighbors(i, j);
                foreach (var neighbor in GetNeighborsY(i,j))
                {
                    int a = neighbor.Item1;
                    int b = neighbor.Item2;
                    if (IsValidNeighbor(a, b, marked) && boggle[a, b] == c)
                        SearchBoggle(next, marked, boggle, a, b, prefix + c);
                }
            }
            marked[i, j] = false;
        }

        private static IEnumerable<Tuple<int, int>> GetNeighborsY(int i, int j)
        {
            yield return new Tuple<int, int> (i, j+1);
            yield return new Tuple<int, int> (i, j-1);
            yield return new Tuple<int, int> (i+1, j);
            yield return new Tuple<int, int> (i-1, j);
            yield return new Tuple<int, int> (i+1, j+1);
            yield return new Tuple<int, int> (i+1, j-1);
            yield return new Tuple<int, int> (i-1, j+1);
            yield return new Tuple<int, int>(i - 1, j - 1);
        }

        private static List<Tuple<int, int>> GetNeighbors(int i, int j)
        {
            return new List<Tuple<int, int>>() { 
                new Tuple<int, int> (i, j+1),
                new Tuple<int, int> (i, j-1),
                new Tuple<int, int> (i+1, j),
                new Tuple<int, int> (i-1, j),
                new Tuple<int, int> (i+1, j+1),
                new Tuple<int, int> (i+1, j-1),
                new Tuple<int, int> (i-1, j+1),
                new Tuple<int, int> (i-1, j-1),
            };
        }

        private void FindWords(char[,] boggle)
        {
            m = boggle.GetLength(0);
            n= boggle.GetLength(1);
            bool[,] marked = new bool[m, n];
            for(int i=0; i< m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (root.Children.ContainsKey(boggle[i, j]))
                    {
                        SearchBoggle(root.Children[boggle[i, j]], marked, boggle, i, j, boggle[i,j].ToString());
                    }
                }
            }
        }

        public static void Test()
        {
            WordSquareTest();
            //TestPrefixMatch();
            //PhoneBookTest();
            //BoggleTest();
            //WordBreakTest();
        }

        private static void WordBreakTest()
        {
            string[] keys = {"the", "a", "there", "answer", "any",
                         "by", "bye", "their"};
            var trie = new TrieWithDict();
            foreach (var key in keys)
            {
                trie.Insert(key);
            }
            trie.WordBreak("abyetheir", keys);
        }

        public static void TestPrefixMatch()
        {
            string[] keys = {"the", "a", "there", "answer", "any",
                         "by", "bye", "their"};
            var trie = new TrieWithDict();
            foreach (var key in keys)
            {
                trie.Insert(key);
            }
            var b = trie.Search("bye");
            b = trie.Search("answer");
            b = trie.Search("answe");
        }

        private static void WordSquareTest()
        {
            var words = new string[] { "area", "lead", "wall", "lady", "ball" };
            TrieWithDict trie = new TrieWithDict(words);
            var squares = trie.wordSquares(words);
        }
        private static void PhoneBookTest()
        {
            var trie = new TrieWithDict();
            trie.Insert("An", "201-222-1119");
            trie.Insert("Antony", "201-222-1111");
            trie.Insert("Angalo", "201-222-1112");
            trie.Insert("Abraham", "201-222-1113");
            trie.Insert("Abcd", "201-222-1114");
            trie.Insert("Nepali", "201-222-1115");
            trie.Insert("NavGlobalHead", "201-222-1116");
            trie.Insert("ZenLoser", "201-222-1117");
            trie.Insert("SujithTheMan", "201-222-1118");
            var people = trie.GetMatchingPeople("n");
        }
        private static void BoggleTest()
        {
            string[] dictionary = {"GEEKS", "FOR", "QUIZ", "GEE"};
      
            // root Node of trie
            TrieWithDict trie = new TrieWithDict();
      
            // insert all words of dictionary into trie
            int n = dictionary.Length;
            foreach(var str in dictionary)
                trie.Insert(str);
      
            char [,]boggle = {{'G','I','Z'},
                               {'U','E','K'},
                               {'Q','S','E'}
            };
            trie.FindWords(boggle);     
        
        }
    }
}
