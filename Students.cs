namespace LibLesson;

public class Students
{
    static void Main(string[] args)
    {
        Student[] students = new Student[]
        {
            new Student("Ivanov", 1),
            new Student("Petrov", 2),
            new Student("Sidorov", 1),
            new Student("Smirnov", 3),
            new Student("Volkov", 2),
            new Student("Semenov", 1)
        };
    
        Console.Write("Group number: ");
        int targetGroup;
        while (!int.TryParse(Console.ReadLine(), out targetGroup))
        {
            Console.Write("Incorrect input. Enter a group number: ");
        }
    
        Console.Write("Input first letter of the lastname: ");
        char targetLetter = Console.ReadLine()[0];
    
        string[] result = FindStudentsByGroupAndLetter(students, targetGroup, targetLetter);
    
        Console.WriteLine("Students of the group with lastnames start from necessary char:");
        foreach (string lastName in result)
        {
            Console.WriteLine(lastName);
        }
    }
    
    static string[] FindStudentsByGroupAndLetter(Student[] students, int group, char letter)
    {
        int count = 0;
        foreach (Student student in students)
        {
            if (student.GroupNumber == group && student.LastName.StartsWith(letter.ToString(), 
                    StringComparison.OrdinalIgnoreCase))
            {
                count++;
            }
        }

        string[] result = new string[count];
        int index = 0;
        foreach (Student student in students)
        {
            if (student.GroupNumber == group && student.LastName.StartsWith(letter.ToString(), 
                    StringComparison.OrdinalIgnoreCase))
            {
                result[index++] = student.LastName;
            }
        }

        return result;
    }
}

struct Student
{
    public readonly string LastName;
    public readonly int GroupNumber;

    public Student(string lastName, int groupNumber)
    {
        LastName = lastName;
        GroupNumber = groupNumber;
    }

    public override string ToString()
    {
        return $"{LastName} (Group {GroupNumber})";
    }
}