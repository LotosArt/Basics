using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibLesson._26082024;

public class ProjectsManagementClass
{
    static string? _connectionString = "";
    
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        
        int changeRequestId = 1;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ChangeRequestID", changeRequestId);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine("Ошибка при утверждении запроса на изменение: " + e.Message);
            }
            finally
            {
                transaction.Commit();
                Console.WriteLine("Request has been approved.");
            }
        }
    }
}