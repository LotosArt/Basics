namespace LibLesson._01082024;

public class AudioRecordClass
{
    public static void Main(string[] args)
    {
        List<AudioRecord> originalList = new List<AudioRecord>
        {
            new(1, "Album1", "Author1", 9.99m),
            new(2, "Album2", "Author2", 12.99m),
            new(3, "Album3", "Author3", 15.99m)
        };

        List<AudioRecord> copiedList = new List<AudioRecord>();
        foreach (var record in originalList)
        {
            copiedList.Add((AudioRecord)record.Clone());
        }

        Console.WriteLine("Original List:");
        foreach (var record in originalList)
        {
            Console.WriteLine(record);
        }

        Console.WriteLine("\nCopied List:");
        foreach (var record in copiedList)
        {
            Console.WriteLine(record);
        }
    }
}

class AudioRecord : ICloneable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }

    public AudioRecord(int id, string title, string author, decimal price)
    {
        Id = id;
        Title = title;
        Author = author;
        Price = price;
    }

    // 1
    // public AudioRecord Clone()
    // {
    //     return new AudioRecord(Id, Title, Author, Price);
    // }

    // 2
    public object Clone()
    {
        return MemberwiseClone();
    }
    
    public override string ToString()
    {
        return $"ID: {Id}, Title: {Title}, Author: {Author}, Price: {Price:C}";
    }

}
