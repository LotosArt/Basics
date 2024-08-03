namespace LibLesson._03082024;

public class SnakeClass
{
    private const int Width = 40;
    private const int Height = 20;
    private const char SnakeChar = 'O';
    private const char AppleChar = 'X';
    private const char EmptyChar = ' ';
    
    private static Queue<Point> snake = new Queue<Point>();
    private static Point apple;
    private static Direction currentDirection = Direction.Right;
    private static bool gameOver = false;
    private static Random random = new Random();
    
    public static void Main(string[] args)
    {
        Console.CursorVisible = false;
        InitializeGame();
        
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                ChangeDirection(key);
            }
            
            MoveSnake();
            Draw();
            Thread.Sleep(200); 
        }
        
        Console.Clear();
        Console.WriteLine("Game Over!");
        Environment.Exit(0);
    }
    
    private static void InitializeGame()
    {
        snake.Enqueue(new Point(Width / 2, Height / 2));
        PlaceApple();
        Draw();
    }

    private static void PlaceApple()
    {
        do
        {
            apple = new Point(random.Next(1, Width - 1), random.Next(1, Height - 1));
        } while (snake.Contains(apple));
    }

    private static void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (currentDirection != Direction.Down)
                    currentDirection = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (currentDirection != Direction.Up)
                    currentDirection = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (currentDirection != Direction.Right)
                    currentDirection = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (currentDirection != Direction.Left)
                    currentDirection = Direction.Right;
                break;
        }
    }
    
    private static void MoveSnake()
    {
        var head = snake.Last();
        Point newHead;
        
        switch (currentDirection)
        {
            case Direction.Up:
                newHead = new Point(head.X, head.Y - 1);
                break;
            case Direction.Down:
                newHead = new Point(head.X, head.Y + 1);
                break;
            case Direction.Left:
                newHead = new Point(head.X - 1, head.Y);
                break;
            case Direction.Right:
                newHead = new Point(head.X + 1, head.Y);
                break;
            default:
                newHead = head;
                break;
        }

        if (newHead.X <= 0 || newHead.X >= Width - 1 || newHead.Y <= 0 || newHead.Y >= Height - 1 || snake.Contains(newHead))
        {
            gameOver = true;
            return;
        }

        snake.Enqueue(newHead);

        if (newHead.Equals(apple))
        {
            PlaceApple();
        }
        else
        {
            snake.Dequeue();
        }
    }

    private static void Draw()
    {
        Console.Clear();
        
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                {
                    Console.Write('#'); 
                }
                else if (snake.Contains(new Point(x, y)))
                {
                    Console.Write(SnakeChar);
                }
                else if (apple.X == x && apple.Y == y)
                {
                    Console.Write(AppleChar);
                }
                else
                {
                    Console.Write(EmptyChar);
                }
            }
            Console.WriteLine();
        }
    }
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}

struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj is Point)
        {
            Point other = (Point)obj;
            return X == other.X && Y == other.Y;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

