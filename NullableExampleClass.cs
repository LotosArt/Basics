namespace LibLesson;

public class NullableExampleClass
{
    static void Main(string[] args)
    {
        try
        {
            NullableExample example = new NullableExample(null, "Value2");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


        bool? b = null;
        int? i = null;
        double? d = null;

        Nullable<int> nInt = null;
        Nullable<bool> nBool = null;

        int i1 = i ??= 0;
        double d1 = d ??= 0;

    }
}

public class NullableExample
{
    private string prop1;
    private string prop2;

    public NullableExample(string prop1, string prop2)
    {
        Prop1 = prop1;
        Prop2 = prop2;
    }
    
    public string Prop1
    {
        get => prop1;
        set => prop1 = value ?? throw new Exception("Prop1 is null");
    }
    
    public string Prop2
    {
        get => prop2;
        set => prop2 = value ?? throw new Exception("Prop2 is null");
    }
}