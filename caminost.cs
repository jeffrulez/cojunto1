using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camino
{
    class Program
    {
        class Graph
        {
            private List<Vertex> _NodeList = new List<Vertex>();

            public List<Vertex> NodeList { get => _NodeList; set => _NodeList = value; }

            //public override bool Equals(object obj)
            //{
            //    var item = obj as Graph;

            //    if (item == null)
            //    {
            //        return false;
            //    }

            //    return this.NodeList.Equals(item.NodeList);
            //}

            //public override int GetHashCode()
            //{
            //    return this.NodeList.GetHashCode();
            //}
        }

        class Vertex
        {
            private string _Name;
            private List<Vertex> _ConnectedTo;
            private string _Predecesor;

            public string Name { get => _Name; set => _Name = value; }
            public List<Vertex> ConnectedTo { get => vertexList; set => _ConnectedTo = value; }
            public string Predecesor { get => _Predecesor; set => _Predecesor = value; }

            List<Vertex> vertexList = new List<Vertex>();
            public void isConnectedTo(Vertex v)
            {
                ConnectedTo.Add(v);
            }
            public Vertex() { }

            public Vertex (string name)
            {
                Name = name;
            }
        }

        class BreadthFirstSearch
        {
            /// <summary>
            /// Fill the graph g with all vertex and all edges
            /// </summary>
            /// <param name="g"></param>
            /// <param name="node"></param>
            /// <returns> Graph G </returns>
            static public Graph BuildGraph(Graph g, Tuple<Vertex, Vertex> node)
            {
                Vertex nodeA = new Vertex();
                nodeA = node.Item1;
                Vertex nodeB = new Vertex();
                nodeB = node.Item2;

                Vertex nodeC = new Vertex();
                nodeC = g.NodeList.Find(v => v.Name == nodeA.Name);
                if (nodeC == null)
                {
                    nodeA.isConnectedTo(nodeB);
                    g.NodeList.Add(nodeA);
                }
                else
                {
                    g.NodeList[g.NodeList.IndexOf(nodeC)].isConnectedTo(nodeB);
                }

                Vertex nodeD = new Vertex();
                nodeD = g.NodeList.Find(v => v.Name == nodeB.Name);
                if (nodeD == null)
                {
                    nodeB.isConnectedTo(nodeA);
                    g.NodeList.Add(nodeB);
                }
                else
                {
                    g.NodeList[g.NodeList.IndexOf(nodeD)].isConnectedTo(nodeA);
                }

                return g;
            }

            /// <summary>
            /// Apply the BreadthFirstSearch (BFS) algorithm to find a path between root and endNode
            /// </summary>
            /// <param name="g"></param>
            /// <param name="root"></param>
            /// <param name="endNode"></param>
            /// <returns> Tuple<Graph, bool> bool = false if not path found </Grapf></returns>
            static public Tuple<Graph, bool> BFS(Graph g, Vertex root, string endNode)
            {                
                Queue<Vertex> q = new Queue<Vertex>();
                HashSet<Vertex> visited = new HashSet<Vertex>();
                q.Enqueue(root);
                visited.Add(root);

                while(q.Count > 0)
                {
                    Vertex v = q.Dequeue();
                    if(v.Name == endNode)
                    {
                        if (v.Predecesor == null)
                        {
                            g.NodeList[g.NodeList.IndexOf(v)].Predecesor = v.Name;
                        }

                        return Tuple.Create(g, true);
                    }

                    foreach (Vertex item in v.ConnectedTo)
                    {
                        if (!visited.Contains(item))
                        {
                            item.Predecesor = v.Name;
                            q.Enqueue(item);
                            visited.Add(item);
                        }
                    }
                }

                return Tuple.Create(g, false);
            }

            /// <summary>
            /// print to console the founded path
            /// </summary>
            /// <param name="g"></param>
            /// <param name="initNode"></param>
            /// <param name="endNode"></param>
            static public void print(Graph g, string initNode, string endNode)
            {
                List<string> result = new List<string>();
                Vertex finalNode = new Vertex();
                finalNode = g.NodeList.Find(v => v.Name == endNode);
                result.Add(finalNode.Name);

                while (finalNode.Name != initNode)
                {
                    result.Add(finalNode.Predecesor);
                    finalNode = g.NodeList.Find(v => v.Name == finalNode.Predecesor);
                    
                }

                result.Reverse();
                Console.WriteLine($"El camino encontrado es: \n\n{string.Join(" -> ", result.ToArray())}");
            }
        }        

        static void Main(string[] args)
        {
            // Input of the graph
            List<Tuple<Vertex, Vertex>> grafo = new List<Tuple<Vertex, Vertex>>();
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("A"), new Vertex("B")));
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("A"), new Vertex("C")));
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("B"), new Vertex("E")));
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("B"), new Vertex("D")));
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("B"), new Vertex("F")));
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("C"), new Vertex("D")));
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("D"), new Vertex("F")));
            grafo.Add(new Tuple<Vertex, Vertex>(new Vertex("F"), new Vertex("G")));
            string initNode = "A";
            string endNode = "G";

            Graph g = new Graph();
            Tuple<Graph, bool> result = Tuple.Create(g, false);

            foreach (var item in grafo)
            {
                g = BreadthFirstSearch.BuildGraph(g, item);
            }

            foreach (var item in g.NodeList)
            {
                if (item.Name == initNode)
                {
                    result = BreadthFirstSearch.BFS(g, item, endNode);
                    break;
                }
            }

            if (!result.Item2)
            {
                Console.WriteLine($"No hay ningún camino que lleve a {endNode}.");
            }
            else
            {
                BreadthFirstSearch.print(result.Item1, initNode, endNode);
            }
            
            Console.ReadLine();
        }
    }
}