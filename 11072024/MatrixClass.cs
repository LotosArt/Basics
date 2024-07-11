namespace LibLesson._11072024;

public class MatrixClass
{
    public static void Main(string[] args)
    {
        var intMatrix = new Matrix<int>(2, 2);
        intMatrix[0, 0] = 1;
        intMatrix[0, 1] = 2;
        intMatrix[1, 0] = 3;
        intMatrix[1, 1] = 4;

        var doubleMatrix = new Matrix<double>(2, 2);
        doubleMatrix[0, 0] = 1.5;
        doubleMatrix[0, 1] = 2.5;
        doubleMatrix[1, 0] = 3.5;
        doubleMatrix[1, 1] = 4.5;

        var floatMatrix = new Matrix<float>(2, 2);
        floatMatrix[0, 0] = 1.1f;
        floatMatrix[0, 1] = 2.2f;
        floatMatrix[1, 0] = 3.3f;
        floatMatrix[1, 1] = 4.4f;

        var intMatrix2 = new Matrix<int>(2, 2);
        intMatrix2[0, 0] = 5;
        intMatrix2[0, 1] = 6;
        intMatrix2[1, 0] = 7;
        intMatrix2[1, 1] = 8;

        Console.WriteLine("Int Matrix:");
        intMatrix.Print();
        Console.WriteLine();

        Console.WriteLine("Double Matrix:");
        doubleMatrix.Print();
        Console.WriteLine();

        Console.WriteLine("Float Matrix:");
        floatMatrix.Print();
        Console.WriteLine();

        var intMatrixSum = intMatrix + intMatrix2;
        Console.WriteLine("Sum of Int Matrices:");
        intMatrixSum.Print();
        Console.WriteLine();

        var intMatrixScaled = intMatrix * 2;
        Console.WriteLine("Int Matrix Scaled by 2:");
        intMatrixScaled.Print();
        Console.WriteLine();

        var intMatrixProduct = intMatrix * intMatrix2;
        Console.WriteLine("Multiplication of Int Matrices:");
        intMatrixProduct.Print();
        Console.WriteLine();
    }
}

public class Matrix<T>
{
    private T[,] data;
    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        data = new T[rows, columns];
    }

    public T this[int row, int col]
    {
        get => data[row, col];
        set => data[row, col] = value;
    }

    public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
        {
            throw new Exception("Matrices must have the same dimensions for addition.");
        }

        var result = new Matrix<T>(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = (dynamic)a[i, j] + (dynamic)b[i, j];
            }
        }
        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, T scalar)
    {
        var result = new Matrix<T>(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = (dynamic)a[i, j] * scalar;
            }
        }
        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
    {
        if (a.Columns != b.Rows)
        {
            throw new Exception("Matrices dimensions do not match for multiplication.");
        }

        var result = new Matrix<T>(a.Rows, b.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < b.Columns; j++)
            {
                for (int k = 0; k < a.Columns; k++)
                {
                    result[i, j] += (dynamic)a[i, k] * (dynamic)b[k, j];
                }
            }
        }
        return result;
    }

    public void Print()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Console.Write($"{data[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}
