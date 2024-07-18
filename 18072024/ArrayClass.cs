namespace LibLesson._18072024;

public class ArrayClass
{
    delegate void ArrayDelegate(int[] array);
    
    public static void Main1(string[] args)
    {
        int[] array = new int[30];
        Random rand = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rand.Next(-100, 101); 
        }
        
        ArrayDelegate ascendingDelegate = SortAndPrintAscending;
        ArrayDelegate descendingDelegate = SortAndPrintDescending;
        ArrayDelegate evenDelegate = CreateAndPrintEvenArray;
        ArrayDelegate oddDelegate = CreateAndPrintOddArray;
        
        ArrayDelegate allOperationsDelegate = ascendingDelegate + descendingDelegate + evenDelegate + oddDelegate;

        Console.WriteLine("Array:");
        PrintArray(array);

        allOperationsDelegate(array);
    }
    
    static void SortAndPrintAscending(int[] array)
    {
        int[] sortedArray = (int[])array.Clone();
        Array.Sort(sortedArray);
        Console.WriteLine("Ascending:");
        PrintArray(sortedArray);
    }
    
    static void SortAndPrintDescending(int[] array)
    {
        int[] sortedArray = (int[])array.Clone();
        Array.Sort(sortedArray);
        Array.Reverse(sortedArray);
        Console.WriteLine("Descending:");
        PrintArray(sortedArray);
    }

    static void CreateAndPrintEvenArray(int[] array)
    {
        int[] evenArray = array.Where(x => x % 2 == 0).ToArray();
        Console.WriteLine("Even numbers:");
        PrintArray(evenArray);
    }

    static void CreateAndPrintOddArray(int[] array)
    {
        int[] oddArray = array.Where(x => x % 2 != 0).ToArray();
        Console.WriteLine("Odd numbers:");
        PrintArray(oddArray);
    }

    static void PrintArray(int[] array)
    {
        foreach (int item in array)
        {
            Console.Write(item + "\t");
        }
        Console.WriteLine();
    }
}
