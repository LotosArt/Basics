namespace LibLesson._20072024;

public class BasketClass
{
    public static void Main(string[] args)
    {
        Product product1 = new Product("Laptop", 999.99m, 5);
        Product product2 = new Product("Smartphone", 499.99m, 4);
        Product product3 = new Product("Tablet", 299.99m, 3);
        Product product4 = new Product("Smartwatch", 199.99m, 4);
        Product product5 = new Product("Headphones", 99.99m, 3);

        Category electronics = new Category("Electronics", new Product[] { product1, product2, product3 });
        Category accessories = new Category("Accessories", new Product[] { product4, product5 });

        electronics.DisplayProducts();
        Console.WriteLine();
        accessories.DisplayProducts();
        Console.WriteLine();
        
        User user = new User("john_doe", "password123");

        user.UserBasket.AddProduct(product1);
        user.UserBasket.AddProduct(product2);
        user.UserBasket.AddProduct(product4);
        Console.WriteLine();

        user.UserBasket.DisplayBasket();

    }
}

public class Product
{
    public Product(string name, decimal price, double rating)
    {
        Name = name;
        Price = price;
        Rating = rating;
    }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Rating { get; set; }
    
    public override string ToString()
    {
        return $"{Name} - ${Price} - Rating: {Rating}";
    }
}

public class Category
{
    public Category(string name, Product[] products)
    {
        Name = name;
        Products = products;
    }

    public string Name { get; set; }
    public Product[] Products { get; set; }
    
    public void DisplayProducts()
    {
        Console.WriteLine($"Category: {Name}");
        foreach (var product in Products)
        {
            Console.WriteLine(product);
        }
    }
}

public class Basket
{
    private List<Product> _purchasedProducts;
    
    public delegate void ProductAddedHandler(Product product);
    public event ProductAddedHandler ProductAdded;

    public Basket()
    {
        _purchasedProducts = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _purchasedProducts.Add(product);
        ProductAdded.Invoke(product);
    }

    public void DisplayBasket()
    {
        Console.WriteLine("Basket contains: ");
        foreach (var product in _purchasedProducts)
        {
            Console.WriteLine(product);
        }
    }
}

public class User
{
    public User(string login, string password)
    {
        Login = login;
        Password = password;
        UserBasket = new Basket();
        UserBasket.ProductAdded += OnProductAdded;
    }

    public string Login { get; set; }
    public string Password { get; set; }
    public Basket UserBasket { get; set; }
    
    private void OnProductAdded(Product product)
    {
        Console.WriteLine($"Product added to basket: {product}");
    }
}