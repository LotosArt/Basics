namespace LibLesson._25072024;

public class PhoneClass
{
    public static void Main1(string[] args)
    {
        Server server = new Server();

        Smartphone smartphone = new Smartphone("123-456-7890", server);
        Cellphone cellphone = new Cellphone("098-765-4321", server);

        Person alice = new Person("Alice", smartphone);
        Person bob = new Person("Bob", cellphone);

        alice.SendMessage("Hi Bob, this is Alice!", "098-765-4321");

        bob.CheckMessages();

        bob.SendMessage("Hello Alice, nice to hear from you!", "123-456-7890");

        alice.CheckMessages();

        Console.ReadLine();
    }
}

public interface ITelephone
{
    void SendMessage(string message, string receiverNumber);
    void ReceiveMessage();
    string PhoneNumber { get; }
}

public abstract class Phone : ITelephone
{
    public string PhoneNumber { get; private set; }
    protected Server Server;

    public Phone(string phoneNumber, Server server)
    {
        PhoneNumber = phoneNumber;
        Server = server;
    }

    public abstract void SendMessage(string message, string receiverNumber);
    public abstract void ReceiveMessage();
}

public class Server
{
    private Dictionary<string, List<string>> messages = new Dictionary<string, List<string>>();

    public void StoreMessage(string senderNumber, string receiverNumber, string message)
    {
        if (!messages.ContainsKey(receiverNumber))
        {
            messages[receiverNumber] = new List<string>();
        }
        messages[receiverNumber].Add($"From {senderNumber}: {message}");
    }

    public List<string> GetMessages(string phoneNumber)
    {
        if (messages.ContainsKey(phoneNumber))
        {
            List<string> userMessages = messages[phoneNumber];
            messages[phoneNumber] = new List<string>(); 
            return userMessages;
        }
        return new List<string> { "No new messages" };
    }
}

public class Smartphone : Phone
{
    public Smartphone(string phoneNumber, Server server) : base(phoneNumber, server) { }

    public override void SendMessage(string message, string receiverNumber)
    {
        Console.WriteLine($"[Smartphone] Sending message from {PhoneNumber} to {receiverNumber}: {message}");
        Server.StoreMessage(PhoneNumber, receiverNumber, message);
    }

    public override void ReceiveMessage()
    {
        List<string> messages = Server.GetMessages(PhoneNumber);
        Console.WriteLine($"[Smartphone] Messages for {PhoneNumber}:");
        foreach (var message in messages)
        {
            Console.WriteLine(message);
        }
    }
}

public class Cellphone : Phone
{
    public Cellphone(string phoneNumber, Server server) : base(phoneNumber, server) { }

    public override void SendMessage(string message, string receiverNumber)
    {
        Console.WriteLine($"[Cellphone] Sending message from {PhoneNumber} to {receiverNumber}: {message}");
        Server.StoreMessage(PhoneNumber, receiverNumber, message);
    }

    public override void ReceiveMessage()
    {
        List<string> messages = Server.GetMessages(PhoneNumber);
        Console.WriteLine($"[Cellphone] Messages for {PhoneNumber}:");
        foreach (var message in messages)
        {
            Console.WriteLine(message);
        }
    }
}

public class Person
{
    public string Name { get; private set; }
    private ITelephone phone;

    public Person(string name, ITelephone phone)
    {
        Name = name;
        this.phone = phone;
    }

    public void SendMessage(string message, string receiverNumber)
    {
        phone.SendMessage(message, receiverNumber);
    }

    public void CheckMessages()
    {
        phone.ReceiveMessage();
    }
}