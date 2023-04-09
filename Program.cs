namespace LargestColorValueDirectedGraph
{
    internal class Program
    {
        public class LargestColorValueDirectedGraph
        {
            private int DFS(string colors, int node, bool[] visited, bool[] visiting, int[,] frequences, Dictionary<int, List<int>> graph)
            {
                if (visiting[node])
                {
                    return int.MaxValue;
                }
                if (visited[node])
                {
                    return frequences[node, colors[node] - 'a'];
                }
                visited[node] = true;
                visiting[node] = true;
                if (graph.TryGetValue(node, out List<int> neighbors))
                {
                    foreach(int neighbor in neighbors)
                    {
                        if (DFS(colors, neighbor, visited, visiting, frequences, graph) == int.MaxValue)
                        {
                            return int.MaxValue;
                        }
                        for (int i = 0; i < 26; ++i)
                        {
                            frequences[node, i] = Math.Max(frequences[node, i], frequences[neighbor, i]);
                        }
                    }
                }
                frequences[node, colors[node] - 'a']++;
                visiting[node] = false;
                return frequences[node, colors[node] - 'a'];
            }

            public int LargestPathValue(string colors, int[][] edges)
            {
                int n = colors.Length;
                Dictionary<int, List<int>> graph = new();
                foreach (int[] edge in edges)
                {
                    if (!graph.ContainsKey(edge[0]))
                    {
                        graph.Add(edge[0], new List<int>());
                    }
                    graph[edge[0]].Add(edge[1]);
                }
                bool[] visited = new bool[n];
                bool[] visiting = new bool[n];
                int[,] frequences = new int[n, 26];
                int largestPathValue = int.MinValue;
                for(int i = 0; i < n; ++i)
                {
                    largestPathValue = Math.Max(DFS(colors, i, visited, visiting, frequences, graph), largestPathValue);
                }
                return largestPathValue == int.MaxValue ? -1 : largestPathValue;
            }
        }

        static void Main(string[] args)
        {
            LargestColorValueDirectedGraph largestColorValueDirectedGraph = new();
            Console.WriteLine(largestColorValueDirectedGraph.LargestPathValue("abaca", new int[][] {
                new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 2, 3 }, new int[] { 3, 4 }
            }));
            Console.WriteLine(largestColorValueDirectedGraph.LargestPathValue("a", new int[][]
            {
                new int[] { 0, 0 }
            }));
        }
    }
}