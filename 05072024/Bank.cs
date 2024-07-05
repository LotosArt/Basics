namespace LibLesson._05072024;

public class Bank
{
    public static void Main(string[] args)
    {
        Deposit deposit = new Deposit(1, "John Doe", 15000, 24, new DateTime(2022, 1, 1));
        deposit.GetDepositInfo();
        Console.WriteLine($"Close Deposit Amount: {deposit.CloseDeposit():C}\n");

        Credit credit = new Credit(2, "Jane Smith", 60000, 18, new DateTime(2023, 1, 1));
        credit.GetDepositInfo();
        Console.WriteLine($"Close Credit Amount: {credit.CloseDeposit():C}\n");
    }
}

public class Deposit
{
    private double amount;
    private int term;

    public virtual int Id { get; set; }
    public virtual string Owner { get; set; }
    public virtual double Amount
    {
        get => amount;
        set
        {
            if (value < 10000 || value > 200000)
                throw new Exception("Amount must be between 10000 and 200000");
            amount = value;
        }
    }
    public virtual int Term
    {
        get => term;
        set
        {
            if (value < 3 || value > 36)
                throw new Exception("Term must be between 3 and 36 months");
            term = value;
        }
    }
    public virtual double InterestRate => CalculateInterestRate();
    public virtual DateTime DepositDate { get; set; }

    public Deposit(int id, string owner, double amount, int term, DateTime depositDate)
    {
        Id = id;
        Owner = owner;
        Amount = amount;
        Term = term;
        DepositDate = depositDate;
    }

    protected virtual double CalculateInterestRate()
    {
        if (Amount < 50000)
        {
            return Term < 12 ? 0.03 : 0.04;
        }
        else if (Amount < 100000)
        {
            return Term < 12 ? 0.035 : 0.045;
        }
        else
        {
            return Term < 12 ? 0.04 : 0.05;
        }
    }

    public virtual void GetDepositInfo()
    {
        Console.WriteLine($"Deposit ID: {Id}");
        Console.WriteLine($"Owner: {Owner}");
        Console.WriteLine($"Amount: {Amount:C}");
        Console.WriteLine($"Term: {Term} months");
        Console.WriteLine($"Interest Rate: {InterestRate:P}");
        Console.WriteLine($"Deposit Date: {DepositDate:dd-MM-yyyy}");
    }

    public virtual double CloseDeposit()
    {
        int monthsElapsed = (DateTime.Now.Year - DepositDate.Year) * 12 + DateTime.Now.Month - DepositDate.Month;
        if (monthsElapsed > Term) monthsElapsed = Term;

        double interestEarned = Amount * InterestRate * monthsElapsed / 12;
        return Amount + interestEarned;
    }
}

public class Credit : Deposit
{
    public Credit(int id, string owner, double amount, int term, DateTime depositDate) 
        : base(id, owner, amount, term, depositDate) { }

    protected override double CalculateInterestRate()
    {
        if (Amount < 50000)
        {
            return Term < 12 ? 0.05 : 0.06;
        }
        else if (Amount < 100000)
        {
            return Term < 12 ? 0.055 : 0.065;
        }
        else
        {
            return Term < 12 ? 0.06 : 0.07;
        }
    }

    public override void GetDepositInfo()
    {
        Console.WriteLine($"Credit ID: {Id}");
        Console.WriteLine($"Owner: {Owner}");
        Console.WriteLine($"Amount: {Amount:C}");
        Console.WriteLine($"Term: {Term} months");
        Console.WriteLine($"Interest Rate: {InterestRate:P}");
        Console.WriteLine($"Credit Date: {DepositDate:dd-MM-yyyy}");
    }

    public override double CloseDeposit()
    {
        int monthsElapsed = (DateTime.Now.Year - DepositDate.Year) * 12 + DateTime.Now.Month - DepositDate.Month;
        if (monthsElapsed > Term) monthsElapsed = Term;

        double interestAccrued = Amount * InterestRate * monthsElapsed / 12;
        return Amount + interestAccrued;
    }
}