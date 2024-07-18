namespace LibLesson._18072024;

public class FilterMethod
{
    public static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //1
        int[] oddNumbersM = Filter(numbers, IsOdd);

        Console.WriteLine("Separate method:");
        foreach (var number in oddNumbersM)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine("\n");
        
        //2
        int[] oddNumbersA = Filter(numbers, delegate(int number) { return number % 2 != 0; });

        Console.WriteLine("Anonymous method:");
        foreach (var number in oddNumbersA)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine("\n");
        
        //3
        int[] oddNumbers = Filter(numbers, number => number % 2 != 0);

        Console.WriteLine("Lambda:");
        foreach (var number in oddNumbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine();
    }
    
    static bool IsOdd(int number)
    {
        return number % 2 != 0;
    }
    
    static int[] Filter(int[] numbers, Func<int, bool> predicate)
    {
        List<int> result = new List<int>();
        foreach (var number in numbers)
        {
            if (predicate(number))
            {
                result.Add(number);
            }
        }
        return result.ToArray();
    }
}