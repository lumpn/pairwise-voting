# Pairwise voting
Unique pair generator for sparse pairwise voting.

## Algorithm
The basic idea is to generate edge cycles over a fully connected graph of `N` nodes by generating edges by stepping through the nodes like a [linear congruential generator](https://en.wikipedia.org/wiki/Linear_congruential_generator) using coprimes of `N`.

```csharp
int previousIndex = 0;
for (int i = 1; i <= numNodes; i++)
{
    var index = (previousIndex + coprime) % numNodes;
    print("{0}, {1}", previousIndex, index);
    previousIndex = index;
}
```

## Example
For a graph of 15 nodes, find coprimes 1, 2, 4, 7 and generate cycles:

| coprime | n<sub>0</sub> | n<sub>1</sub> | n<sub>2</sub> | n<sub>3</sub> | n<sub>4</sub> | n<sub>5</sub> | n<sub>6</sub> | n<sub>7</sub> | n<sub>8</sub> | n<sub>9</sub> | n<sub>10</sub> | n<sub>11</sub> | n<sub>12</sub> | n<sub>13</sub> | n<sub>14</sub> | n<sub>15</sub> |
|--------:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
|    1    |  0 |  1 |  2 |  3 |  4 |  5 |  6 |  7 |  8 |  9 | 10 | 11 | 12 | 13 | 14 |  0 |
|    2    |  0 |  2 |  4 |  6 |  8 | 10 | 12 | 14 |  1 |  3 |  5 |  7 |  9 | 11 | 13 |  0 |
|    4    |  0 |  4 |  8 | 12 |  1 |  5 |  9 | 13 |  2 |  6 | 10 | 14 | 13 |  7 | 11 |  0 |
|    7    |  0 |  7 | 14 |  6 | 13 |  5 | 12 |  4 | 11 |  3 | 10 |  2 |  9 |  1 |  8 |  0 |

All edges (n<sub>i</sub>, n<sub>i+1</sub>) are guaranteed to be unique across all generated cycles.

## Comprime threshold
The reason we stop at coprime 7 in the example above is because while coprimes larger than `N / 2` generate additional unique _directed_ edges they don't generate unique _undirected_ edges. E.g. coprime 8 would generate the edge (0, 8) in its first step but we already have the edge (8, 0) from the last step of comprime 7's cycle.

## Graph radius
If our goal is to generate pairs that minimize the radius of the sparse graph of one cycle, then we should pick the largest coprime smaller than `N / 2` first, because that connects the farthest nodes directly, halving the radius. Following a binary split pattern through the coprimes, we can cut the radius of the graph in half in each step.

## Completeness
If `N` is prime then this algorithm finds all edges of the fully connected graph.

Proof: All numbers 1, 2, 3, ..., (N-1)/2 are coprimes of `N` and they are all smaller than `N / 2`. Therefore the algorithm generates cycles for each one of them, effectively generating `(N-1)/2` cycles of length `N`, resulting in a total of `N * (N-1)/2` unique edges. But that's exactly the number of edges in a fully connected graph. Therefore the algorithm finds all of them.

Finding an algorithm that generates all edges if `N` is not prime is left as an exercise to the reader. 😘
