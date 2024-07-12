using System.Numerics;

namespace LibLesson._12072024;

public class GenericMethodClass
{
    public static void Main(string[] args)
    {
        List<int> intList = new List<int> { 2, 4, 5 };
        List<float> floatList = new List<float> { 1.2f, 2.3f, 3.4f };
        List<double> doubleList = new List<double> { 1.22, 2.33, 3.44 };

        PrintSum(intList);
        PrintSum(floatList);
        PrintSum(doubleList);
    }
    
    public static void PrintSum<T>(IEnumerable<T> collection) where T : INumber<T>
    {
        double sum = 0;
        foreach (T item in collection)
        {
            sum += Convert.ToDouble((dynamic)item);
        }
        Console.WriteLine($"Sum: {sum}");
    }
}