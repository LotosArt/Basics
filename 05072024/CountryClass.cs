namespace LibLesson._05072024;

public class CountryClass
{
    public static void Main(string[] args)
    {
        Country[] countries = new Country[]
        {
            new Country("USA", 50, 331000000),
            new Country("Germany", 16, 83000000),
            new Country("Japan", 47, 126000000),
            new Country("Brazil", 26, 211000000),
            new Country("India", 29, 1353000000)
        };

        City[] cities = new City[]
        {
            new City("New York", 5, 8400000, new string[] { "Wall Street", "Broadway", "Fifth Avenue" }),
            new City("Berlin", 1, 3500000, new string[] { "Unter den Linden", "Kurfürstendamm", "Friedrichstraße" }),
            new City("Tokyo", 1, 9200000, new string[] { "Ginza", "Shinjuku", "Akihabara" }),
            new City("São Paulo", 1, 12000000, new string[] { "Paulista Avenue", "Ibirapuera", "Liberdade" }),
            new City("Mumbai", 1, 12400000, new string[] { "Marine Drive", "Colaba", "Bandra" })
        };

        Console.WriteLine("Countries:");
        foreach (var country in countries)
        {
            country.DisplayInfo();
        }

        Console.WriteLine("\nSorted Countries:");
        Country.SortByName(countries);
        foreach (var country in countries)
        {
            country.DisplayInfo();
        }

        Console.WriteLine("\nCities:");
        foreach (var city in cities)
        {
            city.DisplayInfo();
        }

        Console.WriteLine("\nSorted Cities:");
        City.SortByName(cities);
        foreach (var city in cities)
        {
            city.DisplayInfo();
        }
    }
}

public class Country
{
    private string name;
    private int numberOfCities;
    private long totalPopulation;

    public string Name
    {
        get => name;
        set
        {
            if (value.Length <= 2)
                throw new Exception("Name must be longer than 2 characters.");
            name = value;
        }
    }

    public int NumberOfCities
    {
        get => numberOfCities;
        set
        {
            if (value <= 0)
                throw new Exception("Number of cities must be greater than 0.");
            numberOfCities = value;
        }
    }

    public long TotalPopulation
    {
        get => totalPopulation;
        set
        {
            if (value <= 0)
                throw new Exception("Total population must be greater than 0.");
            totalPopulation = value;
        }
    }

    public Country(string name, int numberOfCities, long totalPopulation)
    {
        Name = name;
        NumberOfCities = numberOfCities;
        TotalPopulation = totalPopulation;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Country: {Name}, Cities: {NumberOfCities}, Population: {TotalPopulation}");
    }

    public static void SortByName(Country[] countries)
    {
        Array.Sort(countries, (c1, c2) => c1.Name.CompareTo(c2.Name));
    }
}

public class City : Country
{
    private string[] streets;
    private long cityPopulation;

    public string[] Streets
    {
        get => streets;
        set => streets = value ?? throw new Exception("Streets cannot be null.");
    }

    public new long TotalPopulation
    {
        get => cityPopulation;
        set
        {
            if (value <= 0 || value > 10000000)
                throw new Exception("City population must be between 1 and 10 million.");
            cityPopulation = value;
        }
    }

    public City(string name, int numberOfCities, long totalPopulation, string[] streets)
        : base(name, numberOfCities, totalPopulation)
    {
        Streets = streets;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"City: {Name}, Population: {TotalPopulation}, Streets: {string.Join(", ", Streets)}");
    }

    public static void SortByName(City[] cities)
    {
        Array.Sort(cities, (c1, c2) => c1.Name.CompareTo(c2.Name));
    }
}
