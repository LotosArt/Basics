using Microsoft.EntityFrameworkCore;

namespace LibLesson._12092024;

public class CompanyShop
{
    public static void Main1(string[] args)
    {
     using (var context = new ApplicationShopDbContext())
     {
         var company1 = new Company { Name = "Company A" };
         var company2 = new Company { Name = "Company B" };

         var store1 = new Shop { Name = "Store 1", Company = company1 };
         var store2 = new Shop { Name = "Store 2", Company = company1 };
         var store3 = new Shop() { Name = "Store 3", Company = company2 };

         var customer1 = new Customer { FullName = "John Doe" };
         var customer2 = new Customer { FullName = "Jane Smith" };

         context.Companies.AddRange(company1, company2);
         context.Shops.AddRange(store1, store2, store3);
         context.Customers.AddRange(customer1, customer2);

         context.CustomerShops.AddRange(
             new CustomerShop { Customer = customer1, Shop = store1 },
             new CustomerShop { Customer = customer1, Shop = store2 },
             new CustomerShop { Customer = customer2, Shop = store2 },
             new CustomerShop { Customer = customer2, Shop = store3 }
         );

         context.SaveChanges();
     }
        
     using (var context = new ApplicationShopDbContext())
     {
         var companies = context.Companies
             .Include(c => c.Shops)
             .ThenInclude(s => s.CustomerShops)
             .ThenInclude(cs => cs.Customer)
             .ToList();

         foreach (var company in companies)
         {
             Console.WriteLine($"Компания: {company.Name}");

             foreach (var store in company.Shops)
             {
                 Console.WriteLine($"  Магазин: {store.Name}");

                 foreach (var customerStore in store.CustomerShops)
                 {
                     Console.WriteLine($"    Покупатель: {customerStore.Customer.FullName}");
                 }
             }
         }
     }
        
     using (var context = new ApplicationShopDbContext())
     {
         var customers = context.Customers
             .Include(c => c.CustomerShops)
             .ThenInclude(cs => cs.Shop)
             .ThenInclude(s => s.Company)
             .ToList();

         foreach (var customer in customers)
         {
             Console.WriteLine($"Покупатель: {customer.FullName}");

             foreach (var customerStore in customer.CustomerShops)
             {
                 var store = customerStore.Shop;
                 var company = store.Company;

                 Console.WriteLine($"  Магазин: {store.Name} (Компания: {company.Name})");
             }
         }
     }   
    }
}

public class Company
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public List<Shop> Shops { get; set; }
}

public class Shop
{
    public int ShopId { get; set; }
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public List<CustomerShop> CustomerShops { get; set; }
}

public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public List<CustomerShop> CustomerShops { get; set; }
}

public class CustomerShop
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public int ShopId { get; set; }
    public Shop Shop { get; set; }
}

public class ApplicationShopDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Company> Companies { get; set; } 
    public DbSet<Shop> Shops { get; set; }
    public DbSet<CustomerShop> CustomerShops { get; set; }
    public ApplicationShopDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=CustomerShopDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@\"");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerShop>().HasKey(x => new { x.CustomerId, x.ShopId });
        modelBuilder.Entity<CustomerShop>().HasOne(x => x.Shop).WithMany(x => x.CustomerShops).HasForeignKey(x => x.ShopId);
        modelBuilder.Entity<CustomerShop>().HasOne(x => x.Customer).WithMany(x => x.CustomerShops).HasForeignKey(x => x.CustomerId);
        base.OnModelCreating(modelBuilder);
        
    }
}