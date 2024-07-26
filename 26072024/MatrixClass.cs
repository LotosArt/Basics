namespace LibLesson._26072024;

public class MatrixClass
{
    public static void Main(string[] args)
    {
        Matrix matrix1 = new Matrix(2, 2);
        Matrix matrix2 = new Matrix(2, 2);

        matrix1.FillRandom();
        matrix2.FillRandom();

        Console.WriteLine("Matrix 1:");
        matrix1.Print();

        Console.WriteLine("Matrix 2:");
        matrix2.Print();

        Matrix sumMatrix = matrix1.Add(matrix2);
        Console.WriteLine("Sum of Matrix 1 and Matrix 2:");
        sumMatrix.Print();

        double multiplier = 2.0;
        Matrix multipliedMatrix = matrix1.MultiplyByNumber(multiplier);
        Console.WriteLine($"Matrix 1 multiplied by {multiplier}:");
        multipliedMatrix.Print();

        Matrix multipliedMatrices = matrix1.Multiply(matrix2);
        Console.WriteLine("Product of Matrix 1 and Matrix 2:");
        multipliedMatrices.Print();
    }
}

public partial class Matrix
{
    public double[,] Elements { get; private set; }
    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public Matrix(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException("Number of rows and columns must be positive");

        Rows = rows;
        Columns = columns;
        Elements = new double[rows, columns];
    }
}