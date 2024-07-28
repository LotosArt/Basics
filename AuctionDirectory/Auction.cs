namespace LibLesson.AuctionDirectory;

public class Auction
{
    public string Name { get; set; }
    private List<Product> Products { get; set; } = new List<Product>();
    public int TotalProducts => Products.Count;
    public decimal TotalRegisteredValue => Products.Sum(p => p.Price * p.Quantity);
    public decimal TotalSales { get; private set; }
    public int TotalSoldItems { get; private set; }
    
    public void AddProduct(Product product)
    {
        product.Id = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
        Products.Add(product);
    }
    
    public void RemoveProductById(int id)
    {
        var product = Products.SingleOrDefault(p => p.Id == id);
        if (product != null)
        {
            Products.Remove(product);
        }
        else
        {
            Console.WriteLine($"Product with ID {id} not found.");
        }
    }

    public void EditProductById(int id, string name, string description, decimal price, DateTime startDate, DateTime endDate, int quantity)
    {
        var product = Products.SingleOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.StartDate = startDate;
            product.EndDate = endDate;
            product.Quantity = quantity;
        }
        else
        {
            Console.WriteLine($"Product with ID {id} not found.");
        }
    }
    
    public void ViewProductById(int id)
    {
        var product = Products.SingleOrDefault(p => p.Id == id);
        if (product != null)
        {
            Console.WriteLine(product);
        }
        else
        {
            Console.WriteLine($"Product with ID {id} not found.");
        }
    }
    
    public void ViewAllProducts()
    {
        foreach (var product in Products)
        {
            Console.WriteLine(product);
            // Console.WriteLine(new string('-', 20));
        }

        Console.WriteLine();
    }
    
    public void SortProductsByStartDate()
    {
        Products = Products.OrderBy(p => p.StartDate).ToList();
    }

    public void SortProductsByEndDate()
    {
        Products = Products.OrderBy(p => p.EndDate).ToList();
    }
}