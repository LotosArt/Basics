using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LibLesson._09092024;

public class AuthNewClass
{
    public static void Main(string[] args)
    {
        using (var context = new AppAuthNewDbContext())
        {
            context.Database.Migrate(); 
        }

        var authService = new AuthService(new AppAuthNewDbContext());

        Console.WriteLine("Введите имя пользователя:");
        var username = Console.ReadLine();

        Console.WriteLine("Введите пароль:");
        var password = Console.ReadLine();

        if (authService.Login(username, password))
        {
            var bookService = new BookService(new AppAuthNewDbContext());
            int pageNumber = 1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню:\n1. Просмотр книг\n2. Поиск книги по названию\n3. Получить книгу по ID\n4. Получить последнюю добавленную книгу\n5. Выйти");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        bookService.DisplayBooks(pageNumber);
                        Console.WriteLine("Нажмите '>' для следующей страницы, '<' для предыдущей или любую другую клавишу для возврата в меню.");
                        var input = Console.ReadLine();
                        if (input == ">") pageNumber++;
                        else if (input == "<" && pageNumber > 1) pageNumber--;
                        break;

                    case "2":
                        Console.WriteLine("Введите ключевое слово для поиска:");
                        var keyword = Console.ReadLine();
                        bookService.SearchBookByTitle(keyword);
                        break;

                    case "3":
                        Console.WriteLine("Введите ID книги:");
                        if (int.TryParse(Console.ReadLine(), out int bookId))
                        {
                            var book = bookService.GetBookById(bookId);
                            if (book != null)
                                Console.WriteLine($"{book.Title} - {book.Author}");
                            else
                                Console.WriteLine("Книга не найдена.");
                        }
                        break;

                    case "4":
                        var latestBook = bookService.GetLatestBook();
                        Console.WriteLine($"Последняя книга: {latestBook.Title} - {latestBook.Author}");
                        break;

                    case "5":
                        return;
                }

                Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
            }
        }
    }
}

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } 
    public bool IsBlocked { get; set; } 
    public int FailedLoginAttempts { get; set; } 
}

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}

public class AppAuthNewDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=AuthNewDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Username = "admin", PasswordHash = HashPassword("admin123"), IsBlocked = false, FailedLoginAttempts = 0 }
        );
        
        modelBuilder.Entity<Book>().HasData(
            new Book { BookId = 1, Title = "War and Peace", Author = "Leo Tolstoy", Year = 1869 },
            new Book { BookId = 2, Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", Year = 1866 }
        );
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}

public class AuthService
{
    private readonly AppAuthNewDbContext _context;

    public AuthService(AppAuthNewDbContext context)
    {
        _context = context;
    }

    public bool Login(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        if (user == null || user.IsBlocked)
        {
            Console.WriteLine("Пользователь не найден или заблокирован.");
            return false;
        }

        if (user.PasswordHash == HashPassword(password))
        {
            user.FailedLoginAttempts = 0;
            _context.SaveChanges();
            Console.WriteLine("Авторизация успешна.");
            return true;
        }
        else
        {
            user.FailedLoginAttempts++;

            if (user.FailedLoginAttempts >= 3)
            {
                user.IsBlocked = true;
                Console.WriteLine("Пользователь заблокирован после 3 неудачных попыток.");
            }

            _context.SaveChanges();
            Console.WriteLine("Неверный пароль.");
            return false;
        }
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}

public class BookService
{
    private readonly AppAuthNewDbContext _context;

    public BookService(AppAuthNewDbContext context)
    {
        _context = context;
    }

    public void DisplayBooks(int pageNumber, int pageSize = 5)
    {
        var books = _context.Books
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.BookId}: {book.Title} - {book.Author} ({book.Year})");
        }
    }

    public void SearchBookByTitle(string keyword)
    {
        var books = _context.Books
            .Where(b => b.Title.Contains(keyword))
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.BookId}: {book.Title} - {book.Author} ({book.Year})");
        }
    }

    public Book GetBookById(int id)
    {
        return _context.Books.Find(id);
    }

    public Book GetLatestBook()
    {
        return _context.Books.OrderByDescending(b => b.BookId).FirstOrDefault();
    }
}
