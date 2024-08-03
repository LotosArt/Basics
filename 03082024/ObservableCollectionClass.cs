using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LibLesson._03082024;

public class ObservableCollectionClass
{
    public static void Main(string[] args)
    {
        var userManager = new UserManager();

        userManager.AddUser(new User { Id = 1, Login = "alice", Password = "pass1", Email = "alice@example.com" });
        userManager.AddUser(new User { Id = 2, Login = "bob", Password = "pass2", Email = "bob@example.com" });
        userManager.AddUser(new User { Id = 3, Login = "carol", Password = "pass3", Email = "carol@example.com" });
        userManager.AddUser(new User { Id = 4, Login = "dave", Password = "pass4", Email = "dave@example.com" });
        userManager.AddUser(new User { Id = 5, Login = "eve", Password = "pass5", Email = "eve@example.com" });
        userManager.AddUser(new User { Id = 6, Login = "alice.doe", Password = "pass1", Email = "doe.alice@example.com" });
        userManager.AddUser(new User { Id = 7, Login = "eve.smith", Password = "pass5", Email = "smith.eve@example.com" });

        Console.WriteLine("\nUsers before sorting:");
        foreach (var user in userManager.Users)
        {
            Console.WriteLine(user);
        }

        var sortedByEmail = User.SortByEmail(userManager.Users);

        Console.WriteLine("\nUsers sorted by Email:");
        foreach (var user in sortedByEmail)
        {
            Console.WriteLine(user);
        }

        var sortedByLogin = User.SortByLogin(userManager.Users);

        Console.WriteLine("\nUsers sorted by Login:");
        foreach (var user in sortedByLogin)
        {
            Console.WriteLine(user);
        }

        Console.WriteLine();
        userManager.RemoveUser(userManager.Users[0]);
    }
}

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Login: {Login}, Email: {Email}";
    }

    public static ObservableCollection<User> SortByEmail(ObservableCollection<User> users)
    {
        var sortedUsers = new ObservableCollection<User>(users.OrderBy(u => u.Email));
        NotifySortChanges(users, sortedUsers);
        return sortedUsers;
    }

    public static ObservableCollection<User> SortByLogin(ObservableCollection<User> users)
    {
        var sortedUsers = new ObservableCollection<User>(users.OrderBy(user => user.Login));
        NotifySortChanges(users, sortedUsers);
        return sortedUsers;
    }

    private static void NotifySortChanges(ObservableCollection<User> original, ObservableCollection<User> sorted)
    {
        for (int i = 0; i < original.Count; i++)
        {
            if (original[i] != sorted[i])
            {
                Console.WriteLine($"\nUser changed position: {original[i]} -> {sorted[i]}");
            }
        }
    }
}

public class UserManager
{
    public ObservableCollection<User> Users { get; private set; }

    public UserManager()
    {
        Users = new ObservableCollection<User>();
        Users.CollectionChanged += Users_CollectionChanged;
    }

    private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (User newUser in e.NewItems)
            {
                Console.WriteLine($"User added: {newUser}");
            }
        }
        if (e.OldItems != null)
        {
            foreach (User oldUser in e.OldItems)
            {
                Console.WriteLine($"User removed: {oldUser}");
            }
        }
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void RemoveUser(User user)
    {
        Users.Remove(user);
    }
}
