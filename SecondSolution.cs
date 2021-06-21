using System;
using System.Collections.Generic;
using System.Collections;
using System.Net.WebSockets;

namespace ASD___1
{
    // Algorytm ma złożoność czasową: O(n)=n^2
    //  ponieważ musi dla każdego przedziału w tablicy wejściowej sprawdzić czy nie ma on części wspólej z innym przedziałem.

    public static class SecondSolution
    {
        static Dictionary<int[], List<int[]>> graph;
        static Dictionary<int, List<int[]>> nodesInComponent;
        static HashSet<int[]> visited;

        public static List<int[]> SumIntervals(List<int[]> intervals)
        {
            var result = new List<int[]>();

            // Problem zaczyna się od stworzenia grafu w którym połączone ze sobą relacjami będą przedziały które należy
            //  ze sobą później połączyć.
            BuildGraph(intervals);
            BuildComponents(intervals);

            // Dla każdego komponentu, połącz wszystkie przedziały w jeden.
            for (int componentCount = 0; componentCount < nodesInComponent.Count; componentCount++)
                result.Add(MergeNodes(nodesInComponent[componentCount]));

            return result;
        }

        // Buduje graf w którym
        static void BuildGraph(List<int[]> intervals)
        {
            graph = new Dictionary<int[], List<int[]>>();

            foreach (int[] interval in intervals)
                graph.Add(interval, new List<int[]>());

            // Cała złożoność programu jest zawarta w tych 2 zagnieżdżonych pętlach, które muszą się wykonać n^2 razy
            foreach (int[] interval1 in intervals)
                foreach (int[] interval2 in intervals)
                    if (CheckOverlap(interval1, interval2))
                    {
                        graph[interval1].Add(interval2);
                        graph[interval2].Add(interval1);
                    }
        }

        // Łączy ze sobą interwały z pojedyńczego komponentu (spójnej składowej) grafu.
        static int[] MergeNodes(List<int[]> nodes)
        {
            int[] result = nodes[0];

            foreach (int[] item in nodes)
                result[0] = Math.Min(result[0], item[0]);

            foreach (int[] item in nodes)
                result[1] = Math.Max(result[1], item[1]);

            return result;
        }

        //
        static void MarkComponent(int[] start, int componentCount)
        {
            var stack = new Stack<int[]>();
            stack.Push(start);

            // Iteruje aż do oczyszczenia stosu (oznakowania wszystkich gałęzi grafu).
            while (stack.Count > 0)
            {
                int[] node = stack.Pop();

                if (!visited.Contains(node))
                {
                    visited.Add(node);

                    if (!nodesInComponent.ContainsKey(componentCount))
                        nodesInComponent.Add(componentCount, new List<int[]>());

                    nodesInComponent[componentCount].Add(node);

                    foreach (int[] interval in graph[node])
                        stack.Push(interval);
                }
            }
        }

        static void BuildComponents(List<int[]> intervals)
        {
            nodesInComponent = new Dictionary<int, List<int[]>>();
            visited = new HashSet<int[]>();
            int componentCount = 0;

            foreach (int[] interval in intervals)
                if (!visited.Contains(interval))
                    MarkComponent(interval, componentCount++);
        }

        /// <summary>
        /// Checks if intervals overlap.
        /// </summary>
        // Jeśli nie wiesz co to za dziwna składnia to wpisz w google "c# expression bodied", turbo wygodne, na pewno
        //  sie w tym zakochasz kiedy się z tym otrzaskasz.
        static bool CheckOverlap(int[] a, int[] b)
            => (a[0] <= b[1]+1 && b[0] <= a[1]+1);
    }
}