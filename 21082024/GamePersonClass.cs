namespace LibLesson._21082024;

public class GamePersonClass
{
    public static void Main(string[] args)
    {
        ICharacter archer = new Archer();
        ICharacter swordsman = new Swordsman();

        Console.WriteLine("Archer:");
        archer.ViewInventory();
        archer.ViewSkills();

        Console.WriteLine("\nSwordsman:");
        swordsman.ViewInventory();
        swordsman.ViewSkills();
    }
}

public interface ICharacter
{
    int Health { get; set; }
    int Defense { get; set; }
    int Attack { get; set; }
    string[] Skills { get; set; }
    string[] Inventory { get; set; }

    void ViewInventory();
    void ViewSkills();
}

public class Archer : ICharacter
{
    public int Health { get; set; }
    public int Defense { get; set; }
    public int Attack { get; set; }
    public string[] Skills { get; set; }
    public string[] Inventory { get; set; }

    public Archer()
    {
        Health = 100;
        Defense = 10;
        Attack = 20;
        Skills = ["Shooting", "Stealth", "Evasion"];
        Inventory = ["Bow", "Arrows", "Potion"];
    }

    public void ViewInventory()
    {
        Console.WriteLine("Inventory:");
        foreach (var item in Inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }

    public void ViewSkills()
    {
        Console.WriteLine("Skills:");
        foreach (var skill in Skills)
        {
            Console.WriteLine($"- {skill}");
        }
    }
}

public class Swordsman : ICharacter
{
    public int Health { get; set; }
    public int Defense { get; set; }
    public int Attack { get; set; }
    public string[] Skills { get; set; }
    public string[] Inventory { get; set; }

    public Swordsman()
    {
        Health = 120;
        Defense = 20;
        Attack = 15;
        Skills = ["Swordsmanship", "Parry", "Charge"];
        Inventory = ["Sword", "Shield", "Armor"];
    }

    public void ViewInventory()
    {
        Console.WriteLine("Inventory:");
        foreach (var item in Inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }

    public void ViewSkills()
    {
        Console.WriteLine("Skills:");
        foreach (var skill in Skills)
        {
            Console.WriteLine($"- {skill}");
        }
    }
}