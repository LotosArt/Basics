namespace LibLesson._02072024;

public class OnlinePlatform
{
    public static void Main1(string[] args)
    {
        PaidCourse paidCourse1 = new PaidCourse("Advanced C#", "Learn advanced C# programming concepts.", 49.99);
        PaidCourse paidCourse2 = new PaidCourse("Data Structures", "Master data structures in C#.", 29.99);
        FreeCourse freeCourse1 = new FreeCourse("Introduction to C#", "Get started with C# programming.");

        User user = new User("John Doe", "john.doe@example.com");

        user.AddCourse(paidCourse1);
        user.AddCourse(paidCourse2);
        user.AddCourse(freeCourse1);

        Console.WriteLine($"User: {user.Name}, Email: {user.Email}");
        Console.WriteLine("Courses:");

        foreach (var course in user.GetCourses())
        {
            Console.WriteLine($"{course.Title}: {course.Description}");
        }

        Console.WriteLine($"Total amount for paid courses: ${user.GetTotalAmount()}");

        user.RemoveCourse(paidCourse1);
        Console.WriteLine("\nUpdated Courses:");

        foreach (var course in user.GetCourses())
        {
            Console.WriteLine($"{course.Title}: {course.Description}");
        }

        Console.WriteLine($"Total amount for paid courses: ${user.GetTotalAmount()}");
    }
}

public abstract class Course
{
    public string Title { get; set; }
    public string Description { get; set; }

    public Course(string title, string description)
    {
        Title = title;
        Description = description;
    }
}

public class PaidCourse : Course
{
    public double Price { get; set; }

    public PaidCourse(string title, string description, double price)
        : base(title, description)
    {
        Price = price;
    }
}

public class FreeCourse : Course
{
    public FreeCourse(string title, string description)
        : base(title, description)
    {
    }
}

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    private List<Course> courses;

    public User(string name, string email)
    {
        Name = name;
        Email = email;
        courses = new List<Course>();
    }

    public void AddCourse(Course course)
    {
        if (course == null)
        {
            throw new Exception("Course cannot be null");
        }
        courses.Add(course);
    }

    public void RemoveCourse(Course course)
    {
        if (course == null)
        {
            throw new Exception("Course cannot be null");
        }
        courses.Remove(course);
    }

    public double GetTotalAmount()
    {
        return courses.OfType<PaidCourse>().Sum(course => course.Price);
    }

    public List<Course> GetCourses()
    {
        return this.courses;
    }
}