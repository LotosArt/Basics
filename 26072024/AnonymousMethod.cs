namespace LibLesson._26072024;

public class AnonymousMethod
{
    public static void Main(string[] args)
    {
        var foodItem = Food();
        Console.WriteLine($"Name - {foodItem.GetType().GetProperty("Name").GetValue(foodItem)}");
        Console.WriteLine($"ExpirationDate - {foodItem.GetType().GetProperty("ExpirationDate").GetValue(foodItem)}");
        Console.WriteLine($"Price - {foodItem.GetType().GetProperty("Price").GetValue(foodItem)}");
    }

    public static object Food()
    {
        Console.Write("Enter the name of the food: ");
        string name = Console.ReadLine();

        Console.Write("Enter the expiration date of the food (yyyy-mm-dd): ");
        DateTime expirationDate;
        while (!DateTime.TryParse(Console.ReadLine(), out expirationDate))
        {
            Console.Write("Invalid date format. Please enter the expiration date (yyyy-mm-dd): ");
        }

        Console.Write("Enter the price of the food: ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price))
        {
            Console.Write("Invalid price. Please enter a valid price: ");
        }
        
        var foodItem = new
        {
            Name = name,
            ExpirationDate = expirationDate,
            Price = price
        };

        return foodItem;
        
    }
}