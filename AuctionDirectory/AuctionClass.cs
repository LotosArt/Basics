namespace LibLesson.AuctionDirectory;

public class AuctionClass
{
    public static void Main(string[] args)
    {
        Auction auction = new Auction { Name = "My Auction" };

        auction.AddProduct(new Product { Name = "Product1", Description = "Description1", Price = 100.0m, 
            StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10), Quantity = 5 });
        auction.AddProduct(new Product { Name = "Product2", Description = "Description2", Price = 200.0m, 
            StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), Quantity = 3 });
        auction.AddProduct(new Product { Name = "Product3", Description = "Description3", Price = 150.0m, 
            StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(3), Quantity = 8 });


        Console.WriteLine("All Products:");
        auction.ViewAllProducts();

        auction.EditProductById(1, "Product1 Edited", "Description1 Edited", 150.0m, DateTime.Now, DateTime.Now.AddDays(15), 10);
        Console.WriteLine("Product with ID 1 after edit:");
        auction.ViewProductById(1);

        auction.RemoveProductById(2);
        Console.WriteLine("All Products after removing product with ID 2:");
        auction.ViewAllProducts();

        auction.SortProductsByStartDate();
        Console.WriteLine("All Products sorted by start date:");
        auction.ViewAllProducts();

        auction.SortProductsByEndDate();
        Console.WriteLine("All Products sorted by end date:");
        auction.ViewAllProducts();
    }
}