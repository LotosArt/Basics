using System.Diagnostics;

namespace LibLesson._07072024;

public class Shapes
{
    public static void Main(string[] args)
    {
        Random rand = new Random();
        Shape[] shapes = new Shape[1000];

        for (int i = 0; i < shapes.Length; i++)
        {
            int shapeType = rand.Next(3);
            switch (shapeType)
            {
                case 0:
                    shapes[i] = new Circle("Circle", rand.NextDouble() * 10);
                    break;
                case 1:
                    shapes[i] = new Rectangle("Rectangle", rand.NextDouble() * 10, rand.NextDouble() * 10);
                    break;
                case 2:
                    shapes[i] = new Triangle("Triangle", rand.NextDouble() * 10, rand.NextDouble() * 10);
                    break;
            }
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int count = 0;
        foreach (var shape in shapes)
        {
            if (shape is Circle)
            {
                count++;
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"Found {count} circles in {stopwatch.ElapsedMilliseconds} ms.");

    }
}

public abstract class Shape
{
    public string Name { get; set; }

    public Shape(string name)
    {
        Name = name;
    }

    public abstract double Area();
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(string name, double radius) : base(name)
    {
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(string name, double width, double height) : base(name)
    {
        Width = width;
        Height = height;
    }

    public override double Area()
    {
        return Width * Height;
    }
}

public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public Triangle(string name, double @base, double height) : base(name)
    {
        Base = @base;
        Height = height;
    }

    public override double Area()
    {
        return 0.5 * Base * Height;
    }
}