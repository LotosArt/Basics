namespace LibLesson._05072024;

public class Warriors
{
    public static void Main(string[] args)
    {
        Warrior[] warriors = new Warrior[]
        {
            new Warrior(100),
            new LightArmoredWarrior(100),
            new HeavyArmoredWarrior(100)
        };

        int damage = 40;
        Console.WriteLine($"All warriors receive {damage} damage:");

        foreach (var warrior in warriors)
        {
            warrior.ReceiveDamage(damage);
            Console.WriteLine(warrior);
        }
    }
}

public class Warrior
{
    public int Health { get; private set; }

    public Warrior(int health)
    {
        Health = health;
    }

    public virtual void ReceiveDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public override string ToString()
    {
        return $"Warrior: Health = {Health}";
    }
}

public class LightArmoredWarrior : Warrior
{
    private const double DamageCoefficient = 0.75;

    public LightArmoredWarrior(int health) : base(health) { }

    public override void ReceiveDamage(int damage)
    {
        int effectiveDamage = (int)(damage * DamageCoefficient);
        base.ReceiveDamage(effectiveDamage);
    }

    public override string ToString()
    {
        return $"LightArmoredWarrior: Health = {Health}";
    }
}

public class HeavyArmoredWarrior : Warrior
{
    private const double DamageCoefficient = 0.5;

    public HeavyArmoredWarrior(int health) : base(health) { }

    public override void ReceiveDamage(int damage)
    {
        int effectiveDamage = (int)(damage * DamageCoefficient);
        base.ReceiveDamage(effectiveDamage);
    }

    public override string ToString()
    {
        return $"HeavyArmoredWarrior: Health = {Health}";
    }
}