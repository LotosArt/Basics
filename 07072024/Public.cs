namespace LibLesson._07072024;

public class Public
{
    public static void Main(string[] args)
    {
        Publication[] catalog = new Publication[]
        {
            new Book("The Great Gatsby", "Fitzgerald", 1925, "Scribner"),
            new Article("The Theory of Relativity", "Einstein", "Annalen der Physik", 17, 1905),
            new ElectronicResource("Learning C#", "Richter", "https://example.com/learning-csharp", "A comprehensive guide to C#.")
        };

        Console.WriteLine("Catalog:");
        foreach (var publication in catalog)
        {
            publication.DisplayInfo();
        }

        Console.WriteLine("\nSearch for author 'Einstein':");
        foreach (var publication in catalog)
        {
            if (publication.IsAuthor("Einstein"))
            {
                publication.DisplayInfo();
            }
        }
    }
}

public abstract class Publication
{
    public string Title { get; set; }
    public string AuthorLastName { get; set; }

    public Publication(string title, string authorLastName)
    {
        Title = title;
        AuthorLastName = authorLastName;
    }

    public abstract void DisplayInfo();

    public bool IsAuthor(string lastName)
    {
        return AuthorLastName.Equals(lastName);
    }
}

public class Book : Publication
{
    public int Year { get; set; }
    public string Publisher { get; set; }

    public Book(string title, string authorLastName, int year, string publisher)
        : base(title, authorLastName)
    {
        Year = year;
        Publisher = publisher;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Book: {Title}, Author: {AuthorLastName}, Year: {Year}, Publisher: {Publisher}");
    }
}

public class Article : Publication
{
    public string JournalName { get; set; }
    public int JournalNumber { get; set; }
    public int Year { get; set; }

    public Article(string title, string authorLastName, string journalName, int journalNumber, int year)
        : base(title, authorLastName)
    {
        JournalName = journalName;
        JournalNumber = journalNumber;
        Year = year;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Article: {Title}, Author: {AuthorLastName}, Journal: {JournalName}, Number: {JournalNumber}, Year: {Year}");
    }
}

public class ElectronicResource : Publication
{
    public string Link { get; set; }
    public string Annotation { get; set; }

    public ElectronicResource(string title, string authorLastName, string link, string annotation)
        : base(title, authorLastName)
    {
        Link = link;
        Annotation = annotation;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Electronic Resource: {Title}, Author: {AuthorLastName}, Link: {Link}, Annotation: {Annotation}");
    }
}
