namespace OSSP;

public static class SalesmanHeuristic
{
    private static int[][] Dij = null!;
    private static List<int> Nezaradene = [];
    private static List<int> Path = [];
    
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
        // TODO
        
        return Path;
    }

    public static int GetPathCost()
    {
        if (Dij is null) throw new InvalidOperationException("Dij is null!");
        int pathCost = 0;

        for (int i = 0; i < Path.Count - 1; i++)
        {
            var first = Path[i];
            var second = Path[i + 1];
            var cost = Dij[first][second];
            pathCost += cost;
        }
        
        return pathCost;
    }

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
} 