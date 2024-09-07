using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibLesson._07092024;

public class LibraryApp
{
    public static void Main(string[] args)
    {
        using (var context = new LibraryContext())
        {
            context.Books.Add(new Book
            {
                Title = "War and Peace",
                Author = "Leo Tolstoy",
                YearPublished = 1869,
                AvailableCopies = 5,
                Genre = "Historical Fiction"
            });

            context.Books.Add(new Book
            {
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                YearPublished = 1813,
                AvailableCopies = 3,
                Genre = "Romance"
            });

            context.SaveChanges();

            var books = context.Books.ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}");
            }
        }
    }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearPublished { get; set; }
    public int AvailableCopies { get; set; }
    public string Genre { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("LibraryConnection"));
    }
}