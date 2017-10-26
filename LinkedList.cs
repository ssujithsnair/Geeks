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
        private Node MergeNodes(Node left, Node right)
        {
            if (left == null)
                return right;
            if (right == null)
                return left;

            Node result = null;
            if (left.value <= right.value)
            {
                result = left;
                result.next = MergeNodes(left.next, right);
            }
            else
            {
                result = right;
                result.next = MergeNodes(left, right.next);
            }
            return result;
        }

        private Node GetMiddle(Node head)
        {
            if (head == null)
                return head;

            var slow = head;
            var fast = head.next;

            // Move fastptr by two and slow ptr by one
            // Finally slowptr will point to middle node
            while (fast != null)
            {
                fast = fast.next;
                if (fast != null)
                {
                    slow = slow.next;
                    fast = fast.next;
                }
            }
            return slow;
        }

        public void MergeSort()
        {
            Head = MergeSort(Head);
        }

        private Node MergeSort(Node head)
        {
            if (head == null || head.next == null)
                return head;

            // get the middle of the list
            Node mid = GetMiddle(head);
            Node midN = mid.next;

            // set the next of middle node to null
            mid.next = null;
            
            // Apply mergeSort on left list
            var left = MergeSort(head);
            
            // Apply mergeSort on right list
            var right = MergeSort(midN);

            // Apply mergeSort on right list
            var mergedNodes = MergeNodes(left, right);
            return mergedNodes;
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
