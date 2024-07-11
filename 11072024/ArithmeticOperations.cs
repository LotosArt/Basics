namespace LibLesson._11072024;

public class ArithmeticOperations
{
    private enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    
    public static void Main(string[] args)
    {
        Console.WriteLine(Calculate(10, 5, Operation.Add));      
        Console.WriteLine(Calculate(10, 5, Operation.Subtract));  
        Console.WriteLine(Calculate(10, 5, Operation.Multiply));  
        Console.WriteLine(Calculate(10, 5, Operation.Divide));    

        Console.WriteLine(Calculate("a", "b", Operation.Add));    
        Console.WriteLine(Calculate('a', 'b', Operation.Add));  
    }

    private static T? Calculate<T>(T a, T b, Operation op)
    {
        if (a is string || b is string || a is char || b is char)
        {
            return default(T);
        }

        dynamic x = a;
        dynamic y = b;

        switch (op)
        {
            case Operation.Add:
                return (T)(x + y);
            case Operation.Subtract:
                return (T)(x - y);
            case Operation.Multiply:
                return (T)(x * y);
            case Operation.Divide:
                if (y == 0)
                {
                    throw new DivideByZeroException("Division by zero is not allowed.");
                }
                return (T)(x / y);
            default:
                throw new InvalidOperationException("Invalid operation.");
        }
    }
    
    
}