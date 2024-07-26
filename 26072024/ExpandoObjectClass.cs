using System.Dynamic;

namespace LibLesson._26072024;

public class ExpandoObjectClass
{
    public static void Main(string[] args)
    {
        dynamic expandoObject = new ExpandoObject();
        expandoObject.Name = "Alex";
        expandoObject.Age = 30;
        
        Console.WriteLine(expandoObject.Name);
        Console.WriteLine(expandoObject.Age);
    }
}