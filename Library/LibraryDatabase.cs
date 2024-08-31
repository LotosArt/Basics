using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibLesson._31082024.Library;

public class LibraryDatabase
{
    private string _connectionString;

    public LibraryDatabase()
    {
        _connectionString = SetConnectionString();
    }

    public string SetConnectionString()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        return configuration.GetConnectionString("DefaultConnectionLib");
    }

    public void AddBook(string title, string authorName, string genreName, int publishedYear, int availableCopies)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            int authorId = GetAuthorId(connection, authorName);
            if (authorId == -1)
            {
                authorId = AddAuthor(connection, authorName, "Unknown");
            }

            int genreId = GetGenreId(connection, genreName);
            if (genreId == -1)
            {
                genreId = AddGenre(connection, genreName);
            }

            string sqlInsertBook = "INSERT INTO Books (Title, AuthorId, PublishedYear, GenreId, AvailableCopies) " +
                                   "VALUES (@Title, @AuthorId, @PublishedYear, @GenreId, @AvailableCopies)";
            using (SqlCommand command = new SqlCommand(sqlInsertBook, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@AuthorId", authorId);
                command.Parameters.AddWithValue("@PublishedYear", publishedYear);
                command.Parameters.AddWithValue("@GenreId", genreId);
                command.Parameters.AddWithValue("@AvailableCopies", availableCopies);

                command.ExecuteNonQuery();
            }
        }
    }

    public void AddReader(string name, string email, string phoneNumber)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sqlInsertReader = "INSERT INTO Readers (Name, Email, PhoneNumber) VALUES (@Name, @Email, @PhoneNumber)";
            using (SqlCommand command = new SqlCommand(sqlInsertReader, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                command.ExecuteNonQuery();
            }
        }
    }

    public void LoanBook(int bookId, int readerId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Проверка количества доступных копий
            string sqlCheckCopies = "SELECT AvailableCopies FROM Books WHERE BookId = @BookId";
            using (SqlCommand command = new SqlCommand(sqlCheckCopies, connection))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                int availableCopies = (int)command.ExecuteScalar();

                if (availableCopies > 0)
                {
                    // Уменьшение количества доступных копий
                    string sqlUpdateCopies = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookId = @BookId";
                    using (SqlCommand updateCommand = new SqlCommand(sqlUpdateCopies, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@BookId", bookId);
                        updateCommand.ExecuteNonQuery();
                    }

                    // Добавление новой записи о выдаче
                    string sqlInsertLoan = "INSERT INTO Loans (BookId, ReaderId, LoanDate) VALUES (@BookId, @ReaderId, @LoanDate)";
                    using (SqlCommand insertCommand = new SqlCommand(sqlInsertLoan, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@BookId", bookId);
                        insertCommand.Parameters.AddWithValue("@ReaderId", readerId);
                        insertCommand.Parameters.AddWithValue("@LoanDate", DateTime.Now);

                        insertCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    throw new InvalidOperationException("Нет доступных копий для выдачи.");
                }
            }
        }
    }

    public void ReturnBook(int loanId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Проверка существования записи о выдаче
            string sqlCheckLoan = "SELECT BookId FROM Loans WHERE LoanId = @LoanId AND ReturnDate IS NULL";
            using (SqlCommand command = new SqlCommand(sqlCheckLoan, connection))
            {
                command.Parameters.AddWithValue("@LoanId", loanId);
                object bookIdObj = command.ExecuteScalar();

                if (bookIdObj != null)
                {
                    int bookId = (int)bookIdObj;

                    // Обновление даты возврата
                    string sqlUpdateLoan = "UPDATE Loans SET ReturnDate = @ReturnDate WHERE LoanId = @LoanId";
                    using (SqlCommand updateCommand = new SqlCommand(sqlUpdateLoan, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                        updateCommand.Parameters.AddWithValue("@LoanId", loanId);

                        updateCommand.ExecuteNonQuery();
                    }

                    // Увеличение количества доступных копий
                    string sqlUpdateCopies = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookId = @BookId";
                    using (SqlCommand updateCopiesCommand = new SqlCommand(sqlUpdateCopies, connection))
                    {
                        updateCopiesCommand.Parameters.AddWithValue("@BookId", bookId);
                        updateCopiesCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    throw new InvalidOperationException("Книга уже была возвращена или неверный идентификатор выдачи.");
                }
            }
        }
    }

    public void GetBooksInfo()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sqlSelectBooks = @"SELECT b.Title, a.Name AS Author, g.GenreName AS Genre, b.AvailableCopies
                                      FROM Books b
                                      JOIN Authors a ON b.AuthorId = a.AuthorId
                                      JOIN Genres g ON b.GenreId = g.GenreId";

            using (SqlCommand command = new SqlCommand(sqlSelectBooks, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название: {reader["Title"]}, Автор: {reader["Author"]}, Жанр: {reader["Genre"]}, Доступные копии: {reader["AvailableCopies"]}");
                    }
                }
            }
        }
    }

    public void GetLoansInfo()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sqlSelectLoans = @"SELECT b.Title, r.Name AS Reader, l.LoanDate
                                      FROM Loans l
                                      JOIN Books b ON l.BookId = b.BookId
                                      JOIN Readers r ON l.ReaderId = r.ReaderId
                                      WHERE l.ReturnDate IS NULL";

            using (SqlCommand command = new SqlCommand(sqlSelectLoans, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Книга: {reader["Title"]}, Читатель: {reader["Reader"]}, Дата выдачи: {reader["LoanDate"]}");
                    }
                }
            }
        }
    }

    private int GetAuthorId(SqlConnection connection, string authorName)
    {
        string sqlSelectAuthor = "SELECT AuthorId FROM Authors WHERE Name = @Name";
        using (SqlCommand command = new SqlCommand(sqlSelectAuthor, connection))
        {
            command.Parameters.AddWithValue("@Name", authorName);
            object result = command.ExecuteScalar();
            return result == null ? -1 : (int)result;
        }
    }

    private int AddAuthor(SqlConnection connection, string authorName, string country)
    {
        string sqlInsertAuthor = "INSERT INTO Authors (Name, Country) OUTPUT INSERTED.AuthorId VALUES (@Name, @Country)";
        using (SqlCommand command = new SqlCommand(sqlInsertAuthor, connection))
        {
            command.Parameters.AddWithValue("@Name", authorName);
            command.Parameters.AddWithValue("@Country", country);

            return (int)command.ExecuteScalar();
        }
    }

    private int GetGenreId(SqlConnection connection, string genreName)
    {
        string sqlSelectGenre = "SELECT GenreId FROM Genres WHERE GenreName = @GenreName";
        using (SqlCommand command = new SqlCommand(sqlSelectGenre, connection))
        {
            command.Parameters.AddWithValue("@GenreName", genreName);
            object result = command.ExecuteScalar();
            return result == null ? -1 : (int)result;
        }
    }

    private int AddGenre(SqlConnection connection, string genreName)
    {
        string sqlInsertGenre = "INSERT INTO Genres (GenreName) OUTPUT INSERTED.GenreId VALUES (@GenreName)";
        using (SqlCommand command = new SqlCommand(sqlInsertGenre, connection))
        {
            command.Parameters.AddWithValue("@GenreName", genreName);

            return (int)command.ExecuteScalar();
        }
    }
}