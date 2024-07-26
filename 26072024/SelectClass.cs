namespace LibLesson._26072024;

public class SelectClass
{
    public static void Main(string[] args)
    {
        Product[] products =
        [
            new Product { Name = "Apple", Category = "Fruit", Price = 1.2m, Stock = 100 },
            new Product { Name = "Banana", Category = "Fruit", Price = 0.8m, Stock = 150 },
            new Product { Name = "Carrot", Category = "Vegetable", Price = 0.5m, Stock = 200 },
            new Product { Name = "Bread", Category = "Bakery", Price = 2.5m, Stock = 50 },
            new Product { Name = "Milk", Category = "Dairy", Price = 1.5m, Stock = 80 }
        ];
        
        Console.WriteLine("All products:");
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.Name}, Category: {product.Category}, Price: {product.Price}, Stock: {product.Stock}");
        }
        
        var selectedProducts = products.Select(p => new { p.Name, p.Price }).ToArray();
        
        Console.WriteLine("\nSelected products:");
        foreach (var product in selectedProducts)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
        }
    }
}

public class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}