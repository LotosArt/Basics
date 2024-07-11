namespace LibLesson._11072024;

public class DuplicateArray
{
    public static void Main(string[] args)
    {
        int[] arr = { 1, 2, 3, 4, 5, 3 };
        arr = DoubleArray(arr);
        Console.WriteLine(string.Join(", ", arr));
        
        string[] strings = { "a", "b", "c" };
        strings = DoubleArray(strings);
        Console.WriteLine(string.Join(", ", strings));
    }
    
    private static T[] DoubleArray<T>(T[] array)
    {
        int length = array.Length;
        T[] doubledArray = new T[length * 2];
        for (int i = 0; i < length; i++)
        {
            doubledArray[i] = array[i];
            doubledArray[i + length] = array[i];
        }
        return doubledArray;
    }
}