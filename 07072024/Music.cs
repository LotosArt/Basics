namespace LibLesson._07072024;

public class Music
{
    public static void Main(string[] args)
    {
        MusicalInstrument[] instruments = new MusicalInstrument[]
        {
            new Violin(),
            new Trombone(),
            new Ukulele(),
            new Cello()
        };

        foreach (var instrument in instruments)
        {
            instrument.Show();
            instrument.Desc();
            instrument.Sound();
            instrument.History();
            Console.WriteLine();
        }
    }
}

public abstract class MusicalInstrument
{
    public string Name { get; protected set; }
    public string Characteristics { get; protected set; }

    public MusicalInstrument(string name, string characteristics)
    {
        Name = name;
        Characteristics = characteristics;
    }

    public abstract void Sound();
    public void Show()
    {
        Console.WriteLine($"Instrument: {Name}");
    }
    public void Desc()
    {
        Console.WriteLine($"Description: {Characteristics}");
    }
    public abstract void History();
}

public class Violin : MusicalInstrument
{
    public Violin() : base("Violin", "A string instrument played with a bow.")
    {
    }

    public override void Sound()
    {
        Console.WriteLine("The violin makes a beautiful, high-pitched sound.");
    }

    public override void History()
    {
        Console.WriteLine("The violin has its origins in 16th century Italy.");
    }
}

public class Trombone : MusicalInstrument
{
    public Trombone() : base("Trombone", "A brass instrument with a telescopic slide.")
    {
    }

    public override void Sound()
    {
        Console.WriteLine("The trombone produces a rich, full sound.");
    }

    public override void History()
    {
        Console.WriteLine("The trombone was developed in the 15th century in Burgundy.");
    }
}

public class Ukulele : MusicalInstrument
{
    public Ukulele() : base("Ukulele", "A small, four-stringed instrument from Hawaii.")
    {
    }

    public override void Sound()
    {
        Console.WriteLine("The ukulele creates a bright, cheerful sound.");
    }

    public override void History()
    {
        Console.WriteLine("The ukulele was introduced to Hawaii by Portuguese immigrants in the 19th century.");
    }
}

public class Cello : MusicalInstrument
{
    public Cello() : base("Cello", "A large, low-pitched string instrument.")
    {
    }

    public override void Sound()
    {
        Console.WriteLine("The cello produces a deep, resonant sound.");
    }

    public override void History()
    {
        Console.WriteLine("The cello developed in the 16th century as part of the violin family.");
    }
}