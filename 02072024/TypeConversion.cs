namespace LibLesson._02072024;

public class TypeConversion
{
    public static void Main(string[] args)
    {
        Animal genericAnimal = new Animal("Generic Animal");
        Dog dog = new Dog("Spike");
        Cat cat = new Cat("Tom");

        Animal animalDog = dog; 
        if (animalDog is Dog)
        {
            Dog? castedDog = animalDog as Dog;
            castedDog?.Bark(); 
        }

        if (genericAnimal is Cat)
        {
            Cat? castedCat = genericAnimal as Cat; 
            castedCat?.Meow(); 
        }
        else
        {
            Console.WriteLine($"Cannot cast {genericAnimal.Name} to Cat");
        }

        if (cat is Animal)
        {
            Animal animalCat = cat as Animal; 
            animalCat?.DisplayInfo(); 
        }
    }
}

public class Animal
{
    public string Name { get; set; }

    public Animal(string name)
    {
        Name = name;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"This is an animal named {Name}");
    }
}

public class Dog : Animal
{
    public Dog(string name) : base(name) { }

    public void Bark()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}

public class Cat : Animal
{
    public Cat(string name) : base(name) { }

    public void Meow()
    {
        Console.WriteLine($"{Name} says: Meow!");
    }
}