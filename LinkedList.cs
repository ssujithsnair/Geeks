using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{
    class LinkedList
    {
        class Node
        {
            public int value;
            public Node next;
            public Node(int val)
            {
                next = null;
                value = val;
            }
        }
        public void Push(int val)
        {
            Node newnode = new Node(val);
            newnode.next = Head;
            Head = newnode;
        }
        private Node Sorted;
        private Node Head;
        private void InsertSorted(Node node)
        {
            if (Sorted == null || Sorted.value > node.value)
            {
                var temp = Sorted;
                Sorted = node;
                node.next = temp;
                return;
            }
            var current = Sorted;
            while (current.next != null && current.next.value <= node.value)
                current = current.next;
            node.next = current.next;
            current.next = node;
        }
        public void InsertionSortSingle()
        {
            Node current = Head;
            Sorted = null;
            while (current != null)
            {
                Node next = current.next;
                InsertSorted(current);
                current = next;
            }
            Head = Sorted;
        }
        public void printlist()
        {
            var head = Head;
            while (head != null)
            {
                Console.Write(head.value + " ");
                head = head.next;
            }
        }
    }
}
