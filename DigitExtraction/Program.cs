namespace DigitExtraction;

class Program
{
    static void Main(string[] args)
    {
        string input = "abc123def456gh789";
        int[] result = DigitExtract.DigitExtractor.ExtractIntegers(input);

        Console.WriteLine("Extracted integers:");
        foreach (int number in result)
        {
            Console.WriteLine(number);
        }
    }
}