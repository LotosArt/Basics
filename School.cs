namespace LibLesson;

public class School
{
    static void Main(string[] args)
    {
        Console.Write("Enter the number of students: ");
        int n = int.Parse(Console.ReadLine());
    
        Student[] students = new Student[n];
    
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter details for student #{i + 1}:");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Class Name: ");
            string className = Console.ReadLine();
    
            students[i] = new Student(firstName, lastName, className);
        }
    
        Console.WriteLine("\nStudent Information:");
    
        foreach (var student in students)
        {
            student.DisplayInfo();
        }
    
        bool hasDuplicates = false;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (students[i].GetLastName() == students[j].GetLastName())
                {
                    hasDuplicates = true;
                    Console.WriteLine($"\nStudents with duplicate last names: {students[i].GetLastName()}");
                    break;
                }
            }
            if (hasDuplicates) break;
        }
    
        if (!hasDuplicates)
        {
            Console.WriteLine("\nNo students with duplicate last names found.");
        }
    // }
}

public class Student
{
    private string firstName;
    private string lastName;
    private string className;

    public Student()
    { }

    public Student(string firstName, string lastName, string className)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.className = className;
    }

    public void SetFirstName(string firstName)
    {
        this.firstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        this.lastName = lastName;
    }

    public void SetClassName(string className)
    {
        this.className = className;
    }

    public string GetFirstName()
    {
        return firstName;
    }

    public string GetLastName()
    {
        return lastName;
    }

    public string GetClassName()
    {
        return className;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Student {firstName} {lastName} from {className}");
    }
}