namespace LibLesson._03082024;

public class DictionaryClass
{
    public static void Main(string[] args)
    {
        var dictionary = new EnglishFrenchDictionary();

        dictionary.AddTranslation("apple", "pomme");
        dictionary.AddTranslation("apple", "pommette");
        dictionary.AddTranslation("book", "livre");
        dictionary.AddTranslation("book", "bouquin");

        var appleTranslations = dictionary["apple"];
        Console.WriteLine("Translations for 'apple': " + string.Join(", ", appleTranslations));

        dictionary.RemoveTranslation("apple", "pommette");

        appleTranslations = dictionary["apple"];
        Console.WriteLine("Translations for 'apple' after removal: " + string.Join(", ", appleTranslations));

        dictionary.RemoveWord("book");

        dictionary.DisplayAllTranslations();
    }
}

public class EnglishFrenchDictionary
{
    private Dictionary<string, List<string>> dictionary;

    public EnglishFrenchDictionary()
    {
        dictionary = new Dictionary<string, List<string>>();
    }
    
    public void AddTranslation(string english, string french)
    {
        if (!dictionary.ContainsKey(english))
        {
            dictionary[english] = new List<string>();
        }

        dictionary[english].Add(french);
    }
    
    public bool RemoveTranslation(string english, string french)
    {
        if (dictionary.ContainsKey(english))
        {
            return dictionary[english].Remove(french);
        }

        return false;
    }
    
    public bool RemoveWord(string english)
    {
        return dictionary.Remove(english);
    }

    public List<string> GetTranslations(string english)
    {
        if (dictionary.ContainsKey(english))
        {
            return dictionary[english];
        }

        return new List<string>();
    }

    public List<string> this[string english] => GetTranslations(english);

    public void DisplayAllTranslations()
    {
        foreach (var entry in dictionary)
        {
            Console.WriteLine($"English: {entry.Key}");
            foreach (var translation in entry.Value)
            {
                Console.WriteLine($"French: {translation}");
            }
        }
    }
}