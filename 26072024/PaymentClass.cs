namespace LibLesson._26072024;

public class PaymentClass
{
    public static void Main(string[] args)
    {
        IPayment paypal = new Paypal();
        paypal.ProcessTransaction();
        Console.WriteLine();

        IPayment cod = new COD();
        cod.ProcessTransaction();
    }
}

interface IPayment
{
    void ProcessTransaction();
}

interface IBalanceOperations
{
    void CheckBalance();

    void DeductAmount();
}

abstract class Payment : IPayment
{
    public abstract void ProcessTransaction();
}

abstract class OnlinePayment : Payment, IBalanceOperations
{
    public abstract void CheckBalance();

    public abstract void DeductAmount();
}

class Paypal : OnlinePayment
{
    public override void CheckBalance()
    {
        Console.WriteLine("CheckBalance Method Called");
    }

    public override void DeductAmount()
    {
        Console.WriteLine("DeductAmount Method Called");
    }

    public override void ProcessTransaction()
    {
        Console.WriteLine("Paypal");
        CheckBalance();
        DeductAmount();
        Console.WriteLine("ProcessTransaction Method Called");
    }
}

class COD : Payment
{
    public override void ProcessTransaction()
    {
        Console.WriteLine("COD");
        Console.WriteLine("ProcessTransaction Method Called");
    }
}
