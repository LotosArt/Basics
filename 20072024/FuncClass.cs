namespace LibLesson._20072024;

public class FuncClass
{
    public static void Main(string[] args)
    {
        Func<string, int> closestToZero = s =>
        {
            var nums = s.Split(' ').Select(int.Parse).ToArray();
            int closest = nums.OrderBy(n => Math.Abs(n)).ThenByDescending(n => n).First();

            return closest;
        };
        
        string numbers = "3 -2 -5 4 -1 6 1 -3 2";
        int result = closestToZero(numbers);
        Console.WriteLine($"The number closest to zero is: {result}");
    }
}