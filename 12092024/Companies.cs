using Microsoft.EntityFrameworkCore;

namespace LibLesson._12092024;

public class Companies
{
    public static void Main(string[] args)
    {
        using (var context = new ApplicationUserCompanyDbContext())
        {
            var company1 = new CompanyN { Name = "TechCorp" };
            var company2 = new CompanyN { Name = "InnovateLtd" };

            var employee1 = new Employee { FullName = "John Doe", Company = company1 };
            var employee2 = new Employee { FullName = "Jane Smith", Company = company1 };
            var employee3 = new Employee { FullName = "Sam Brown", Company = company2 };

            var project1 = new Project { ProjectName = "Project Alpha" };
            var project2 = new Project { ProjectName = "Project Beta" };

            employee1.Projects = new List<Project> { project1 };
            employee2.Projects = new List<Project> { project1, project2 };
            employee3.Projects = new List<Project> { project2 };

            context.Companies.AddRange(company1, company2);
            context.Employees.AddRange(employee1, employee2, employee3);
            context.Projects.AddRange(project1, project2);
    
            context.SaveChanges();
        }
        
        using (var context = new ApplicationUserCompanyDbContext())
        {
            int companyId = 1; 

            var projects = context.Projects
                .Where(p => p.Employees.Any(e => e.CompanyId == companyId))
                .ToList();

            foreach (var project in projects)
            {
                Console.WriteLine($"Project: {project.ProjectName}");
            }
        }


    }
}

public class CompanyN
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public int CompanyId { get; set; }
    public CompanyN Company { get; set; }

    public ICollection<Project> Projects { get; set; }
}

public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; }

    public ICollection<Employee> Employees { get; set; }
}

public class ApplicationUserCompanyDbContext : DbContext
{
    public DbSet<CompanyN> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    
    public ApplicationUserCompanyDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=UserCompanyDB;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@\"");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Projects)
            .WithMany(p => p.Employees)
            .UsingEntity<Dictionary<string, object>>(
                "EmployeeProject", // Имя таблицы соединения
                ep => ep.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                ep => ep.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId")
            );

        base.OnModelCreating(modelBuilder);
    }
}
