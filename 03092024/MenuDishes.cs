using Microsoft.EntityFrameworkCore;

namespace LibLesson._03092024;

public class MenuDishes
{
    public static void Main1(string[] args)
    {
        GetDishesFromMenu();
        
        var singleDish = new MenuDish
        {
            Name = "Борщ",
            Description = "Традиционный украинский суп",
            Price = 250.00m
        };
        AddDishToMenu(singleDish);
        
        var dishes = new List<MenuDish>
        {
            new MenuDish { Name = "Суп Харчо", Description = "Грузинский острый суп", Price = 300.00m },
            new MenuDish { Name = "Солянка", Description = "Русский суп из мяса", Price = 350.00m },
            new MenuDish { Name = "Пельмени", Description = "Традиционные русские пельмени", Price = 400.00m }
        };
        AddDishesToMenu(dishes);
        
        GetSoupDishes();
        
        GetDishById(1);
        GetLastAddedDish();
    }

    private static void GetLastAddedDish()
    {
        using (var context = new ApplicationDbContextDish())
        {
            var lastDish = context.MenuDishes
                .OrderByDescending(d => d.Id)
                .FirstOrDefault();

            if (lastDish != null)
            {
                Console.WriteLine($"Самое последнее блюдо: {lastDish.Name}, Описание: {lastDish.Description}, Цена: {lastDish.Price}");
            }
            else
            {
                Console.WriteLine("Блюда не найдены.");
            }
        }
    }

    private static void GetDishById(int dishId)
    {
        using (var context = new ApplicationDbContextDish())
        {
            var dish = context.MenuDishes.Find(dishId);

            if (dish != null)
            {
                Console.WriteLine($"Блюдо с ID {dishId}: {dish.Name}, Описание: {dish.Description}, Цена: {dish.Price}");
            }
            else
            {
                Console.WriteLine($"Блюдо с ID {dishId} не найдено.");
            }
        }
    }

    private static void GetSoupDishes()
    {
        using (var context = new ApplicationDbContextDish())
        {
            var soups = context.MenuDishes
                .Where(d => d.Name.Contains("Суп"))
                .ToList();

            Console.WriteLine("Блюда, содержащие слово 'Суп':");
            foreach (var soup in soups)
            {
                Console.WriteLine($"ID: {soup.Id}, Name: {soup.Name}, Description: {soup.Description}, Price: {soup.Price}");
            }
        }
    }

    private static void GetDishesFromMenu()
    {
        using (var context = new ApplicationDbContextDish())
        {
            if (context.Database.CanConnect())
            {
                var dishes = context.MenuDishes.ToList();
                Console.WriteLine("Все блюда в меню:");
                foreach (var dish in dishes)
                {
                    Console.WriteLine($"ID: {dish.Id}, Name: {dish.Name}, Description: {dish.Description}, Price: {dish.Price}");
                }
            }
            else
            {
                Console.WriteLine("Не удалось подключиться к базе данных.");
            }
        }
    }

    private static void AddDishToMenu(MenuDish singleDish)
    {
        using (var context = new ApplicationDbContextDish())
        {
            context.MenuDishes.Add(singleDish);
            context.SaveChanges();
        }
    }
    
    private static void AddDishesToMenu(List<MenuDish> dishes)
    {
        using (var context = new ApplicationDbContextDish())
        {
            context.MenuDishes.AddRange(dishes);
            context.SaveChanges();
        }
    }
}

public class ApplicationDbContextDish : DbContext
{
    public DbSet<MenuDish> MenuDishes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=MenuDished;TrustServerCertificate=True;User Id=sa;Password=Qwert12345!@");
    }
}

public class MenuDish
{
    public int Id { get; set; }           
    public string Name { get; set; }      
    public string Description { get; set; } 
    public decimal Price { get; set; }    
}