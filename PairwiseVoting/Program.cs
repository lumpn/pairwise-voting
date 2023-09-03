namespace PairwiseVoting
{
    internal class Program
    {
        struct Edge
        {
            public readonly int nodeA, nodeB;

            public Edge(int nodeA, int nodeB)
            {
                this.nodeA = nodeA;
                this.nodeB = nodeB;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            int numNodes = 17;
            int[] coprimes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            Console.WriteLine("Generating unique pairs for a graph of {0} nodes using these coprimes: {1}",
                numNodes, string.Join(", ", coprimes));

            foreach (int coprime in coprimes)
            {
                Console.WriteLine("Cycle using coprime {0}:", coprime);

                int previousIndex = 0;
                for (int i = 1; i < numNodes + 1; i++)
                {
                    var index = (i * coprime) % numNodes;
                    var edge = new Edge(previousIndex, index);
                    previousIndex = index;

                    Console.WriteLine("  {0} -- {1}", edge.nodeA, edge.nodeB);
                }
            }
        }
    }
}
