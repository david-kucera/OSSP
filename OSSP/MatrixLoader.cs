namespace OSSP;

public static class MatrixLoader
{
    #region Public functions
    public static int[][] Load(string[] lines)
    {
        var length = int.Parse(lines[0]);
        int[][] matrix = new int[length][];
        
        for (int i = 0; i < length; i++)
        {
            matrix[i] = new int[length];
            var values = lines[i + 1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < length; j++)
            {
                matrix[i][j] = int.Parse(values[j]);
            }
        }
        
        return matrix;
    }
    #endregion // Public functions

    #region Private functions
    private static void PrintMatrix(int[][] matrix)
    {
        Console.WriteLine($"Matrix dimensions: {matrix.Length}, {matrix[0].Length}");
        foreach (var row in matrix)
        {
            Console.WriteLine($"{string.Join(", ", row)}");
        }
    }
    #endregion // Private functions
}