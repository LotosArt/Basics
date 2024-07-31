namespace LibLesson._01082024;

public class UniversityClass
{
    public static void Main(string[] args)
    {
        University university = new University();

        university.AddStudent(new Student("Alice", 3));
        university.AddStudent(new Student("Bob", 4));
        university.AddStudent(new Student("Charlie", 5));
        
        university.PrintStudentsWithHighGrades();
        Console.WriteLine();
        university.PrintAllStudents();
        
        university.AddStudent(new Student("Dave", 2));
        university.AddStudent(new Student("Eve", 4));

        Console.WriteLine("\nAfter add new students");
        university.PrintAllStudents();
    }
}

class Student
{
    public string Name { get; set; }
    public int Grade { get; set; }

    public Student(string name, int grade)
    {
        Name = name;
        Grade = grade;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Grade: {Grade}";
    }
}

class University
{
    private LinkedList<Student> students;

    public University()
    {
        students = new LinkedList<Student>();
    }

    public void AddStudent(Student student)
    {
        if (student.Grade >= 4)
        {
            students.AddFirst(student);
        }
        else
        {
            students.AddLast(student);
        }
    }

    public void PrintStudentsWithHighGrades()
    {
        Console.WriteLine("Students with grades 4 and above:");
        foreach (var student in students)
        {
            if (student.Grade >= 4)
            {
                Console.WriteLine(student);
            }
        }
    }

    public void PrintAllStudents()
    {
        Console.WriteLine("All students:");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
}

