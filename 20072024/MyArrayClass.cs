namespace LibLesson._20072024;

public class MyArrayClass
{
    public static void Main(string[] args)
    {
        var myArray = new MyArray<int>();
        myArray.ArrayChanged += message => Console.WriteLine($"Array changed: {message}");

        myArray.Add(10);
        myArray.AddRange([20, 30, 40]);
        myArray.Add(50, 60, 70);
        myArray.Insert(3, 25);
        myArray.RemoveAt(5);
        myArray[2] = 35;

        Console.WriteLine($"Array Length: {myArray.Length}");
        myArray.PrintArray();
    }
}

public class MyArray<T>
{
    private List<T> _items;

    public MyArray()
    {
        _items = new List<T>();
    }
    
    public int Length => _items.Count;

    public T this[int index]
    {
        get => _items[index];
        set
        {
            _items[index] = value;
            OnArrayChanged($"Element at index {index} was set to {value}");
        }
    }
    
    public event Action<string> ArrayChanged;
    
    public void Add(T item)
    {
        _items.Add(item);
        OnArrayChanged($"Added element: {item}");
    }

    public void AddRange(T[] items)
    {
        _items.AddRange(items);
        OnArrayChanged($"Added array of elements: {string.Join(", ", items)}");
    }

    public void Add(params T[] items)
    {
        _items.AddRange(items);
        OnArrayChanged($"Added elements: {string.Join(", ", items)}");
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > _items.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        
        _items.Insert(index, item);
        OnArrayChanged($"Inserted element {item} at index {index}");
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _items.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        
        var removedItem = _items[index];
        _items.RemoveAt(index);
        OnArrayChanged($"Removed element {removedItem} at index {index}");
    }

    public void PrintArray()
    {
        Console.WriteLine(string.Join(", ", _items));
    }

    protected virtual void OnArrayChanged(string obj)
    {
        ArrayChanged?.Invoke(obj);
    }
}