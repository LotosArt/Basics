namespace LibLesson._14072024;

public class MoneyClass
{
    public static void Main(string[] args)
    {
        try
        {
            Money m1 = new Money(5, 75);
            Money m2 = new Money(3, 50);

            Money m3 = m1 + m2;
            Console.WriteLine($"Add: {m1} + {m2} = {m3}");

            Money m4 = m1 - m2;
            Console.WriteLine($"Minus: {m1} - {m2} = {m4}");

            Money m5 = m1 * 2;
            Console.WriteLine($"Multiply: {m1} * 2 = {m5}");

            Money m6 = m1 / 2;
            Console.WriteLine($"Divisor: {m1} / 2 = {m6}");

            m1++;
            Console.WriteLine($"Increment: {m1}");

            m1--;
            Console.WriteLine($"Decrement: {m1}");

            Console.WriteLine($">: {m1} > {m2} = {m1 > m2}");
            Console.WriteLine($"<: {m1} < {m2} = {m1 < m2}");
            Console.WriteLine($"==: {m1} == {m2} = {m1 == m2}");
            Console.WriteLine($"!=: {m1} != {m2} = {m1 != m2}");
        }
        catch (BankException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

public class Money
{
    private int hrn;
    private int kop;

    public int Hrn => hrn;

    public int Kop => kop;

    public Money(int hrn, int kop)
    {
        if (hrn < 0 || kop < 0 || (hrn == 0 && kop < 0))
        {
            throw new BankException("Amount can't be less than 0");
        }

        this.hrn = hrn + kop / 100;
        this.kop = kop % 100;
    }

    public override string ToString()
    {
        return $"{hrn} hrn {kop} kop";
    }

    public static Money operator +(Money a, Money b)
    {
        return new Money(a.hrn + b.hrn, a.kop + b.kop);
    }

    public static Money operator -(Money a, Money b)
    {
        int totalKopA = a.hrn * 100 + a.kop;
        int totalKopB = b.hrn * 100 + b.kop;

        if (totalKopA < totalKopB)
        {
            throw new BankException("You can't minus more than you have.");
        }

        int resultKops = totalKopA - totalKopB;
        return new Money(resultKops / 100, resultKops % 100);
    }

    public static Money operator /(Money a, int divisor)
    {
        if (divisor <= 0)
        {
            throw new BankException("Divisor should be more than 0.");
        }

        int totalKops = (a.hrn * 100 + a.kop) / divisor;
        return new Money(totalKops / 100, totalKops % 100);
    }

    public static Money operator *(Money a, int multiplier)
    {
        if (multiplier < 0)
        {
            throw new BankException("Multiplier should be more than 0.");
        }

        int totalKops = (a.hrn * 100 + a.kop) * multiplier;
        return new Money(totalKops / 100, totalKops % 100);
    }

    public static Money operator ++(Money a)
    {
        return new Money(a.hrn, a.kop + 1);
    }

    public static Money operator --(Money a)
    {
        if (a.hrn == 0 && a.kop == 0)
        {
            throw new BankException("The amount of money should not be less than 0.");
        }

        return new Money(a.hrn, a.kop - 1);
    }

    public static bool operator <(Money a, Money b)
    {
        return (a.hrn * 100 + a.kop) < (b.hrn * 100 + b.kop);
    }

    public static bool operator >(Money a, Money b)
    {
        return (a.hrn * 100 + a.kop) > (b.hrn * 100 + b.kop);
    }

    public static bool operator ==(Money a, Money b)
    {
        return (a.hrn * 100 + a.kop) == (b.hrn * 100 + b.kop);
    }

    public static bool operator !=(Money a, Money b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Money)
        {
            Money other = (Money)obj;
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (hrn * 100 + kop).GetHashCode();
    }
}

public class BankException : Exception
{
    public BankException(string message) : base(message) { }
}