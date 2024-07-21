namespace LibLesson._21082024;

public class BuildingCompanyClass
{
    public static void Main(string[] args)
    {
        House house = new House();

        List<IWorker> workers = new List<IWorker>
        {
            new Worker("Worker 1"),
            new Worker("Worker 2"),
            new Worker("Worker 3"),
            new TeamLeader("Team Leader")
        };

        Team team = new Team(workers);
        team.BuildHouse(house);
    }
}

public interface IPart
{
    string Name { get; }
    bool IsCompleted { get; set; }
}

public interface IWorker
{
    string Name { get; }
    void Work(House house);
}

public class Basement : IPart
{
    public string Name => "Basement";
    public bool IsCompleted { get; set; }
}

public class Wall : IPart
{
    public string Name => "Wall";
    public bool IsCompleted { get; set; }
}

public class Door : IPart
{
    public string Name => "Door";
    public bool IsCompleted { get; set; }
}

public class Window : IPart
{
    public string Name => "Window";
    public bool IsCompleted { get; set; }
}

public class Roof : IPart
{
    public string Name => "Roof";
    public bool IsCompleted { get; set; }
}


public class House
{
    public IPart Basement { get; } = new Basement();
    public List<IPart> Walls { get; } = new List<IPart> { new Wall(), new Wall(), new Wall(), new Wall() };
    public IPart Door { get; } = new Door();
    public List<IPart> Windows { get; } = new List<IPart> { new Window(), new Window(), new Window(), new Window() };
    public IPart Roof { get; } = new Roof();

    public bool IsCompleted => Basement.IsCompleted && Walls.All(w => w.IsCompleted) && Door.IsCompleted 
                               && Windows.All(w => w.IsCompleted) && Roof.IsCompleted;
}

public class Worker : IWorker
{
    public string Name { get; }

    public Worker(string name)
    {
        Name = name;
    }

    public void Work(House house)
    {
        if (!house.Basement.IsCompleted)
        {
            house.Basement.IsCompleted = true;
            Console.WriteLine($"{Name} completed the basement.");
        }
        else if (house.Walls.Any(part => !part.IsCompleted))
        {
            var wall = house.Walls.First(part => !part.IsCompleted);
            wall.IsCompleted = true;
            Console.WriteLine($"{Name} completed a wall.");
        }
        else if (!house.Door.IsCompleted)
        {
            house.Door.IsCompleted = true;
            Console.WriteLine($"{Name} completed the door.");
        }
        else if (house.Windows.Any(w => !w.IsCompleted))
        {
            var window = house.Windows.First(w => !w.IsCompleted);
            window.IsCompleted = true;
            Console.WriteLine($"{Name} completed a window.");
        }
        else if (!house.Roof.IsCompleted)
        {
            house.Roof.IsCompleted = true;
            Console.WriteLine($"{Name} completed the roof.");
        }
    }
}

public class TeamLeader : IWorker
{
    public string Name { get; }

    public TeamLeader(string name)
    {
        Name = name;
    }

    public void Work(House house)
    {
        Console.WriteLine("Team Leader Report:");
        Console.WriteLine($"Basement: {(house.Basement.IsCompleted ? "Completed" : "Not Completed")}");
        Console.WriteLine($"Walls: {house.Walls.Count(w => w.IsCompleted)} out of 4 completed");
        Console.WriteLine($"Door: {(house.Door.IsCompleted ? "Completed" : "Not Completed")}");
        Console.WriteLine($"Windows: {house.Windows.Count(w => w.IsCompleted)} out of 4 completed");
        Console.WriteLine($"Roof: {(house.Roof.IsCompleted ? "Completed" : "Not Completed")}\n");
    }
}

public class Team
{
    private List<IWorker> workers;

    public Team(List<IWorker> workers)
    {
        this.workers = workers;
    }

    public void BuildHouse(House house)
    {
        while (!house.IsCompleted)
        {
            foreach (var worker in workers)
            {
                worker.Work(house);
                if (house.IsCompleted) break;
            }
        }
        Console.WriteLine("The house is completed!");
    }
}