namespace LibLesson._25072024;

public class MediaLibraryClass
{
    public static void Main1(string[] args)
    {
        Library library = new Library();

        library.AddItem(new Book("1984", "George Orwell", 1949));
        library.AddItem(new Magazine("National Geographic", "Various", 2023));
        library.AddItem(new DVD("Inception", "Christopher Nolan", 2010));

        library.SearchItem("1984");

        library.RemoveItem("1984");

        library.SearchItem("1984");

        library.DisplayAllItems();

    }
}

public interface IMediaItem
{
    string Title { get; }
    string Author { get; }
    int Year { get; }
    void DisplayInfo();
}

public interface IBorrowable
{
    void Borrow();
    void Return();
}

public class Book : IMediaItem, IBorrowable
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }
    public bool IsBorrowed { get; private set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
        IsBorrowed = false;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Book: {Title} by {Author}, published in {Year}");
    }

    public void Borrow()
    {
        if (!IsBorrowed)
        {
            IsBorrowed = true;
            Console.WriteLine($"Book '{Title}' has been borrowed.");
        }
        else
        {
            Console.WriteLine($"Book '{Title}' is already borrowed.");
        }
    }

    public void Return()
    {
        if (IsBorrowed)
        {
            IsBorrowed = false;
            Console.WriteLine($"Book '{Title}' has been returned.");
        }
        else
        {
            Console.WriteLine($"Book '{Title}' was not borrowed.");
        }
    }
}

public class Magazine : IMediaItem
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }

    public Magazine(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Magazine: {Title} by {Author}, published in {Year}");
    }
}

public class DVD : IMediaItem, IBorrowable
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }
    public bool IsBorrowed { get; private set; }

    public DVD(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
        IsBorrowed = false;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"DVD: {Title} by {Author}, released in {Year}");
    }

    public void Borrow()
    {
        if (!IsBorrowed)
        {
            IsBorrowed = true;
            Console.WriteLine($"DVD '{Title}' has been borrowed.");
        }
        else
        {
            Console.WriteLine($"DVD '{Title}' is already borrowed.");
        }
    }

    public void Return()
    {
        if (IsBorrowed)
        {
            IsBorrowed = false;
            Console.WriteLine($"DVD '{Title}' has been returned.");
        }
        else
        {
            Console.WriteLine($"DVD '{Title}' was not borrowed.");
        }
    }
}

public class Library
{
    private List<IMediaItem> items;

    public Library()
    {
        items = new List<IMediaItem>();
    }

    public void AddItem(IMediaItem item)
    {
        items.Add(item);
        Console.WriteLine($"{item.Title} has been added to the library.");
    }

    public void RemoveItem(string title)
    {
        IMediaItem item = items.FirstOrDefault(i => i.Title == title);
        if (item != null)
        {
            items.Remove(item);
            Console.WriteLine($"{title} has been removed from the library.");
        }
        else
        {
            Console.WriteLine($"{title} not found in the library.");
        }
    }

    public void SearchItem(string title)
    {
        IMediaItem item = items.FirstOrDefault(i => i.Title == title);
        if (item != null)
        {
            item.DisplayInfo();
        }
        else
        {
            Console.WriteLine($"{title} not found in the library.");
        }
    }

    public void DisplayAllItems()
    {
        foreach (var item in items)
        {
            item.DisplayInfo();
        }
    }
}

