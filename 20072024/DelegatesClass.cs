namespace LibLesson._20072024;

public class DelegatesClass
{
    public static void Main(string[] args)
    {
        Action displayTime = DisplayCurrentTime;
        Action displayDate = DisplayCurrentDate;
        Action displayDayOfWeek = DisplayCurrentDayOfWeek;

        displayTime?.Invoke();
        displayDate?.Invoke();
        displayDayOfWeek?.Invoke();
        
        Func<double, double, double> calculateTriangleArea = CalculateTriangleArea;
        
        double baseLength = 5.0;
        double height = 10.0;
        double triangleArea = calculateTriangleArea(baseLength, height);
        Console.WriteLine($"Triangle Area: {triangleArea}");
        
        Func<double, double, double> calculateRectangleArea = CalculateRectangleArea;
        
        double length = 4.0;
        double width = 7.0;
        double rectangleArea = calculateRectangleArea(length, width);
        Console.WriteLine($"Rectangle Area: {rectangleArea}");
        
    }
    
    static void DisplayCurrentTime()
    {
        Console.WriteLine($"Current Time: {DateTime.Now.ToString("HH:mm:ss")}");
    }

    static void DisplayCurrentDate()
    {
        Console.WriteLine($"Current Date: {DateTime.Now.ToString("yyyy-MM-dd")}");
    }

    static void DisplayCurrentDayOfWeek()
    {
        Console.WriteLine($"Current Day of Week: {DateTime.Now.DayOfWeek}");
    }

    static double CalculateTriangleArea(double baseLength, double height)
    {
        return 0.5 * baseLength * height;
    }

    static double CalculateRectangleArea(double length, double width)
    {
        return length * width;
    }
}