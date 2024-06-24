namespace DigitExtract;

public class DigitExtractor
{
    public static int[] ExtractIntegers(string input)
    {
        if (input == null) Console.WriteLine("Empty input.");

        int count = 0;
        foreach (char ch in input)
        {
            if (char.IsDigit(ch))
            {
                count++;
            }
        }

        int[] integers = new int[count];
        int idx = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                integers[idx] = int.Parse(input[i].ToString());
                idx++;
            }
        }

        return integers;
    }
}