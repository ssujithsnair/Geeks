using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks
{

    class WeightedGraph
    {
        int NodeCount;
        List<GraphNode>[] nodes;
        public WeightedGraph(int capacity)
        {
            NodeCount = capacity;
            nodes = new List<GraphNode>[capacity];
            for (int i = 0; i < capacity; i++)
                nodes[i] = new List<GraphNode>();
        }
        public bool AddEdge(int a, int b, int w)
        {
            if (nodes.Length <= a)
                return false;
            if (nodes[a] == null)
                nodes[a] = new List<GraphNode>();
            nodes[a].Add(new GraphNode(b, w));
            return true;
        }

        private GraphNode ExtractMin(List<GraphNode> mins)
        {
            GraphNode min = new GraphNode(-1, int.MaxValue);
            foreach (GraphNode node in mins)
                if (node < min)
                {
                    min = node;
                }
            mins.Remove(min);
            return min;
        }
        private GraphNode GetNode(List<GraphNode> mins, int v)
        {
            foreach (var node in mins)
                if (node.Vertex == v)
                    return node;
            return null;
        }
        public void SortACG(int v, Stack<int> sortedGraph, bool[] visited)
        {
            visited[v] = true;
            foreach (var node in nodes[v])
            {
                if (visited[node.Vertex])
                    continue;
                SortACG(node.Vertex, sortedGraph, visited);
            }
            sortedGraph.Push(v);
        }
        public void AcyclicDirectedGraph(int source)
        {
            int[] dist = new int[NodeCount];
            int[] parent = new int[NodeCount];
            for (int i=0; i< NodeCount; i++)
            {
                dist[i] = int.MaxValue;
                parent[i] = -1;
            }
            dist[source] = 0;

            Stack<int> sortedGraph = new Stack<int>();
            bool[] visited = new bool[NodeCount];
            for (int i = 0; i < NodeCount; i++)
            {
                if (!visited[i])
                    SortACG(i, sortedGraph, visited);                    
            }
            while (sortedGraph.Count > 0)
            {
                var u = sortedGraph.Pop();
                if (dist[u] == int.MaxValue)
                    continue;
                foreach (GraphNode v in nodes[u])
                {
                    if (dist[v.Vertex] > (dist[u] + v.Weight))
                    {
                        dist[v.Vertex] = dist[u] + v.Weight;
                        parent[v.Vertex] = u;
                    }
                }
            }
            Print(dist, parent);
        }
        public void Dijkstra(int source)
        {
            int[] dist = new int[NodeCount];
            int[] parent = new int[NodeCount];
            List<GraphNode> mins = new List<GraphNode>(NodeCount);
            for (int i=0; i< NodeCount; i++)
            {
                dist[i] = int.MaxValue;
                parent[i] = -1;
                mins.Add(new GraphNode(i, int.MaxValue));
            }
            dist[source] = 0;
            mins[source].Weight = 0;
            while (mins.Count > 0)
            {
                var u = ExtractMin(mins);
                foreach (var v in nodes[u.Vertex])
                {
                    var node = GetNode(mins, v.Vertex);
                    if (node != null &&
                        dist[v.Vertex] > dist[u.Vertex] + v.Weight)
                    {
                        parent[v.Vertex] = u.Vertex;
                        dist[v.Vertex] = node.Weight = dist[u.Vertex] + v.Weight;
                    }
                }
            }
            Print(dist, parent);

        }

        static void PrintPath(int[] parent, int j)
        {
            if (parent[j] == -1)// base case if parent is source
                return;
            PrintPath(parent, parent[j]);
            Console.Write(parent[j] + "  ");
        }

        static void Print(int[] dist, int[] parent)
        {
            for (int i = 0; i < dist.Length; i++)
            {
                Console.Write("{0}      {1}     ", i, dist[i]);
                PrintPath(parent, i);
                Console.WriteLine();
            }
        }
        class GraphNode
        {
            public int Vertex { get; private set; }
            public int Weight { get; set; }
            public GraphNode(int vertex, int weight)
            {
                Vertex = vertex;
                Weight = weight;
            }
            public static bool operator <(GraphNode a, GraphNode b)
            {
                return a.Weight < b.Weight;
            }
            public static bool operator >(GraphNode a, GraphNode b)
            {
                return a.Weight > b.Weight;
            }
        }
        public static void Test()
        {
            //TestDijkstra();
            TestACG();
        }
        private static void TestDijkstra()
        {
            WeightedGraph g = new WeightedGraph(9);
            g.AddEdge(0, 1, 4);
            g.AddEdge(0, 7, 8);
            g.AddEdge(1, 2, 8);
            g.AddEdge(1, 7, 11);
            g.AddEdge(2, 3, 7);
            g.AddEdge(2, 8, 2);
            g.AddEdge(2, 5, 4);
            g.AddEdge(3, 4, 9);
            g.AddEdge(3, 5, 14);
            g.AddEdge(4, 5, 10);
            g.AddEdge(5, 6, 2);
            g.AddEdge(6, 7, 1);
            g.AddEdge(6, 8, 6);
            g.AddEdge(7, 8, 7);

            g.Dijkstra(0);
        }
        private static void TestACG()
        {
            WeightedGraph g = new WeightedGraph(6);
            g.AddEdge(0, 1, 5);
            g.AddEdge(0, 2, 3);
            g.AddEdge(1, 3, 6);
            g.AddEdge(1, 2, 2);
            g.AddEdge(2, 4, 4);
            g.AddEdge(2, 5, 2);
            g.AddEdge(2, 3, 7);
            g.AddEdge(3, 4, -1);
            g.AddEdge(4, 5, -2);
            g.AcyclicDirectedGraph(1);
        }
    }
    class Graph
    {
        int V;
        List<int>[] nodes;
        public Graph(int v)
        {
            nodes = new List<int>[V=v];
            //for(int i=0; i< v; i++)
            //{
            //    nodes[i] = new List<int>();
            //}
        }

        public bool AddEdge(int a, int b)
        {
            if (nodes.Length <= a)
                return false;
            if (nodes[a] == null)
                nodes[a] = new List<int>();
            nodes[a].Add(b);
            return true;
        }

        public void DFS(int n)
        {
            bool[] marked = new bool[nodes.Length];
            for(int i=0; i < nodes.Length; i++)
            {
                if (!marked[i])
                    DfsInternal(n, marked);
            }
        }

        private void DfsInternal(int n, bool[] marked)
        {
            marked[n] = true;
            Console.WriteLine(n);
            var items = nodes[n];
            if (items == null)
                return;
            foreach (var node in items)
            {
                if (!marked[node])
                    DfsInternal(node, marked);
            }
        }

        public void BFS(int n)
        {
            bool[] marked = new bool[nodes.Length];
            Queue<int> q = new Queue<int>();
            marked[n] = true;
            q.Enqueue(n);
            while (q.Count > 0)
            {
                var item = q.Dequeue();
                Console.WriteLine(item);
                var items = nodes[item];
                if (items == null)
                    continue;
                foreach(var entry in items)
                {
                    if (!marked[entry])
                    {
                        marked[entry] = true;
                        q.Enqueue(entry);
                    }
                }
            }
        }

        public static void Test()
        {
            Graph g = new Graph(6);
            g.AddEdge(0, 1);
            g.AddEdge(0, 4);
            g.AddEdge(0, 5);
            g.AddEdge(1, 3);
            g.AddEdge(1, 4);
            g.AddEdge(3, 2);
            g.AddEdge(2, 1);

            Console.WriteLine("DFS for 0");
            g.DFS(0);
            Console.WriteLine("BFS for 0");
            g.BFS(0);
        }

        
    }
}
