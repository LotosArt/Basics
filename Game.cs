namespace LibLesson;

public class Game
{
    static void Main(string[] args)
    {
        
        Item sword = new Item("Sword", 10, 150, 5.0);
        Item shield = new Item("Shield", 8, 100, 7.5);
        Item potion = new Item("Health Potion", 5, 50, 0.5);
    
        Player player = new Player("Hero", 10);
    
        player.AddItem(sword);
        player.AddItem(shield);
        player.AddItem(potion);
    
        Console.WriteLine($"Total weight of inventory: {player.GetTotalWeight()} kg");
        Console.WriteLine($"Total value of inventory: {player.GetTotalValue()} gold");
    
        Item[] level10Items = player.FindItemsByLevel(10);
        Item[] value100Items = player.FindItemsByValue(100);
    
        Console.WriteLine("Items of level 10:");
        foreach (Item item in level10Items)
        {
            Console.WriteLine($"- {item.Name}");
        }
    
        Console.WriteLine("Items with value 100:");
        foreach (Item item in value100Items)
        {
            Console.WriteLine($"- {item.Name}");
        }
    
        player.RemoveItem(sword);
    }
}

public class Item
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Value { get; set; }
    public double Weight { get; set; }

    public Item(string name, int level, int value, double weight)
    {
        Name = name;
        Level = level;
        Value = value;
        Weight = weight;
    }
}

public class Player
{
    public string Name { get; set; }
    private Item[] Inventory { get; set; }
    private int itemCount;

    public Player(string name, int inventorySize)
    {
        Name = name;
        Inventory = new Item[inventorySize];
        itemCount = 0;
    }

    public void AddItem(Item item)
    {
        if (itemCount < Inventory.Length)
        {
            Inventory[itemCount] = item;
            itemCount++;
            Console.WriteLine($"Item '{item.Name}' added to {Name}'s inventory.");
        }
        else
        {
            Console.WriteLine($"Inventory is full. Cannot add item '{item.Name}'.");
        }
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (Inventory[i] == item)
            {
                Inventory[i] = Inventory[itemCount - 1];
                Inventory[itemCount - 1] = null;
                itemCount--;
                Console.WriteLine($"Item '{item.Name}' removed from {Name}'s inventory.");
                return;
            }
        }
        Console.WriteLine($"Item '{item.Name}' not found in {Name}'s inventory.");
    }

    public double GetTotalWeight()
    {
        double totalWeight = 0;
        for (int i = 0; i < itemCount; i++)
        {
            totalWeight += Inventory[i].Weight;
        }
        return totalWeight;
    }

    public int GetTotalValue()
    {
        int totalValue = 0;
        for (int i = 0; i < itemCount; i++)
        {
            totalValue += Inventory[i].Value;
        }
        return totalValue;
    }

    public Item[] FindItemsByLevel(int level)
    {
        Item[] foundItems = new Item[itemCount];
        int foundCount = 0;
        for (int i = 0; i < itemCount; i++)
        {
            if (Inventory[i].Level == level)
            {
                foundItems[foundCount] = Inventory[i];
                foundCount++;
            }
        }
        Array.Resize(ref foundItems, foundCount);
        return foundItems;
    }

    public Item[] FindItemsByValue(int value)
    {
        Item[] foundItems = new Item[itemCount];
        int foundCount = 0;
        for (int i = 0; i < itemCount; i++)
        {
            if (Inventory[i].Value == value)
            {
                foundItems[foundCount] = Inventory[i];
                foundCount++;
            }
        }
        Array.Resize(ref foundItems, foundCount);
        return foundItems;
    }
}