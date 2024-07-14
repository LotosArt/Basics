using System.Reflection;
using log4net;
using log4net.Config;

namespace LibLesson._14072024;

public class LogExceptionClass
{
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    
    public static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("14072024/log4net.config"));
        
        int[] numbers = new int[10];

        Random rand = new Random();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rand.Next(1, 101);
        }

        Console.WriteLine("Array: " + string.Join(", ", numbers));
        log.Info("Array: " + string.Join(", ", numbers));

        Console.WriteLine("Please enter two indexes of array (from 1 to 10):");

        try
        {
            int index1 = int.Parse(Console.ReadLine());
            int index2 = int.Parse(Console.ReadLine());

            if (index1 < 1 || index1 > 10 || index2 < 1 || index2 > 10)
            {
                throw new ArgumentOutOfRangeException("One or both indexes can't be less than 1 and greater than 10.");
            }

            int sum = numbers[index1 - 1] + numbers[index2 - 1];
            Console.WriteLine($"Sum of elements {index1} and {index2}: {sum}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine("You haven't entered a number");
            log.Error("You haven't entered a number", ex);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            log.Error(ex.Message, ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            log.Error("Error: ", ex);
        }
    }
}