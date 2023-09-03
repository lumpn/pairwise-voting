using System;
using System.Collections.Generic;

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

            public override string ToString()
            {
                return string.Format("({0}, {1})", nodeA, nodeB);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Generates unique pairs for a graph of N nodes using coprimes.");
                Console.WriteLine("Usage: PairwiseVoting <numNodes>");
                return;
            }

            var numNodes = int.Parse(args[0]);
            var coprimes = FindCoprimes(numNodes);
            var numPairs = numNodes * coprimes.Length;

            Console.WriteLine("Generating {0} unique pairs for a graph of {1} nodes using these coprimes: {2}",
                numPairs, numNodes, string.Join(", ", coprimes));

            foreach (int coprime in coprimes)
            {
                Console.WriteLine("Cycle using coprime {0}:", coprime);

                int previousIndex = 0;
                for (int i = 1; i <= numNodes; i++)
                {
                    var index = (previousIndex + coprime) % numNodes;
                    var edge = new Edge(previousIndex, index);
                    previousIndex = index;

                    Console.WriteLine("  {0}", edge);
                }
            }
        }

        private static int[] FindCoprimes(int N)
        {
            var coprimes = new List<int>();
            for (int i = 1; i <= (N / 2); i++)
            {
                if (AreCoprime(N, i))
                {
                    coprimes.Add(i);
                }
            }
            return coprimes.ToArray();
        }

        private static bool AreCoprime(int larger, int smaller)
        {
            var gdc = GreatestCommonDivisor(larger, smaller);
            return (gdc == 1);
        }

        private static int GreatestCommonDivisor(int largerNumber, int smallerNumber)
        {
            if (smallerNumber == 0)
            {
                return largerNumber;
            }

            return GreatestCommonDivisor(smallerNumber, largerNumber % smallerNumber);
        }
    }
}
