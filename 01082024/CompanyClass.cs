namespace LibLesson._01082024;

public class CompanyClass
{
    public static void Main(string[] args)
    {
        Company company = new Company();

        company.AddEmployee(new Employee("Alice", "Manager", 2));
        company.AddEmployee(new Employee("Bob", "Director", 1));
        company.AddEmployee(new Employee("Charlie", "Engineer", 3));
        company.AddEmployee(new Employee("Dave", "CEO", 0));
        company.AddEmployee(new Employee("Eve", "Intern", 4));

        Console.WriteLine("Employees sorted by priority:");
        company.PrintEmployeesByPriority();
    }
}

class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public int Priority { get; set; }

    public Employee(string name, string position, int priority)
    {
        Name = name;
        Position = position;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Name} - {Position}";
    }
}

class Company
{
    private List<Employee> employees;

    public Company()
    {
        employees = new List<Employee>();
    }

    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
    }

    public void PrintEmployeesByPriority()
    {
        var sortedEmployees = employees.OrderBy(e => e.Priority).ToList();
        foreach (var employee in sortedEmployees)
        {
            Console.WriteLine(employee);
        }
    }
}
