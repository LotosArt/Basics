namespace LibLesson._25072024;

public class ComprarNumberClass
{
    public static void Main1(string[] args)
    {
        List<int> numbers = new List<int> { 3, 5, 2, 4, 1, 5, 4, 6 };

        numbers.Sort(new CustomComparer());

        Console.WriteLine("Sorted numbers:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}

public class CustomComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (x == 5 && y == 5) return 0;
        if (x == 5) return -1;
        if (y == 5) return 1;

        if (x == 4 && y == 4) return 0;
        if (x == 4) return 1;
        if (y == 4) return -1;

        return x.CompareTo(y);
    }
}