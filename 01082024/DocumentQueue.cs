namespace LibLesson._01082024;

public class DocumentQueue
{
    public static void Main(string[] args)
    {
        PrintQueue printQueue = new PrintQueue();

        printQueue.Enqueue(new Document("Document1", DocumentPriority.Low));
        printQueue.Enqueue(new Document("Document2", DocumentPriority.High));
        printQueue.Enqueue(new Document("Document3", DocumentPriority.Medium));
        printQueue.Enqueue(new Document("Document4", DocumentPriority.High));
        printQueue.Enqueue(new Document("Document5", DocumentPriority.Low));

        while (printQueue.HasDocuments())
        {
            Document nextDoc = printQueue.Dequeue();
            Console.WriteLine($"Printing: {nextDoc}");
        }
    }
}

public enum DocumentPriority
{
    Low,    
    Medium, 
    High    
}

public class Document
{
    public string Name { get; set; }
    public DocumentPriority Priority { get; set; }

    public Document(string name, DocumentPriority priority)
    {
        Name = name;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Name} ({Priority} Priority)";
    }
}

public class PrintQueue
{
    private List<Document> documents = new List<Document>();

    public void Enqueue(Document document)
    {
        documents.Add(document);
        documents = documents.OrderByDescending(d => d.Priority).ToList();
    }

    public Document Dequeue()
    {
        if (documents.Count < 1)
        {
            throw new InvalidOperationException("Queue is empty");
            
        }
        
        Document doc = documents[0];
        documents.RemoveAt(0);
        return doc;
        
    }

    public bool HasDocuments()
    {
        return documents.Count > 0;
    }
}
