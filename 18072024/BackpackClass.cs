namespace LibLesson._18072024;

public class BackpackClass
{
    public static void Main(string[] args)
    {
        try
        {
            Backpack backpack = new Backpack("Black", "Nike", "Polyester", 1.2, 25);

            Console.WriteLine(backpack);

            BackpackItem item1 = new BackpackItem("Book", 2);
            BackpackItem item2 = new BackpackItem("Bottle", 1.5);
            BackpackItem item3 = new BackpackItem("Laptop", 5);
            BackpackItem item4 = new BackpackItem("Clothes", 10);
            BackpackItem item5 = new BackpackItem("Food", 7);

            backpack.AddItem(item1);
            backpack.AddItem(item2);
            backpack.AddItem(item3);
            backpack.AddItem(item4);
            backpack.AddItem(item5); 

            Console.WriteLine(backpack);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

public class BackpackItem
    {
        public string Name { get; set; }
        public double Volume { get; set; }

        public BackpackItem(string name, double volume)
        {
            Name = name;
            Volume = volume;
        }
    }

    public class Backpack
    {
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public string Fabric { get; set; }
        public double Weight { get; set; }
        public double Capacity { get; set; }
        public List<BackpackItem> Contents { get; set; } = new List<BackpackItem>();

        public delegate void AddItemDelegate(BackpackItem item);
        public AddItemDelegate OnAddItem;

        public Backpack(string color, string manufacturer, string fabric, double weight, double capacity)
        {
            Color = color;
            Manufacturer = manufacturer;
            Fabric = fabric;
            Weight = weight;
            Capacity = capacity;

            OnAddItem += item =>
            {
                if (GetCurrentVolume() + item.Volume > Capacity)
                {
                    throw new InvalidOperationException("The volume of the backpack has been exceeded!");
                }
                else
                {
                    Contents.Add(item);
                    Console.WriteLine($"Item: {item.Name}, volume: {item.Volume}");
                }
            };
        }

        public void AddItem(BackpackItem item)
        {
            OnAddItem?.Invoke(item);
        }

        private double GetCurrentVolume()
        {
            double currentVolume = 0;
            foreach (var item in Contents)
            {
                currentVolume += item.Volume;
            }
            return currentVolume;
        }

        public override string ToString()
        {
            return $"Backpack: {Color}, {Manufacturer}, " +
                   $"{Fabric}, Weight: {Weight}, Capacity: {Capacity}, Volume: {GetCurrentVolume()}";
        }
    }