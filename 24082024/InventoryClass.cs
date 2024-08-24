using System.Data;
using Microsoft.Data.SqlClient;

namespace LibLesson._22082024;

public class InventoryClass
{
    private static string connectionString = "Server=localhost;Database=InventoryDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@";
    
    public static void Main(string[] args)
    {
        CreateDatabase();
        
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Соединение с базой данных установлено.");
                
                CreateTable(connection);
                StoreProcedures(connection);
                
                string objectName = "Office Chair";
                int objectQuantity = 10;
                string objectStatus = "В наличии";

                int objectId = AddInventoryObject(objectName, objectQuantity, objectStatus);

                Console.WriteLine($"Новый инвентарный объект добавлен с ID: {objectId}");
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
            SqlCommand command = new SqlCommand("CREATE DATABASE InventoryDB",connection);
            command.ExecuteNonQuery();
            Console.WriteLine("База данных создана");
        }
    }
    
    private static void CreateTable(SqlConnection connection)
    {
        string createTableQuery = @"
                CREATE TABLE Inventory (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100),
                    Quantity INT,
                    Status NVARCHAR(50)
                )";
        using (SqlCommand command = new SqlCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Таблица «Inventory» успешно создана.");
        }
    }
    
    private static void StoreProcedures(SqlConnection connection)
    {
        string createAddInventoryObject = @"
               CREATE PROCEDURE AddInventoryObject
                    @ObjectName NVARCHAR(100),
                    @ObjectQuantity INT,
                    @ObjectStatus NVARCHAR(50),
                    @ObjectID INT OUTPUT
                AS
                BEGIN
                    INSERT INTO Inventory (Name, Quantity, Status)
                    VALUES (@ObjectName, @ObjectQuantity, @ObjectStatus);

                    SET @ObjectID = SCOPE_IDENTITY();
                END;
                GO";
        
        
        connection.OpenAsync();
        SqlCommand command = new SqlCommand(createAddInventoryObject, connection);
        command.ExecuteNonQueryAsync();
        Console.WriteLine("Хранимая процедура добавлена в базу данных.");
    }
    
    static int AddInventoryObject(string objectName, int objectQuantity, string objectStatus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("AddInventoryObject", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ObjectName", objectName);
            command.Parameters.AddWithValue("@ObjectQuantity", objectQuantity);
            command.Parameters.AddWithValue("@ObjectStatus", objectStatus);

            SqlParameter outputIdParam = new SqlParameter("@ObjectID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (int)outputIdParam.Value;
        }
    }
}