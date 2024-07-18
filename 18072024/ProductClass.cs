namespace LibLesson._18072024;

public class ProductClass
{
    public static void Main(string[] args)
    {
        Product[] products =
        [
            new Product { Id = 1, Name = "Phone", Quantity = 10, Price = 500.0 },
            new Product { Id = 2, Name = "Laptop", Quantity = 5, Price = 1200.0 },
            new Product { Id = 3, Name = "Tablet", Quantity = 7, Price = 750.0 },
            new Product { Id = 4, Name = "Headphone", Quantity = 20, Price = 150.0 },
            new Product { Id = 5, Name = "Watch", Quantity = 15, Price = 250.0 }
        ];
        
        Product? mostExpensiveProduct = products.MaxBy(p => p.Price);
        
        if (mostExpensiveProduct != null)
        {
            Console.WriteLine("Most  expensive product:");
            Console.WriteLine($"ID: {mostExpensiveProduct.Id}");
            Console.WriteLine($"Name: {mostExpensiveProduct.Name}");
            Console.WriteLine($"Quantity: {mostExpensiveProduct.Quantity}");
            Console.WriteLine($"Price: {mostExpensiveProduct.Price}");
        }
        else
        {
            Console.WriteLine("Товары не найдены.");
        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
