namespace LibLesson;

public class PersonData
{
    static void Main(string[] args)
    {
        PhoneNumber phone1 = new PhoneNumber("123-456-7890");
        PhoneNumber phone2 = new PhoneNumber("123-456-7890");
        PhoneNumber phone3 = new PhoneNumber("987-654-3210");
    
        Address address1 = new Address("123 Main St", phone1);
        Address address2 = new Address("456 Elm St", phone2);
        Address address3 = new Address("789 Oak St", phone3);
    
        Person person1 = new Person("John Doe", address1);
        Person person2 = new Person("Jane Smith", address2);
        Person person3 = new Person("Alice Johnson", address3);
    
        Console.WriteLine(person1 == person2); 
        Console.WriteLine(person1 == person3); 
        
        string personToString = person1;
        Console.WriteLine($"String representation: {personToString}");
    
        Person stringToPerson = (Person)personToString;
        Console.WriteLine($"Person created from string: {stringToPerson.Name}, " +
                          $"{stringToPerson.Address.Street}, " +
                          $"{stringToPerson.Address.PhoneNumber.Number}");
    
    }
}

public class PhoneNumber
{
    public string Number { get; }

    public PhoneNumber(string number)
    {
        Number = number;
    }
}

public class Address
{
    public string Street { get; }
    public PhoneNumber PhoneNumber { get; }

    public Address(string street, PhoneNumber phoneNumber)
    {
        Street = street;
        PhoneNumber = phoneNumber;
    }
}

public class Person
{
    public string Name { get; }
    public Address Address { get; }

    public Person(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public static bool operator ==(Person p1, Person p2)
    {
        if (ReferenceEquals(p1, null) && ReferenceEquals(p2, null))
        {
            return true;
        }

        if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null))
        {
            return false;
        }
        
        return p1.Address.PhoneNumber.Number == p2.Address.PhoneNumber.Number;
    }

    public static bool operator !=(Person p1, Person p2)
    {
        return p1.Address.PhoneNumber.Number != p2.Address.PhoneNumber.Number;
    }
    
    public static explicit operator Person(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }

        string[] parts = str.Split(',');
        if (parts.Length != 3)
        {
            throw new Exception("Invalid string format.");
        }

        var name = parts[0];
        var street = parts[1];
        var phoneNumber = new PhoneNumber(parts[2]);

        var address = new Address(street, phoneNumber);
        return new Person(name, address);
    }
    
    public static implicit operator string(Person person)
    {
        if (person == null)
        {
            return null;
        }
        return $"{person.Name},{person.Address.Street},{person.Address.PhoneNumber.Number}";
    }
}

