namespace OSSP;

public static class SalesmanHeuristic
{
    #region Class members
    private static int[][] Dij = null!;
    private static int D => GetPathCost();
    private static List<int> Nezaradene = [];
    private static List<int> Path = [];
    #endregion // Class members
    
    #region Public functions
    public static List<int> Solve(int[][] dij)
    {
        Dij = dij ?? throw new InvalidOperationException("dij is null!");
        for (int i = 0; i < dij.Length; i++) Nezaradene.Add(i);

        // Initial route
        const int routeStartEndIndex = 0;
        Path.Add(routeStartEndIndex);
        Nezaradene.Remove(routeStartEndIndex);
        
        int secondIndex = GetMostDistantNodeFrom(routeStartEndIndex);
        Path.Add(secondIndex);
        Nezaradene.Remove(secondIndex);
        
        int thirdIndex = GetMostDistantNodeFrom(secondIndex);
        Path.Add(thirdIndex);
        Nezaradene.Remove(thirdIndex);
        
        Path.Add(routeStartEndIndex);
        
        // Algorithm
        while (Nezaradene.Count > 0)
        {
            int bestNode = -1;
            int bestPosition = -1;
            int minIncrease = int.MaxValue;

            foreach (var node in Nezaradene)
            {
                for (int i = 0; i < Path.Count - 1; i++)
                {
                    int currentNode = Path[i];
                    int nextNode = Path[i + 1];
                    int increase = Dij[currentNode][node] + Dij[node][nextNode] - Dij[currentNode][nextNode];

                    if (increase < minIncrease)
                    {
                        minIncrease = increase;
                        bestNode = node;
                        bestPosition = i + 1;
                    }
                }
            }
            
            Path.Insert(bestPosition, bestNode);
            Nezaradene.Remove(bestNode);
        }
        
        return Path;
    }

    public static int GetPathCost(List<int>? path = null)
    {
        if (Dij is null) throw new InvalidOperationException("Dij is null!");
        if (path is null) path = Path;
        int pathCost = 0;

        for (int i = 0; i < path.Count - 1; i++)
        {
            var first = path[i];
            var second = path[i + 1];
            var cost = Dij[first][second];
            pathCost += cost;
        }
        
        return pathCost;
    }
    #endregion // Public functions

    #region Private functions
    private static int GetMostDistantNodeFrom(int index)
    {
        if (Dij is null) throw new InvalidOperationException("Dij is null!");
        var row = Dij[index];

        while (true)
        {
            int maxValue = row.Max();
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == maxValue)
                {
                    if (Nezaradene.Contains(i)) return i;
                    else row[i] = int.MinValue;
                }
            }
        }
    }
    #endregion // Private functions
} 