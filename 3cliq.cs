using System;
using System.Collections.Generic;

namespace _3clique
{
    class Program
    {
        //static Graph graphG = new Graph();
        static Dictionary<string, Graph> dictionary = new Dictionary<string, Graph>(); 

        class Graph
        {
            private List<string> _Cliques = new List<string>();
            private List<string> _Neighbor = new List<string>();
            private List<string> _MutualNeighbor = new List<string>();
            private List<int> _NumMutualNeighbor = new List<int>();

            public List<string> Cliques { get => _Cliques; set => _Cliques = value; }
            public List<string> Neighbor { get => _Neighbor; set => _Neighbor = value; }
            public List<string> MutualNeighbor { get => _MutualNeighbor; set => _MutualNeighbor = value; }
            public List<int> NumMutualNeighbor { get => _NumMutualNeighbor; set => _NumMutualNeighbor = value; }

            public Graph()
            {

            }

            public Graph(List<string> cliques, List<string> neighbor, List<string> mutualNeighbor, List<int> numMutualNeighbor)
            {
                Cliques = cliques;
                Neighbor = neighbor;
                MutualNeighbor = mutualNeighbor;
                NumMutualNeighbor = numMutualNeighbor;
            }
        }

        /// <summary>
        /// Connect nodes and create the graph G
        /// </summary>
        /// <param name="input"></param>
        static void ConnectNodes (string[,] input)
        {
            for (int i = 0; i < input.Length / 2; i++)
            {
                
                if (!dictionary.ContainsKey(input[i,0]))
                {
                    Graph graphG = new Graph();
                    graphG.Neighbor.Add(input[i, 1]);
                    dictionary.Add(input[i, 0], graphG);
                }
                else
                {
                    dictionary[input[i, 0]].Neighbor.Add(input[i, 1]);
                }

                if (!dictionary.ContainsKey(input[i, 1]))
                {
                    Graph graphG = new Graph();
                    graphG.Neighbor.Add(input[i, 0]);
                    dictionary.Add(input[i, 1], graphG);
                }
                else
                {
                    dictionary[input[i, 1]].Neighbor.Add(input[i, 0]);
                }
            }
        }

        /// <summary>
        /// Check if 3-Clique exist in graph G
        /// </summary>
        static void check3Clique()
        {
            string aux = string.Empty;
            int cont = 0;

            foreach (var item in dictionary)
            {
                for (int i = 0; i < item.Value.Neighbor.Count; i++)
                {
                    cont = 0;
                    if (dictionary.TryGetValue(item.Value.Neighbor[i], out Graph neighbor))
                    {
                        for (int j = i + 1; j < item.Value.Neighbor.Count; j++)
                        {
                            aux = neighbor.Neighbor.Find(ng => ng == item.Value.Neighbor[j]);

                            if (aux != string.Empty && aux != null)
                            {
                                item.Value.MutualNeighbor.Add(aux);
                                item.Value.Cliques.Add($"[{item.Key}, {item.Value.Neighbor[i]}, {item.Value.Neighbor[j]}]");
                                cont++;
                            }

                            aux = string.Empty;
                        }
                        item.Value.NumMutualNeighbor.Add(cont);
                    }
                }
            }
        }

        static void print3Clique()
        {
            foreach (KeyValuePair<string, Graph> item in dictionary)
            {
                for (int i = 0; i < item.Value.Cliques.Count; i++)
                {
                    Console.WriteLine($"{item.Key} , {item.Value.Cliques[i]}");
                }
            }
        }
        
        static void Main(string[] args)
        {
            string[,] input = new string[,]
            {
                {"A", "B" },
                {"A", "C" },
                {"A", "D" },
                {"B", "C" },
                {"B", "D" },
                {"C", "D" },
                {"C", "E" },
                {"C", "F" },
                {"E", "F" }
            };

            ConnectNodes(input);
            check3Clique();
            print3Clique();
            Console.ReadLine();

        }
    }
}
