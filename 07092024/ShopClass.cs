using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibLesson._07092024;

public class ShopClass
{
    public static void Main1(string[] args)
    {
        AddProduct();
        TestOrderService();
    }

    public static void AddProduct()
    {
        using (var context = new ApplicationShopDbContext())
        {
            context.Products.AddRange(
                new Product { Name = "Laptop", Price = 999.99M },
                new Product { Name = "Smartphone", Price = 599.99M },
                new Product { Name = "Tablet", Price = 399.99M }
            );
            context.SaveChanges();
        }

        Console.WriteLine("Products added successfully.");
    }

    public static void TestOrderService()
    {
        using (var context = new ApplicationShopDbContext())
        {
            var orderService = new OrderService(context);

            var newOrder = new Order
            {
                OrderDate = DateTime.Now,
                Products = context.Products.Take(2).ToList()
            };
            orderService.AddOrder(newOrder);

            orderService.ViewOrders();

            orderService.RemoveOrder(newOrder.Id);

            orderService.ViewOrders();
        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<Order> Orders { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public List<Product> Products { get; set; }
}

public class OrderService
{
    private readonly ApplicationShopDbContext _context;

    public OrderService(ApplicationShopDbContext context)
    {
        _context = context;
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        Console.WriteLine("Order added successfully.");
    }

    public void RemoveOrder(int orderId)
    {
        var order = _context.Orders.Find(orderId);
        if (order != null)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            Console.WriteLine("Order removed successfully.");
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }

    public void ViewOrders()
    {
        var orders = _context.Orders.Include(o => o.Products).ToList();
        foreach (var order in orders)
        {
            Console.WriteLine($"Order Id: {order.Id}, Date: {order.OrderDate}");
            foreach (var product in order.Products)
            {
                Console.WriteLine($"  Product: {product.Name}, Price: {product.Price}");
            }
        }
    }
}

public class ApplicationShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
}