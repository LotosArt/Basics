namespace LibLesson._12072024;

public class SingleLinkedListClass
{
    public static void Main(string[] args)
    {
        SingleLinkedList<int> list = new SingleLinkedList<int>();

        list.InsertAtBeginning(10);
        list.InsertAtEnd(20);
        list.InsertAt(1, 15);

        list.Display(); 

        list.DeleteFromBeginning();
        list.Display(); 

        list.DeleteFromEnd();
        list.Display();

        list.InsertAtEnd(25);
        list.DeleteNode(15);
        list.Display(); 

        Console.WriteLine("Count: " + list.Count); 
    }
}

public class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}

public class SingleLinkedList<T>
{
    private Node<T> head;
    private int count;

    public SingleLinkedList()
    {
        head = null;
        count = 0;
    }

    public void InsertAtBeginning(T data)
    {
        Node<T> newNode = new Node<T>(data);
        newNode.Next = head;
        head = newNode;
        count++;
    }

    public void InsertAtEnd(T data)
    {
        Node<T> newNode = new Node<T>(data);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    public void InsertAt(int index, T data)
    {
        if (index < 0 || index > count)
        {
            throw new Exception( "Index out of bounds");
        }

        if (index == 0)
        {
            InsertAtBeginning(data);
            return;
        }

        Node<T> newNode = new Node<T>(data);
        Node<T> current = head;
        for (int i = 0; i < index - 1; i++)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
        count++;
    }

    public void DeleteFromBeginning()
    {
        if (head == null)
        {
            throw new Exception("List is empty");
        }

        head = head.Next;
        count--;
    }

    public void DeleteFromEnd()
    {
        if (head == null)
        {
            throw new Exception("List is empty");
        }

        if (head.Next == null)
        {
            head = null;
        }
        else
        {
            Node<T> current = head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }

            current.Next = null;
        }
        count--;
    }

    public bool DeleteNode(T data)
    {
        if (head == null)
        {
            return false;
        }

        if (head.Data.Equals(data))
        {
            head = head.Next;
            count--;
            return true;
        }

        Node<T> current = head;
        while (current.Next != null)
        {
            if (current.Next.Data.Equals(data))
            {
                current.Next = current.Next.Next;
                count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public void Display()
    {
        Node<T> current = head;
        while (current != null)
        {
            Console.Write(current.Data + " -> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }

    public int Count => count;
}
