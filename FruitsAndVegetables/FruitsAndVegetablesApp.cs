using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibLesson;

public class FruitsAndVegetablesApp
{
    static string? _connectionStringCreate = "";
    static string? _connectionString = "";
    
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        _connectionStringCreate = configuration.GetConnectionString("DefaultConnectionCreate");
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        
        CreateDatabase();
        
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                Console.WriteLine("Успешно подключено к базе данных «Овощи и фрукты».");
        
                CreateTable(connection);
                InsertData(connection);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Connection closed.");
        }
        
        Menu();
    }
    
    private static void CreateDatabase()
    {
        using (SqlConnection connection = new SqlConnection(_connectionStringCreate))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("CREATE DATABASE VegetablesAndFruitsDB",connection);
            command.ExecuteNonQuery();
            Console.WriteLine("База данных создана");
        }
    }
    
    private static void CreateTable(SqlConnection connection)
    {
        string createTableQuery = @"
                CREATE TABLE VegetablesAndFruits (
                    Id INT PRIMARY KEY IDENTITY,
                    Name NVARCHAR(50),
                    Color NVARCHAR(50),
                    Calories INT,
                    Type NVARCHAR(50)
                )";
        using (SqlCommand command = new SqlCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Таблица «Овощи и фрукты» успешно создана.");
        }
    }

    private static void InsertData(SqlConnection connection)
    {
        string insertQuery = @"
                INSERT INTO VegetablesAndFruits (Name, Color, Calories, Type) 
                VALUES 
                ('Apple', 'Red', 52, 'Fruit'),
                ('Banana', 'Yellow', 96, 'Fruit'),
                ('Carrot', 'Orange', 41, 'Vegetable'),
                ('Broccoli', 'Green', 34, 'Vegetable'),
                ('Tomato', 'Red', 18, 'Vegetable')";

        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Данные успешно добавлены в таблицу «Овощи и фрукты».");
        }
    }
    
    static void ShowAllData(SqlConnection connection)
    {
        string query = "SELECT * FROM VegetablesAndFruits";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Color"]}, {reader["Calories"]} калорий, {reader["Type"]}");
            }
        }
    }

    static void ShowAllNames(SqlConnection connection)
    {
        string query = "SELECT Name FROM VegetablesAndFruits";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }
        }
    }

    static void ShowAllColors(SqlConnection connection)
    {
        string query = "SELECT DISTINCT Color FROM VegetablesAndFruits";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Color"]);
            }
        }
    }

    static void ShowMaxCalories(SqlConnection connection)
    {
        string query = "SELECT MAX(Calories) FROM VegetablesAndFruits";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            Console.WriteLine($"Максимальная калорийность: {command.ExecuteScalar()} калорий");
        }
    }

    static void ShowMinCalories(SqlConnection connection)
    {
        string query = "SELECT MIN(Calories) FROM VegetablesAndFruits";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            Console.WriteLine($"Минимальная калорийность: {command.ExecuteScalar()} калорий");
        }
    }

    static void ShowAverageCalories(SqlConnection connection)
    {
        string query = "SELECT AVG(Calories) FROM VegetablesAndFruits";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            Console.WriteLine($"Средняя калорийность: {command.ExecuteScalar()} калорий");
        }
    }

    static void ShowCountOfVegetables(SqlConnection connection)
    {
        string query = "SELECT COUNT(*) FROM VegetablesAndFruits WHERE Type = 'Vegetable'";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            Console.WriteLine($"Количество овощей: {command.ExecuteScalar()}");
        }
    }

    static void ShowCountOfFruits(SqlConnection connection)
    {
        string query = "SELECT COUNT(*) FROM VegetablesAndFruits WHERE Type = 'Fruit'";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            Console.WriteLine($"Количество фруктов: {command.ExecuteScalar()}");
        }
    }

    static void ShowCountByColor(SqlConnection connection)
    {
        Console.WriteLine("Введите цвет:");
        string color = Console.ReadLine();
        string query = "SELECT COUNT(*) FROM VegetablesAndFruits WHERE Color = @Color";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Color", color);
            Console.WriteLine($"Количество овощей и фруктов цвета {color}: {command.ExecuteScalar()}");
        }
    }

    static void ShowByCaloriesRange(SqlConnection connection)
    {
        Console.WriteLine("Введите минимальную калорийность:");
        int minCalories = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальную калорийность:");
        int maxCalories = int.Parse(Console.ReadLine());

        string query = "SELECT * FROM VegetablesAndFruits WHERE Calories BETWEEN @MinCalories AND @MaxCalories";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@MinCalories", minCalories);
            command.Parameters.AddWithValue("@MaxCalories", maxCalories);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Calories"]} калорий");
            }
        }
    }
    
    private static void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n1. Отобразить всю информацию из таблицы");
            Console.WriteLine("2. Отобразить все названия овощей и фруктов");
            Console.WriteLine("3. Отобразить все цвета");
            Console.WriteLine("4. Показать максимальную калорийность");
            Console.WriteLine("5. Показать минимальную калорийность");
            Console.WriteLine("6. Показать среднюю калорийность");
            Console.WriteLine("7. Показать количество овощей");
            Console.WriteLine("8. Показать количество фруктов");
            Console.WriteLine("9. Показать количество овощей и фруктов заданного цвета");
            Console.WriteLine("10. Показать овощи и фрукты с калорийностью в диапазоне");
            Console.WriteLine("0. Выход");

            string? choice = Console.ReadLine();
            if (choice == "0") break;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    switch (choice)
                    {
                        case "1":
                            ShowAllData(connection);
                            break;
                        case "2":
                            ShowAllNames(connection);
                            break;
                        case "3":
                            ShowAllColors(connection);
                            break;
                        case "4":
                            ShowMaxCalories(connection);
                            break;
                        case "5":
                            ShowMinCalories(connection);
                            break;
                        case "6":
                            ShowAverageCalories(connection);
                            break;
                        case "7":
                            ShowCountOfVegetables(connection);
                            break;
                        case "8":
                            ShowCountOfFruits(connection);
                            break;
                        case "9":
                            ShowCountByColor(connection);
                            break;
                        case "10":
                            ShowByCaloriesRange(connection);
                            break;
                        default:
                            Console.WriteLine("Неверный ввод, попробуйте еще раз.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}