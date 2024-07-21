namespace LibLesson._21082024;

public class ConveyorClass
{
    public static void Main(string[] args)
    {
        MechanicHandler mechanic = new MechanicHandler();
        Conveyor conveyor = new Conveyor(5, mechanic);
        Loader loader = new Loader(10, conveyor);

        conveyor.MaterialsDepleted += loader.OnMaterialsDepleted;
        conveyor.ConveyorBroken += () => conveyor.FixConveyor();

        while (true)
        {
            conveyor.Operate();
            Console.WriteLine("Press Enter to simulate the next operation cycle, or type 'exit' to quit.");
            if (Console.ReadLine().ToLower() == "exit") break;
        }
    }
}

public interface IMechanic
{
    void FixConveyor();
}

public class MechanicHandler : IMechanic
{
    public void FixConveyor()
    {
        Console.WriteLine("Mechanic: Fixing the conveyor...");
        Console.WriteLine("Mechanic: Conveyor fixed.");
    }
}

public class Conveyor
{
    public delegate void ConveyorHandler();
    public event ConveyorHandler MaterialsDepleted;
    public event ConveyorHandler ConveyorBroken;

    private int materials;
    private Random random;
    private IMechanic mechanic;

    public Conveyor(int initialMaterials, IMechanic mechanic)
    {
        materials = initialMaterials;
        random = new Random();
        this.mechanic = mechanic;
    }

    public void Operate()
    {
        if (materials > 0)
        {
            materials--;
            Console.WriteLine($"Conveyor: Processing material. Remaining: {materials}");
            if (materials == 0)
            {
                MaterialsDepleted?.Invoke();
            }
        }

        if (random.Next(100) < 5) 
        {
            ConveyorBroken?.Invoke();
        }
    }

    public void LoadMaterials(int amount)
    {
        materials += amount;
        Console.WriteLine($"Loader: Loaded {amount} materials. Total: {materials}");
    }

    public void FixConveyor()
    {
        mechanic.FixConveyor();
    }
}

public class Loader
{
    private int loadAmount;
    private Conveyor conveyor;

    public Loader(int loadAmount, Conveyor conveyor)
    {
        this.loadAmount = loadAmount;
        this.conveyor = conveyor;
    }

    public void OnMaterialsDepleted()
    {
        Console.WriteLine("Loader: Materials depleted. Loading new materials...");
        conveyor.LoadMaterials(loadAmount);
    }
}