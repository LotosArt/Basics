using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibLesson._09092024;

public class AuthClass
{
    public static void Main1(string[] args)
    {
        using (var context = new AppAuthDbContext())
        {
            context.Database.EnsureCreated(); 
            
            var authService = new AuthService1(context);

            while (true)
            {
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Авторизация");
                Console.WriteLine("3. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите имя пользователя: ");
                        var username = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        var password = Console.ReadLine();

                        authService.Register(username, password);
                        break;

                    case "2":
                        Console.Write("Введите имя пользователя: ");
                        username = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        password = Console.ReadLine();

                        if (authService.Login(username, password))
                        {
                            Console.WriteLine("Добро пожаловать в систему!");
                        }
                        break;

                    case "3":
                        return;
                }
            }
        }
    }
}

public class User1
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class AppAuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=AuthDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}

public class AuthService1
{
    private readonly AppAuthDbContext _context;

    public AuthService1(AppAuthDbContext context)
    {
        _context = context;
    }

    public bool Register(string username, string password)
    {
        if (_context.Users.Any(u => u.Username == username))
        {
            Console.WriteLine("Пользователь с таким именем уже существует.");
            return false;
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User { Username = username, PasswordHash = passwordHash };
        
        _context.Users.Add(user);
        _context.SaveChanges();
        
        Console.WriteLine("Пользователь успешно зарегистрирован.");
        return true;
    }

    public bool Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            Console.WriteLine("Неправильное имя пользователя или пароль.");
            return false;
        }

        Console.WriteLine("Авторизация успешна.");
        return true;
    }
}
