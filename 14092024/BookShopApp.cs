using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace LibLesson._14092024;

public class BookShopApp
{
    private static BookContext _context;
    
    public static void Main1(string[] args)
    {
        using (var context = new BookContext())
        {
            var countyrCnt = GetBookCountByGenre(1);
        }
    }
    
    // 1) Получить количество книг определенного жанра
    public static int GetBookCountByGenre(int genreId)
    {
        return _context.Books.Count(b => b.GenreId == genreId);
    }

    // 2) Получить минимальную цену для книг определенного автора
    public static decimal GetMinPriceByAuthor(int authorId)
    {
        return _context.Books.Where(b => b.AuthorId == authorId).Min(b => b.Price);
    }

    // 3) Получить среднюю цену книг в определенном жанре
    public static decimal GetAveragePriceByGenre(int genreId)
    {
        return _context.Books.Where(b => b.GenreId == genreId).Average(b => b.Price);
    }

    // 4) Получить суммарную стоимость всех книг определенного автора
    public static decimal GetTotalPriceByAuthor(int authorId)
    {
        return _context.Books.Where(b => b.AuthorId == authorId).Sum(b => b.Price);
    }

    // 5) Выполнить группировку книг по жанрам
    public static IEnumerable<IGrouping<int, Book>> GroupBooksByGenre()
    {
        return _context.Books.GroupBy(b => b.GenreId);
    }

    // 6) Выбрать только названия книг определенного жанра
    public static IEnumerable<string> GetBookTitlesByGenre(int genreId)
    {
        return _context.Books.Where(b => b.GenreId == genreId).Select(b => b.Title);
    }

    // 7) Выбрать все книги, кроме тех, что относятся к определенному жанру, используя метод Except
    public static IEnumerable<Book> GetBooksExcludingGenre(int genreId)
    {
        var booksInGenre = _context.Books.Where(b => b.GenreId == genreId);
        return _context.Books.Except(booksInGenre);
    }

    // 8) Объединить книги от двух авторов, используя метод Union
    public static IEnumerable<Book> GetBooksFromTwoAuthors(int authorId1, int authorId2)
    {
        var booksByAuthor1 = _context.Books.Where(b => b.AuthorId == authorId1);
        var booksByAuthor2 = _context.Books.Where(b => b.AuthorId == authorId2);
        return booksByAuthor1.Union(booksByAuthor2);
    }

    // 9) Достать 5-ть самых дорогих книг
    public static IEnumerable<Book> GetTop5ExpensiveBooks()
    {
        return _context.Books.OrderByDescending(b => b.Price).Take(5);
    }

    // 10) Пропустить первые 10 книг и взять следующие 5
    public static IEnumerable<Book> GetNext5BooksAfter10()
    {
        return _context.Books.OrderBy(b => b.Id).Skip(10).Take(5);
    }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }

    public Author Author { get; set; }
    public Genre Genre { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; }
}


public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public BookContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId);

        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "Author 1" },
            new Author { Id = 2, Name = "Author 2" }
        );

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Fiction" },
            new Genre { Id = 2, Name = "Non-Fiction" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Book 1", AuthorId = 1, GenreId = 1, Price = 200 },
            new Book { Id = 2, Title = "Book 2", AuthorId = 2, GenreId = 2, Price = 150 },
            new Book { Id = 3, Title = "Book 3", AuthorId = 1, GenreId = 2, Price = 300 }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=BookShopDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@");
    }
}
