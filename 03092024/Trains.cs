using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibLesson._03092024;

public class Trains
{
    public static void Main(string[] args)
    {
        var train = new Train
        {
            TrainNumber = "123A",
            DepartureStation = "Moscow",
            ArrivalStation = "Saint Petersburg",
            DepartureTime = new DateTime(2024, 9, 1, 8, 0, 0),
            ArrivalTime = new DateTime(2024, 9, 1, 12, 0, 0),
            TrainType = "Express"
        };

        AddTrain(train);
        
        var trains = GetAllTrains();
        foreach (var trainEl in trains)
        {
            Console.WriteLine($"Train {trainEl.TrainNumber}: {trainEl.DepartureStation} -> {trainEl.ArrivalStation}");
        }
        
        UpdateTrain(1, "456B", "Kazan");
        
        DeleteTrain(1);
    }
    
    public static void DeleteTrain(int id)
    {
        using (var context = new ApplicationDbContext())
        {
            var train = context.Trains.Find(id);
            if (train != null)
            {
                context.Trains.Remove(train);
                context.SaveChanges();
            }
        }
    }
    
    public static void UpdateTrain(int id, string newTrainNumber, string newDepartureStation)
    {
        using (var context = new ApplicationDbContext())
        {
            var train = context.Trains.Find(id);
            if (train != null)
            {
                train.TrainNumber = newTrainNumber;
                train.DepartureStation = newDepartureStation;
                context.SaveChanges();
            }
        }
    }
    
    public static List<Train> GetAllTrains()
    {
        using (var context = new ApplicationDbContext())
        {
            return context.Trains.ToList();
        }
    }
    
    public static void AddTrain(Train train)
    {
        using (var context = new ApplicationDbContext())
        {
            context.Trains.Add(train);
            context.SaveChanges();
        }
    }
}

public class Train
{
    public int Id { get; set; }
    public string TrainNumber { get; set; }
    public string DepartureStation { get; set; }
    public string ArrivalStation { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string TrainType { get; set; }
}

public class ApplicationDbContext : DbContext
{
    public DbSet<Train> Trains { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}
