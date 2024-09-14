using Microsoft.EntityFrameworkCore;

namespace LibLesson._14092024;

public class UniversityMangmentApp
{
    private static readonly UniversityContext _context;

    public static void Main(string[] args)
    {
        using (var context = new UniversityContext())
        {
            var studentsByCourxe = GetStudentsByCourse(1);
        }
    }
    
    public static IEnumerable<Student> GetStudentsByCourse(int courseId)
    {
        return _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.Student)
            .ToList();
    }

    // 2) Получить список курсов, на которых учит определенный преподаватель
    public static IEnumerable<Course> GetCoursesByInstructor(int instructorId)
    {
        return _context.Courses
            .Where(c => c.Instructors.Any(i => i.Id == instructorId))
            .ToList();
    }

    // 3) Получить список курсов, на которых учит определенный преподаватель, вместе с именами студентов
    public static IEnumerable<object> GetCoursesAndStudentsByInstructor(int instructorId)
    {
        return _context.Courses
            .Where(c => c.Instructors.Any(i => i.Id == instructorId))
            .Select(c => new
            {
                CourseTitle = c.Title,
                Students = c.Enrollments.Select(e => e.Student.FirstName + " " + e.Student.LastName).ToList()
            })
            .ToList();
    }

    // 4) Получить список курсов, на которые зачислено более 5 студентов
    public static IEnumerable<Course> GetCoursesWithMoreThanFiveStudents()
    {
        return _context.Courses
            .Where(c => c.Enrollments.Count > 5)
            .ToList();
    }

    // 5) Получить список студентов, старше 25 лет
     public static IEnumerable<Student> GetStudentsOlderThan25()
    {
        var currentDate = DateTime.Today;
        return _context.Students
            .Where(s => (currentDate.Year - s.BirthDate.Year) > 25)
            .ToList();
    }

    // 6) Получить средний возраст всех студентов
     public static double GetAverageStudentAge()
    {
        var currentDate = DateTime.Today;
        return _context.Students
            .Select(s => currentDate.Year - s.BirthDate.Year)
            .Average();
    }

    // 7) Получить самого молодого студента
     public static Student GetYoungestStudent()
    {
        return _context.Students
            .OrderByDescending(s => s.BirthDate)
            .FirstOrDefault();
    }

    // 8) Получить количество курсов, на которых учится студент с определенным Id
     public static int GetCourseCountByStudent(int studentId)
    {
        return _context.Enrollments
            .Count(e => e.StudentId == studentId);
    }

    // 9) Получить список имен всех студентов
     public static IEnumerable<string> GetAllStudentNames()
    {
        return _context.Students
            .Select(s => s.FirstName + " " + s.LastName)
            .ToList();
    }

    // 10) Сгруппировать студентов по возрасту
     public static IEnumerable<IGrouping<int, Student>> GroupStudentsByAge()
    {
        var currentDate = DateTime.Today;
        return _context.Students
            .GroupBy(s => currentDate.Year - s.BirthDate.Year)
            .ToList();
    }

    // 11) Получить список студентов, отсортированных по фамилии в алфавитном порядке
     public static IEnumerable<Student> GetStudentsSortedByLastName()
    {
        return _context.Students
            .OrderBy(s => s.LastName)
            .ToList();
    }

    // 12) Получить список студентов вместе с информацией о зачислениях на курсы
     public static IEnumerable<object> GetStudentsWithEnrollments()
    {
        return _context.Students
            .Select(s => new
            {
                StudentName = s.FirstName + " " + s.LastName,
                Courses = s.Enrollments.Select(e => e.Course.Title).ToList()
            })
            .ToList();
    }

    // 13) Получить список студентов, не зачисленных на определенный курс
     public static IEnumerable<Student> GetStudentsNotInCourse(int courseId)
    {
        var studentsInCourse = _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.StudentId);
        return _context.Students
            .Where(s => !studentsInCourse.Contains(s.Id))
            .ToList();
    }

    // 14) Получить список студентов, зачисленных одновременно на два определенных курса
     public static IEnumerable<Student> GetStudentsInTwoCourses(int courseId1, int courseId2)
    {
        return _context.Enrollments
            .Where(e => e.CourseId == courseId1 || e.CourseId == courseId2)
            .GroupBy(e => e.StudentId)
            .Where(g => g.Count() > 1)
            .Select(g => g.First().Student)
            .ToList();
    }

    // 15) Получить количество студентов на каждом курсе
     public static IEnumerable<object> GetStudentCountByCourse()
    {
        return _context.Courses
            .Select(c => new
            {
                CourseTitle = c.Title,
                StudentCount = c.Enrollments.Count
            })
            .ToList();
    }
}

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }

    public List<Enrollment> Enrollments { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public List<Enrollment> Enrollments { get; set; }
    public List<Instructor> Instructors { get; set; }
}

public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
}

public class Instructor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Course> Courses { get; set; }
}

public class UniversityContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }

    public UniversityContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        modelBuilder.Entity<Instructor>()
            .HasMany(i => i.Courses)
            .WithMany(c => c.Instructors)
            .UsingEntity(j => j.ToTable("CourseInstructor"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=UniversityManagmentDB;Trusted_Connection=True;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@\"");
    }
}
