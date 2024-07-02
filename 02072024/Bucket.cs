namespace LibLesson._02072024;

public class Bucket
{
    public static void Main1(string[] args)
    {
        Check check = new Check("Laptop", 1999.99, 2.5, 3);

        check.Display();
    }
}

public class Product
{
    private string name;
    private double price;
    private double weight;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public double Price
    {
        get { return price; }
        set { price = value; }
    }

    public double Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public Product(string name, double price, double weight)
    {
        Name = name;
        Price = price;
        Weight = weight;
    }
}

public class Buy : Product
{
    private int quantity;
    private double totalPrice;
    private double totalWeight;

    public int Quantity
    {
        get { return quantity; }
        set
        {
            quantity = value;
            UpdateTotalPrice();
            UpdateTotalWeight();
        }
    }

    public double TotalPrice
    {
        get { return totalPrice; }
        private set { totalPrice = value; }
    }

    public double TotalWeight
    {
        get { return totalWeight; }
        private set { totalWeight = value; }
    }

    public Buy(string name, double price, double weight, int quantity)
        : base(name, price, weight)
    {
        Quantity = quantity;
    }

    private void UpdateTotalPrice()
    {
        TotalPrice = Price * Quantity;
    }

    private void UpdateTotalWeight()
    {
        TotalWeight = Weight * Quantity;
    }
}

public class Check : Buy
{
    public Check(string name, double price, double weight, int quantity)
        : base(name, price, weight, quantity)
    {
    }

    public void Display()
    {
        Console.WriteLine("Product Information:");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Price per unit: {Price:C}");
        Console.WriteLine($"Weight per unit: {Weight} kg");
        Console.WriteLine();
        Console.WriteLine("Purchase Information:");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Total Price: {TotalPrice:C}");
        Console.WriteLine($"Total Weight: {TotalWeight} kg");
    }
}