namespace LibLesson._14072024;

public class ArrayClass
{
    public static void Main(string[] args)
    {
        int[] numbers = new int[10];
        Random rand = new Random();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rand.Next(1, 101); 
        }

        Console.WriteLine("Array: " + string.Join(", ", numbers));

        Console.WriteLine("Please enter two indexes of array (from 1 to 10):");
        
        try
        {
            int index1 = int.Parse(Console.ReadLine());
            int index2 = int.Parse(Console.ReadLine());

            if (index1 < 1 || index1 > 10 || index2 < 1 || index2 > 10)
            {
                Console.WriteLine("One or both indexes can't be less than 1 and greater than 10.");
            }
            else
            {
                int sum = numbers[index1 - 1] + numbers[index2 - 1];
                Console.WriteLine($"Sum of elements from {index1} and {index2} is {sum}");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("You haven't entered a number");
        }
    }
}