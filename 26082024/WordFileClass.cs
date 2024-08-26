using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Word;

namespace LibLesson._26082024;

public class WordFileClass
{
    public static void Main(string[] args)
    {
        string filePath = @"C:\temp\document.docx";
        string connectionString = "Server=localhost;Database=FirstDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@";
        string fileName = "document.docx";
        int documentId = 1;  // Замените на актуальный Id документа
        
        CreateWordDocument(filePath, "This is a sample document.");
        UploadDocumentToDatabase(connectionString, fileName, filePath);
        SaveDocumentToDesktop(connectionString, documentId);
    }
    
    public static void CreateWordDocument(string filePath, string content)
    {
        Application wordApp = new Application();
        Document doc = wordApp.Documents.Add();

        Paragraph para = doc.Content.Paragraphs.Add();
        para.Range.Text = content;
        para.Range.InsertParagraphAfter();

        doc.SaveAs2(filePath);
        doc.Close();
        wordApp.Quit();

        Console.WriteLine("Word document created and saved successfully!");
    }
    
    public static void UploadDocumentToDatabase(string connectionString, string fileName, string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string query = "INSERT INTO Documents (Name, Filename, FileData) VALUES (@Name, @Filename, @FileData)";

                using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@Name", Path.GetFileNameWithoutExtension(fileName));
                    cmd.Parameters.AddWithValue("@Filename", fileName);
                    cmd.Parameters.AddWithValue("@FileData", fileData);

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                Console.WriteLine("Document uploaded successfully!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    
    public static void SaveDocumentToDesktop(string connectionString, int documentId)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Filename, FileData FROM Documents WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", documentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string filename = reader.GetString(0);
                        byte[] fileData = (byte[])reader["FileData"];

                        string filePath = Path.Combine(desktopPath, filename);
                        File.WriteAllBytes(filePath, fileData);

                        Console.WriteLine("Document saved to desktop: " + filePath);
                    }
                }
            }
        }
    }
}