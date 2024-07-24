namespace LibLesson._25072024;

public class ExtensionMethodClass
{
    public static void Main(string[] args)
    {
        int num1 = 1274;
        Console.WriteLine($"SummaDigit of {num1} is {num1.SummaDigit()}"); 

        uint num2 = 132;
        Console.WriteLine($"SummaWithReverse of {num2} is {num2.SummaWithReverse()}"); 
    }
}

public static class MyExtension
{
    public static int SummaDigit(this int number)
    {
        int sum = 0;
        while (number != 0)
        {
            sum += Math.Abs(number % 10);
            number /= 10;
        }
        return sum;
    }

    public static uint SummaWithReverse(this uint number)
    {
        uint reversedNumber = 0;
        uint temp = number;
        while (temp != 0)
        {
            reversedNumber = reversedNumber * 10 + temp % 10;
            temp /= 10;
        }
        return number + reversedNumber;
    }
}