namespace LibLesson._12072024;

public class CircleClass
{
    public static void Main(string[] args)
    {
        int[] intArray = { 1, 2, 3, 4, 5 };
        Console.WriteLine("Current array:");
        PrintArray(intArray);
        
        CircleMove(intArray);
        Console.WriteLine("After array:");
        PrintArray(intArray);
        
        string[] stringArray = { "A", "B", "C", "D" };
        Console.WriteLine("\nCurrent array:");
        PrintArray(stringArray);
        
        CircleMove(stringArray);
        Console.WriteLine("After array:");
        PrintArray(stringArray);
    }

    public static void CircleMove<T>(T[] arr)
    {
        if (arr.Length == 0)
        {
            throw new Exception("Array is empty");
        }
        
        T lastElement = arr[^1];
        for (int i = arr.Length - 1; i > 0; i--)
        {
            arr[i] = arr[i - 1];
        }
        arr[0] = lastElement;
    }
    
    public static void PrintArray<T>(T[] arr)
    {
        foreach (T item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}