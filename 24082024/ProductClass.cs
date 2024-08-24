using System.Data;
using Microsoft.Data.SqlClient;

namespace LibLesson._22082024;

public class ProductClass
{
    private static string connectionString = "Server=localhost;Database=ProductsDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@";
    public static void Main(string[] args)
    {
        List<Product> products = new List<Product>();
        CreateDatabase();
        
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Соединение с базой данных установлено.");
                
                CreateTable(connection);

                int newProductId = AddProduct(connection, "Apple", 1.50m, products);
                Console.WriteLine($"Товар был добавлен с ID: {newProductId}");

                Console.WriteLine("Текущие товары в коллекции:");
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    private static void CreateDatabase()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("CREATE DATABASE ProductsDB",connection);
            command.ExecuteNonQuery();
            Console.WriteLine("База данных создана");
        }
    }
    
    private static void CreateTable(SqlConnection connection)
    {
        string createTableQuery = @"
                CREATE TABLE Products (
                    ProductID INT PRIMARY KEY IDENTITY,
                    ProductName NVARCHAR(100),
                    Price DECIMAL(10, 2)
                )";
        using (SqlCommand command = new SqlCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Таблица «Product» успешно создана.");
        }
    }

    static int AddProduct(SqlConnection connection, string productName, decimal price, List<Product> products)
    {
        string query = @"
                INSERT INTO Products (ProductName, Price) 
                VALUES (@ProductName, @Price);
                SET @NewProductID = SCOPE_IDENTITY();";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ProductName", productName);
            command.Parameters.AddWithValue("@Price", price);

            SqlParameter outputIdParam = new SqlParameter("@NewProductID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            command.ExecuteNonQuery();

            int newProductId = Convert.ToInt32(outputIdParam.Value);

            products.Add(new Product(newProductId, productName, price));

            return newProductId;
        }
    }
}

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }

    public Product(int productId, string productName, decimal price)
    {
        ProductID = productId;
        ProductName = productName;
        Price = price;
    }

    public override string ToString()
    {
        return $"ID: {ProductID}, Name: {ProductName}, Price: {Price}";
    }
}