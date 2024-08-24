using System.Data;
using Microsoft.Data.SqlClient;

namespace LibLesson._22082024;

public class RecordsClass
{
    private static string connectionString = "Server=localhost;Database=RecordsDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@";
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
                
                InsertRecord("First Record", "This is the first record.", "Active");
                InsertRecord("Second Record", "This is the second record.", "Inactive");
                InsertRecord("Third Record", "This is the third record.", "Pending");

                Record record = GetRecordById(1); 
                Console.WriteLine(record);
                
                UpdateRecordById(1, "New DTitle", "New Description", "Pending");
                record = GetRecordById(1); 
                Console.WriteLine(record);

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
            SqlCommand command = new SqlCommand("CREATE DATABASE RecordsDB",connection);
            command.ExecuteNonQuery();
            Console.WriteLine("База данных создана");
        }
    }
    
    private static void CreateTable(SqlConnection connection)
    {
        string createTableQuery = @"
                CREATE TABLE Records (
                    Id INT PRIMARY KEY IDENTITY,
                    Title NVARCHAR(100),
                    Description NVARCHAR(255),
                    CreatedAt DATETIME,
                    UpdatedAt DATETIME,
                    Status NVARCHAR(50)
                )";
        using (SqlCommand command = new SqlCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Таблица «Records» успешно создана.");
        }
    }
    
    private static void StoreProcedures(SqlConnection connection)
    {
        string createInsertRecord = @"
                CREATE PROCEDURE InsertRecord
                    @Title NVARCHAR(100),
                    @Description NVARCHAR(255),
                    @Status NVARCHAR(50)
                AS
                BEGIN
                    INSERT INTO Records (Title, Description, CreatedAt, UpdatedAt, Status)
                    VALUES (@Title, @Description, GETDATE(), GETDATE(), @Status);
                END;
                GO";
        
        string createUpdateRecordById = @"
                CREATE PROCEDURE UpdateRecordById
                    @Id INT,
                    @Title NVARCHAR(100),
                    @Description NVARCHAR(255),
                    @Status NVARCHAR(50)
                AS
                BEGIN
                    UPDATE Records
                    SET Title = @Title, Description = @Description, UpdatedAt = GETDATE(), Status = @Status
                    WHERE Id = @Id;
                END;
                GO";
        
        string createGetRecordById = @"
                CREATE PROCEDURE GetRecordById
                    @Id INT
                AS
                BEGIN
                    SELECT Id, Title, Description, CreatedAt, UpdatedAt, Status
                    FROM Records
                    WHERE Id = @Id;
                END;
                GO";
        
        connection.OpenAsync();
        SqlCommand command = new SqlCommand(createInsertRecord, connection);
        command.ExecuteNonQueryAsync();
        command.CommandText = createUpdateRecordById;
        command.ExecuteNonQueryAsync();
        command.CommandText = createGetRecordById;
        command.ExecuteNonQueryAsync();
        Console.WriteLine("Хранимые процедуры добавлены в базу данных.");
    }
    
    static void InsertRecord(string title, string description, string status)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("InsertRecord", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Status", status);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine($"Record '{title}' inserted successfully.");
        }
    }
    
    static Record GetRecordById(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("GetRecordById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Record
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        CreatedAt = reader.GetDateTime(3),
                        UpdatedAt = reader.GetDateTime(4),
                        Status = reader.GetString(5)
                    };
                }
            }
        }
        return null;
    }
    
    static void UpdateRecordById(int id, string title, string description, string status)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("UpdateRecordById", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Status", status);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine($"Record with Id {id} updated successfully.");
        }
    }
}

public class Record
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Description: {Description}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}, Status: {Status}";
    }
}
