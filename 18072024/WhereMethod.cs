namespace LibLesson._18072024;

public class WhereMethod
{
    public static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        int[] evenNumbers = Where(numbers, x => x % 2 == 0);

        Console.WriteLine("Even numbers:");
        foreach (int number in evenNumbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine("\n");
        
        User[] users = {
            new User { Name = "Alice", Age = 30 },
            new User { Name = "Bob", Age = 40 },
            new User { Name = "Charlie", Age = 25 },
            new User { Name = "David", Age = 35 },
            new User { Name = "Edward", Age = 50 }
        };

        User[] usersOlderThan35 = Where(users, user => user.Age > 35);

        Console.WriteLine("Users over 35 years old:");
        foreach (User user in usersOlderThan35)
        {
            Console.WriteLine($"{user.Name}, {user.Age}");
        }

    }
    
    public static T[] Where<T>(T[] array, Predicate<T> predicate)
    {
        List<T> result = new List<T>();

        foreach (T item in array)
        {
            if (predicate(item))
            {
                result.Add(item);
            }
        }

        return result.ToArray();
    }
}

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}