using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    class Node
    {
        public int data;
        public Node left, right;

        public Node(int item)
        {
            data = item;
            left = null;
            right = null;
        }
    }
    class BinarySearchTree
    {
        /*
         * Time Complexity: The worst case time complexity of search/insert/delete operations is O(h) where h is height of Binary
         * Search Tree. In worst case, we may have to travel from root to the deepest leaf node. The height of a skewed tree may
         * become n and the time complexity of search and insert operation may become O(n).
         */
        Node Root;
        public void Insert(int val)
        {
            Insert(Root, val);
        }
        public Node Insert(Node root, int val)
        {
            if (root == null)
            {
                root = new Node(val);
            }
            else if (val < root.data)
            {
                root.left = Insert(root.left, val);
            }
            else
            {
                root.right = Insert(root.right, val);
            }
            return root;
        }

        public void Delete(int val)
        {
            Delete(Root, val);
        }
        
        private Node Delete(Node root, int val)
        {
            if (root == null)
                return root;
            if (val < root.data)
                root.left = Delete(root.left, val);
            else if (val > root.data)
                root.right = Delete(root.right, val);
            // if key is same as root's key, then This is the node
            // to be deleted
            else
            {
                // node with only one child or no child
                if (root.left == null)
                    return root.right;
                if (root.right == null)
                    return root.left;

                // node with two children: Get the inorder successor (smallest
                // in the right subtree)
                int min = minVal(root.right);
                root.data = min;

                // Delete the inorder successor
                root.right = Delete(root.right, min);
            }
            return root;
        }

        private int minVal(Node root)
        {
            int minval = root.data;
            while (root.left != null)
            {
                root = root.left;
                minval = root.data;
            }
            return minval;
        }

        public Node Search(Node root, int val)
        {
            if (val == root.data)
                return root;
            if (val < root.data)
                return Search(root.left, val);
            return Search(root.right, val);
        }
    }

    class BinaryTree
    {
        public Node Root;
        
        public void PrintLevelNode()
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(Root);
            while (q.Count > 0)
            {
                Node n = q.Dequeue();
                Console.Write(n.data + " ");
                if (n.left != null)
                    q.Enqueue(n.left);
                if (n.right != null)
                    q.Enqueue(n.right);
            }
            Console.WriteLine();
        }

        public void PrintLevelNodeInNewLine()
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(Root);
            while(true)
            {
                int count = q.Count;
                if (count <= 0)
                    break;
                while (count > 0)
                {
                    Node n = q.Dequeue();
                    Console.Write(n.data + " ");
                    if (n.left != null)
                        q.Enqueue(n.left);
                    if (n.right != null)
                        q.Enqueue(n.right);
                    count--;
                }
                Console.WriteLine();
            }
        }

        public void InOrderNoRecursion()
        {
            Stack<Node> s = new Stack<Node>();
            Node current = Root;
            while (current != null)
            {
                s.Push(current);
                current = current.left;
            }
            while (s.Count > 0)
            {
                Node p = s.Pop();
                Console.Write(p.data + " ");
                if (p.right != null)
                {
                    current = p.right;
                    while (current != null)
                    {
                        s.Push(current);
                        current = current.left;
                    }
                }
            }
            Console.WriteLine();
        }
        
        public static void Test()
        {
            BinaryTree tree = new BinaryTree();
            tree.Root = new Node(1);
            tree.Root.left = new Node(2);
            tree.Root.right = new Node(3);
            tree.Root.left.left = new Node(4);
            tree.Root.left.right = new Node(5);
            Console.WriteLine("Level order traversal...");
            tree.PrintLevelNode();

            Console.WriteLine("Level on new line traversal...");
            tree.PrintLevelNodeInNewLine();

            Console.WriteLine("In order with out recursion");
            tree.InOrderNoRecursion();
        }
    }
}
