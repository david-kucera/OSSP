namespace OSSP;

public class SimulatedAnnealing
{
    #region Constants
    private const int T_MAX = 10_000;   // Počiatočná teplota
    private const int U = 40;           // Maximálny počet preskúmaných prechodov od prechodu k súčasnému riešeniu
    private const int Q = 50;           // Maximálny počet preskúmaných prechodov od poslednej zmeny teploty
    #endregion // Constants
    
    public static List<int> Solve(List<int> path)
    {
        // 0. krok
        List<int> xStar = path; // Doposiaľ najlepšie nájdené riešenie
        int i = 0;       
        int t = T_MAX;          // Teplota

        // 1. krok
        int v = 0;  // Počet aktualizácií doposiaľ najlepšieho nájdeného riešenia od posledného zahrievania
        int r = 0;  // Počet preskúmaných prechodov od predchodu k súčasnému riešeniu
        int w = 0;  // Celkový počet preskúmaných prechodov od poslednej zmeny teploty
        
        // 2. krok
        List<int[]> okolie = GetOkolie(xStar);

        return xStar;
    }

    public static List<int[]> GetOkolie(List<int> xStar)
    {
        List<int[]> okolie = new List<int[]>();
        
        
        
        return okolie;
    }
}