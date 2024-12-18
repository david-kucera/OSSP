namespace OSSP;

public static class SimulatedAnnealing
{
    #region Constants
    private const int T_MAX = 10_000;   // Počiatočná teplota
    private const int U = 40;           // Maximálny počet preskúmaných prechodov od prechodu k súčasnému riešeniu
    private const int Q = 50;           // Maximálny počet preskúmaných prechodov od poslednej zmeny teploty
    private const int DLZKA_INV_RETAZCA = 5;
    private const double BETA = 0.01;
    #endregion // Constants
    
    #region Class members
    private static readonly Random _rand = new Random();
    #endregion // Class members
    
    public static List<int> Solve(List<int> x_0)
    {
        // 0. krok
        List<int> xStar = x_0; // Doposiaľ najlepšie nájdené riešenie
        List<int> x_i = xStar;
        int i = 0;       
        double t = T_MAX;          // Teplota

        // 1. krok
        int v = 0;  // Počet aktualizácií doposiaľ najlepšieho nájdeného riešenia od posledného zahrievania
        int r = 0;  // Počet preskúmaných prechodov od predchodu k súčasnému riešeniu
        int w = 0;  // Celkový počet preskúmaných prechodov od poslednej zmeny teploty
        
        // 2. krok
        while (true)
        {
            List<int[]> okolie = GetOkolie(x_i);
            int[] x = okolie[_rand.Next(0, okolie.Count)];
            w++;
            r++;
                
            if (w == Q)
            {
                t = t / (1 + BETA * t);
                w = 0;
            }
    
            // 3. krok
            if (SalesmanHeuristic.GetPathCost(x.ToList()) <= SalesmanHeuristic.GetPathCost(x_i))
            {
                i++;
                x_i = x.ToList();
                r = 0;

                if (SalesmanHeuristic.GetPathCost(x_i) < SalesmanHeuristic.GetPathCost(xStar))
                {
                    xStar = x_i.ToList();
                    v++;
                }
            }
            else
            {
                // 4. krok
                int fx = SalesmanHeuristic.GetPathCost(x.ToList());
                int fxi = SalesmanHeuristic.GetPathCost(x_i);
                double delta = fx - fxi;
                double p = Math.Exp(-delta / t);
                var h = _rand.NextDouble();

                if (h <= p)
                {
                    i++;
                    x_i = x.ToList();
                    r = 0;
                }
            }

            // 5. krok
            if (r == U)
            {
                // 6. krok
                if (v > 0)
                {
                    t = T_MAX;
                    v = 0;
                    r = 0;
                    w = 0;
                }
                else
                {
                    break; // KONIEC
                }
            }
        }
        
        return xStar;
    }

    public static List<int[]> GetOkolie(List<int> xStar)
    {
        List<int[]> okolie = new List<int[]>();

        for (int i = 1; i < xStar.Count - DLZKA_INV_RETAZCA; i++)
        {
            var novaTrasa = xStar.ToArray();
            Array.Reverse(novaTrasa, i, DLZKA_INV_RETAZCA);
            okolie.Add(novaTrasa);
            
            //Console.WriteLine($"Trasa {i}: {string.Join("-", novaTrasa)}");
        }
        
        return okolie;
    }
}