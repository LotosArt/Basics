namespace LibLesson;

public class BusDepotClass
{
    public static void Main()
    {
        BusDepot depot = new BusDepot(10);
    
        depot.AddBus(new Bus(83, 30));
        depot.AddBus(new Bus(42, 25));
        depot.AddBus(new Bus(83, 40));
        depot.AddBus(new Bus(10, 20));
        depot.AddBus(new Bus(25, 15));
    
        depot.SortBusesByRouteNumber();
    
        Console.WriteLine("All buses:");
        depot.DisplayBuses();
    
        Bus[] busesOnRoute83 = depot.GetBusesOnRoute83();
        Console.WriteLine("\nBuses on route 83:");
        foreach (var bus in busesOnRoute83)
        {
            Console.WriteLine($"Route Number: {bus.RouteNumber}, Passenger Count: {bus.PassengerCount}");
        }
    
        Console.WriteLine($"\nTotal Passenger Count: {depot.TotalPassengerCount}");
    }
}

public class Bus
{
    public int RouteNumber { get; set; }
    public int PassengerCount { get; set; }

    public Bus(int routeNumber, int passengerCount)
    {
        RouteNumber = routeNumber;
        PassengerCount = passengerCount;
    }
}

public class BusDepot
{
    private Bus[] buses;
    private int busCount;

    public int BusCount
    {
        get => busCount;
        set => busCount = value;
    }

    public BusDepot(int capacity)
    {
        buses = new Bus[capacity];
        busCount = 0;
    }

    public void AddBus(Bus bus)
    {
        if (busCount < buses.Length)
        {
            buses[busCount] = bus;
            busCount++;
        }
        else
        {
            Console.WriteLine("Bus depot is full. Cannot add more buses.");
        }
    }

    public int TotalPassengerCount
    {
        get
        {
            int total = 0;
            for (int i = 0; i < busCount; i++)
            {
                total += buses[i].PassengerCount;
            }
            return total;
        }
    }

    public Bus[] GetBusesOnRoute83()
    {
        int count = 0;
        for (int i = 0; i < busCount; i++)
        {
            if (buses[i].RouteNumber == 83)
            {
                count++;
            }
        }

        Bus[] busesOnRoute83 = new Bus[count];
        int index = 0;
        for (int i = 0; i < busCount; i++)
        {
            if (buses[i].RouteNumber == 83)
            {
                busesOnRoute83[index] = buses[i];
                index++;
            }
        }

        return busesOnRoute83;
    }

    public void SortBusesByRouteNumber()
    {
        for (int i = 0; i < busCount - 1; i++)
        {
            for (int j = 0; j < busCount - i - 1; j++)
            {
                if (buses[j].RouteNumber > buses[j + 1].RouteNumber)
                {
                    (buses[j], buses[j + 1]) = (buses[j + 1], buses[j]);
                }
            }
        }
    }

    public void DisplayBuses()
    {
        for (int i = 0; i < busCount; i++)
        {
            Console.WriteLine($"Route Number: {buses[i].RouteNumber}, Passenger Count: {buses[i].PassengerCount}");
        }
    }
}