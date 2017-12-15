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
        }

        bool Insert(string key, string phone=null)
        {
            key = key.ToLower();
            TrieNode next = root;
            foreach (var c in key)
            {
                TrieNode node;
                if (!next.Children.ContainsKey(c))
                    next.Children.Add(c, node = new TrieNode());
                else
                    node = next.Children[c];
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
                    if (next.Children.ContainsKey(split[j]))
                    {
                        next = next.Children[split[j]];
                        if (next.IsEndOfNode)
                            match[j + 1] = true;
                    }
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

        public static void Test()
        {
            TestPrefixMatch();
            PhoneBookTest();
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
    }
}
