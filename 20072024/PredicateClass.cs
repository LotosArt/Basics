namespace LibLesson._20072024;

public class PredicateClass
{
    public static void Main(string[] args)
    {
        Predicate<string> strLen = s => s.Length >= 7;
        
        string[] testStrings = { "Hello", "World", "Predicate", "CSharp", "Programming", 
            "Test", "Short", "LongStringExample" };

        foreach (var str in testStrings)
        {
            bool result = strLen(str);
            Console.WriteLine($"String: \"{str}\" - Length >= 7: {result}");
        }

    }
}