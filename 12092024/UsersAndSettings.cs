using Microsoft.EntityFrameworkCore;

namespace LibLesson._12092024;

public class UsersAndSettings
{
    public static void Main1(string[] args)
    {
        using (var context = new ApplicationUserSettingsDbContext())
        {
            var user1 = new User
            {
                Name = "John",
                UserSettings = new UserSettings
                {
                    Theme = "Dark",
                    NotificationsEnabled = true
                }
            };

            var user2 = new User
            {
                Name = "Jane",
                UserSettings = new UserSettings
                {
                    Theme = "Light",
                    NotificationsEnabled = false
                }
            };

            var user3 = new User
            {
                Name = "Mark",
                UserSettings = new UserSettings
                {
                    Theme = "Dark",
                    NotificationsEnabled = true
                }
            };

            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();
        }
        
        using (var context = new ApplicationUserSettingsDbContext())
        {
            var user = context.Users
                .Include(u => u.UserSettings)  
                .FirstOrDefault(u => u.Id == 2);

            if (user != null)
            {
                Console.WriteLine($"User: {user.Name}, Theme: {user.UserSettings.Theme}");
            }
        }
        
        using (var context = new ApplicationUserSettingsDbContext())
        {
            var userToDelete = context.Users
                .Include(u => u.UserSettings)  
                .FirstOrDefault(u => u.Id == 3);

            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }
        }


    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UserSettings UserSettings { get; set; }
}

public class UserSettings
{
    public int Id { get; set; }
    public string Theme { get; set; }
    public bool NotificationsEnabled { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}

public class ApplicationUserSettingsDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    
    public ApplicationUserSettingsDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=CustomerUserSetingsDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@\"");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserSettings)
            .WithOne(us => us.User)
            .HasForeignKey<UserSettings>(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);  

        base.OnModelCreating(modelBuilder);
    }
}