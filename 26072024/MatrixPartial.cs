namespace LibLesson._26072024;

public partial class Matrix
{
    public void FillRandom()
    {
        Random rand = new Random();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Elements[i, j] = rand.NextDouble() * 10; // Заполняем числами от 0 до 10
            }
        }
    }
    
    public Matrix Add(Matrix other)
    {
        if (Rows != other.Rows || Columns != other.Columns)
            throw new ArgumentException("Matrices must be of the same size");

        Matrix result = new Matrix(Rows, Columns);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                result.Elements[i, j] = Elements[i, j] + other.Elements[i, j];
            }
        }
        return result;
    }
    
    public Matrix MultiplyByNumber(double number)
    {
        Matrix result = new Matrix(Rows, Columns);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                result.Elements[i, j] = Elements[i, j] * number;
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
                Console.Write($"{Elements[i, j]:F2}\t");
            }
            Console.WriteLine();
        }
    }
    
    public Matrix Multiply(Matrix other)
    {
        if (Columns != other.Rows)
            throw new ArgumentException("Number of columns of the first matrix must be equal to the number of rows of the second matrix");

        Matrix result = new Matrix(Rows, other.Columns);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < other.Columns; j++)
            {
                double sum = 0;
                for (int k = 0; k < Columns; k++)
                {
                    sum += Elements[i, k] * other.Elements[k, j];
                }
                result.Elements[i, j] = sum;
            }
        }
        return result;
    }
}