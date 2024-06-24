using PwdGenerator;

namespace PasswordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            PwdGenerator.Generator generator = new Generator();
            Console.WriteLine("Password with length 10: " + generator.Generate(10));
            Console.WriteLine("Password with length 15 and symbols: " + generator.Generate(15, true));
            Console.WriteLine("Password with random length and symbols: " + generator.Generate());
        }
    }    
}

// 1. Создать генератор паролей, используя отдельное пространство имен, класс и метод.
// Выполнить перегрузку метода, первый метод -  принимает длину пароля для генерации и
// возвращает сгенерированный пароль, второй метода принимает длину пароля и значение
// true/false для генерации символов ($#%?., и т.д.) в пароле, третий вариант метода –
// ничего не принимает и выполняет генерацию пароля с символами и случайной длинный от 5 до 30 символов.
// Реализовать проверки при передачи параметров методу, к примеру, максимальная длина пароля – 500 символов.

namespace PwdGenerator
{
    class Generator
    {
        private Random random = new Random();
        private const int MaxPasswordLength = 500;
        private string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string digits = "0123456789";
        private string symbols = "$#%?.,!@";
        
        private string GeneratePassword(int length, bool includeSymbols)
        {
            string password = "";
            string chars = letters + digits;

            if (includeSymbols)
            {
                chars += symbols;
            }

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                password += chars[index].ToString();
            }

            return password;
        }
        
        public string Generate(int len)
        {
            if (len < 1 || len > MaxPasswordLength)
            {
                Console.WriteLine($"Password length must be between 1 and {MaxPasswordLength}");
            }

            return GeneratePassword(len, false);
        }

        public string Generate(int len, bool includeSymbols)
        {
            if (len < 1 || len > MaxPasswordLength)
            {
                Console.WriteLine($"Password length must be between 1 and {MaxPasswordLength}");
            }

            return GeneratePassword(len, includeSymbols);
        }

        public string Generate()
        {
            int len = random.Next(5, 31); 
            return GeneratePassword(len, true);
        }

    }
}