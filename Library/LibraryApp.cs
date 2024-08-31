namespace LibLesson._31082024.Library;

public class LibraryApp
{
    public static void Main(string[] args)
    {
        LibraryDatabase library = new LibraryDatabase();

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Регистрация новой книги");
            Console.WriteLine("2. Регистрация нового читателя");
            Console.WriteLine("3. Выдача книги");
            Console.WriteLine("4. Возврат книги");
            Console.WriteLine("5. Просмотр информации о книгах");
            Console.WriteLine("6. Просмотр информации о выданных книгах");
            Console.WriteLine("7. Выход");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите название книги:");
                    string title = Console.ReadLine();

                    Console.WriteLine("Введите имя автора:");
                    string author = Console.ReadLine();

                    Console.WriteLine("Введите жанр:");
                    string genre = Console.ReadLine();

                    Console.WriteLine("Введите год издания:");
                    int year = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите количество доступных копий:");
                    int copies = int.Parse(Console.ReadLine());

                    library.AddBook(title, author, genre, year, copies);
                    break;

                case "2":
                    Console.WriteLine("Введите имя читателя:");
                    string readerName = Console.ReadLine();

                    Console.WriteLine("Введите email читателя:");
                    string email = Console.ReadLine();

                    Console.WriteLine("Введите номер телефона читателя:");
                    string phone = Console.ReadLine();

                    library.AddReader(readerName, email, phone);
                    break;

                case "3":
                    Console.WriteLine("Введите ID книги:");
                    int bookId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите ID читателя:");
                    int readerId = int.Parse(Console.ReadLine());

                    library.LoanBook(bookId, readerId);
                    break;

                case "4":
                    Console.WriteLine("Введите ID выдачи:");
                    int loanId = int.Parse(Console.ReadLine());

                    library.ReturnBook(loanId);
                    break;

                case "5":
                    library.GetBooksInfo();
                    break;

                case "6":
                    library.GetLoansInfo();
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine();
        }
    }
}