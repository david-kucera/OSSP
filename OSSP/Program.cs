﻿namespace OSSP;

/// <summary>
/// Semestrálna práca z predmetu Optimalizácia sietí, FRI UNIZA, 2024
/// Zadanie ZA - Duálna heuristika 2 - Simulated Annealing B
/// Duálna heuristika: Algoritmus zväčšovania o najvýhodnejší uzol
/// Simulated Annealing - Inverzia podreťazcov dĺžky 5
/// SA parametre: tmax = 10_000, u = 40, q = 50
/// </summary>
internal static class Program
{
    #region Constants
    public const string BA = "../data/BA.txt";
    public const string BB = "../data/BB.txt";
    public const string KE = "../data/KE.txt";
    public const string NR = "../data/NR.txt";
    public const string PO = "../data/PO.txt";
    public const string TN = "../data/TN.txt";
    public const string TT = "../data/TT.txt";
    public const string ZA = "../data/ZA.txt";
    public static readonly List<string> MATRICES = [BA, BB, KE, NR, PO, TN, TT, ZA];
    #endregion // Constants

    private static void Main()
    {
        // Načítanie vstupných dát zo súboru
        CheckFile(ZA);
        string[] lines = File.ReadAllLines(ZA);
        int[][] dij = MatrixLoader.Load(lines);
        
        // Heuristika obchodného cestujúceho - duálna heuristika
        List<int> result = SalesmanHeuristic.Solve(dij);
        int cost = SalesmanHeuristic.GetPathCost();
        
        Console.WriteLine("After heuristic:");
        string line = result.Aggregate(string.Empty, (current, node) => current + (node + "-"));
        Console.WriteLine($"Path: -{line}");
        Console.WriteLine($"Path cost: {cost}");
        
        // Metaheuristika Simulated Annealing
        var metaResult = SimulatedAnnealing.Solve(result);
        int costSA = SalesmanHeuristic.GetPathCost(metaResult);
        Console.WriteLine("After metaheuristic:");
        string line2 = metaResult.Aggregate(string.Empty, (current, node) => current + (node + "-"));
        Console.WriteLine($"Path after SA: -{line2}");
        Console.WriteLine($"Path cost: {costSA}");
    }

    private static void CheckFile(string path)
    {
        Console.WriteLine($"Path: {path}");
        if (!File.Exists(path)) throw new FileNotFoundException();
    }
}