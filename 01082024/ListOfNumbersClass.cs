namespace LibLesson._01082024;

public class ListOfNumbersClass
{
    public static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        Random rand = new Random();
        
        for (int i = 0; i < 100; i++)
        {
            numbers.Add(rand.Next(1, 11));
        }

        Console.WriteLine("Numbers:");
        foreach (var number in numbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine();
        
        int[] counts = new int[10];
        for (int i = 0; i < 100; i++)
        {
            counts[numbers[i] - 1]++;
        }

        Console.WriteLine();
        for (int i = 0; i < counts.Length; i++)
        {
            Console.WriteLine($"Number {i + 1} - {counts[i]} times.");
        }
    }
}