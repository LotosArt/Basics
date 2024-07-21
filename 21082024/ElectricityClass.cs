namespace LibLesson._21082024;

public class ElectricityClass
{
    public static void Main(string[] args)
    {
        Counter counter = new Counter(100, 500, 4.89m);
        counter.Show();
    }
}

public interface IResultShowInterface
{
    void Show();
}

public interface ISumInterface
{
    decimal Sum();
}

public class Counter : IResultShowInterface, ISumInterface
{
    public int StartValue { get; set; }
    public int EndValue { get; set; }
    public decimal PricePerKw { get; set; }

    public Counter(int startValue, int endValue, decimal pricePerKw)
    {
        StartValue = startValue;
        EndValue = endValue;
        PricePerKw = pricePerKw;
    }
    
    public void Show()
    {
        Console.WriteLine($"Start value: {StartValue}, end value: {EndValue}, " +
                          $"price per kW: {PricePerKw:C}\nResult: {Sum():C}");        
    }

    public decimal Sum()
    {
        return (EndValue - StartValue) * PricePerKw;
    }
}