using System;
using System.Collections.Generic;

namespace cwu.cs.TaAssignments
{
    class Graph
    {
        public int noOfVertices = -1;

        // Adjacency list representing the graph.
        public int[][] edges = null;
        public int[][] weights = null;
        public int[][] capacities = null;


        // Creates a new empty graph.
        public Graph() { }


        public int[][] flowDijkstra(int startId, int[] piVal)
        {
            // --- Initialise. ---

            Distance[] distances = new Distance[noOfVertices];
            int[] parents = new int[noOfVertices];

            // The index of an edge to a vertx from its parent.
            int[] neiIndices = new int[noOfVertices];

            for (int i = 0; i < noOfVertices; i++)
            {
                distances[i] = Distance.Infinite;
                parents[i] = -1;
                neiIndices[i] = -1;
            }
            distances[startId] = Distance.Zero;


            // --- Run Dijkstra's algorithm. ---

            for (DijkstraHeap heap = new DijkstraHeap(distances); heap.getSize() > 0;)
            {
                int vId = heap.removeMin();
                Distance vDis = distances[vId];

                // Unreachable vertex?
                if (vDis.IsInfinite) break;

                for (int i = 0; i < edges[vId].Length; i++)
                {
                    int vuWeight = weights[vId][i];
                    if (vuWeight == int.MaxValue) continue;

                    int uId = edges[vId][i];
                    Distance uDis = distances[uId];

                    vuWeight += piVal[vId] - piVal[uId];
                    Distance vuDis = vDis + vuWeight;

                    // Relax
                    if (vuDis < uDis)
                    {
                        heap.update(uId, vuDis);
                        parents[uId] = vId;
                        neiIndices[uId] = i;
                    }
                }
            }

            int[] dist1D = new int[noOfVertices];

            for (int i = 0; i < noOfVertices; i++)
            {
                dist1D[i] = distances[i].Weight;
            }

            return new int[][] { dist1D, parents, neiIndices };
        }

        // ---------------------

        public int[][] minCostMaxFlow(int sId, int tId)
        {
            // Implementation based on
            // J. Edmonds, R.M. Karp:
            // Theoretical Improvements in Algorithmic Efficiency for Network Flow Problems
            // Journal of the ACM 19 (2), 248-264, 1972.

            // Returns the selected capacities of the edges (in same structure as adjacency list).


            // (1) Set f_0 and pi_0 to 0 each (i.e., no capacity used and no modification of distances).
            int[] piVal = new int[noOfVertices];

            int[][] flow = new int[noOfVertices][];
            for (int vId = 0; vId < noOfVertices; vId++)
            {
                flow[vId] = new int[edges[vId].Length];
            }


            while (true)
            {
                // (2) [k to k + 1]
                //     Determine f_k+1 by augmenting along a minimum-weight path from s to t in G_k.
                //     As edge weights, use w_k(u, v) = w(u, v) + pi_k(u) - pi_k(v).
                //     Give preference to path with fewer edges if there are multiple with the same weight.

                Graph resG = buildResidualGraph(flow);

                int[][] dijResult = resG.flowDijkstra(sId, piVal);
                int[] dijDistances = dijResult[0];
                int[] dijParents = dijResult[1];
                int[] dijNeiInds = dijResult[2];


                // (4) Stop if there is no path from s to t in G_k.
                if (dijDistances[tId] == int.MaxValue) break;


                // Determine the available capacity on the path.
                int minCap = int.MaxValue;
                for (int curId = tId; curId != sId; curId = dijParents[curId])
                {
                    int parId = dijParents[curId];
                    int neiInd = dijNeiInds[curId];
                    int cap = resG.capacities[parId][neiInd];

                    minCap = Math.Min(minCap, cap);
                }

                // Update flow.
                for (int curId = tId; curId != sId; curId = dijParents[curId])
                {
                    int parId = dijParents[curId];
                    int neiInd = dijNeiInds[curId];

                    if (neiInd < edges[parId].Length)
                    {
                        // Original edge was used.
                        flow[parId][neiInd] += minCap;
                    }
                    else
                    {
                        // Dummy edge was used, i.e., original edge is from current vertex to parent.

                        // Find original edge.
                        for (int i = 0; i < edges[curId].Length; i++)
                        {
                            if (edges[curId][i] == parId)
                            {
                                flow[curId][i] -= minCap;
                                break;
                            }
                        }
                    }
                }


                // (3) [Update pi values]
                //     Let d_k(u) be weight of shortest path from s to u in G_k with respect to w_k().
                //     Set, for each u, pi_k+1(u) = pi_k(u) + d_k(u); set to infinite if u is not reachable from s.

                for (int vId = 0; vId < noOfVertices; vId++)
                {
                    if (dijDistances[vId] < int.MaxValue)
                    {
                        piVal[vId] += dijDistances[vId];
                    }
                    else
                    {
                        piVal[vId] = int.MaxValue;
                    }
                }

            }

            return flow;
        }

        private Graph buildResidualGraph(int[][] flow)
        {
            Graph g = new Graph();
            g.noOfVertices = noOfVertices;

            List<List<int>> neiBuffer = new List<List<int>>(noOfVertices);
            List<List<int>> weiBuffer = new List<List<int>>(noOfVertices);
            List<List<int>> capBuffer = new List<List<int>>(noOfVertices);

            // Add existing edges.
            for (int uId = 0; uId < noOfVertices; uId++)
            {
                neiBuffer.Add(new List<int>());
                weiBuffer.Add(new List<int>());
                capBuffer.Add(new List<int>());

                for (int i = 0; i < edges[uId].Length; i++)
                {
                    int vId = edges[uId][i];
                    int uvWeight = weights[uId][i];
                    int uvCap = capacities[uId][i];
                    int uvFlow = flow[uId][i];

                    if (uvCap == uvFlow)
                    {
                        uvWeight = int.MaxValue;
                    }

                    neiBuffer[uId].Add(vId);
                    weiBuffer[uId].Add(uvWeight);
                    capBuffer[uId].Add(uvCap - uvFlow);
                }
            }

            // Add dummy edges.
            for (int uId = 0; uId < noOfVertices; uId++)
            {
                for (int i = 0; i < edges[uId].Length; i++)
                {
                    int vId = edges[uId][i];
                    int uvWeight = weights[uId][i];
                    int uvFlow = flow[uId][i];

                    if (uvFlow > 0)
                    {
                        neiBuffer[vId].Add(uId);
                        weiBuffer[vId].Add(-uvWeight);
                        capBuffer[vId].Add(uvFlow);
                    }
                }
            }

            g.edges = new int[noOfVertices][];
            g.weights = new int[noOfVertices][];
            g.capacities = new int[noOfVertices][];

            for (int i = 0; i < noOfVertices; i++)
            {
                g.edges[i] = new int[neiBuffer[i].Count];
                g.weights[i] = new int[weiBuffer[i].Count];
                g.capacities[i] = new int[capBuffer[i].Count];

                for (int j = 0; j < g.edges[i].Length; j++)
                {
                    g.edges[i][j] = neiBuffer[i][j];
                    g.weights[i][j] = weiBuffer[i][j];
                    g.capacities[i][j] = capBuffer[i][j];
                }
            }

            return g;
        }
    }
}
