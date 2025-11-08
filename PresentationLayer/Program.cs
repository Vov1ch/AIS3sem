using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.DataAccessLayer.Configuration;
using BookManagementSystem.DataAccessLayer.Contexts;
using BookManagementSystem.DataAccessLayer.Repositories;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookManagementSystem.PresentationLayer;

internal static class Program
{
    private enum DataProvider
    {
        EntityFramework = 1,
        Dapper = 2
    }

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var connectionString = SqliteConnectionProvider.GetDefaultConnectionString();
        var provider = AskForProvider();

        if (provider == DataProvider.EntityFramework)
        {
            using var context = BookDbContextFactory.Create(connectionString);
            var service = new BookService(new EfBookRepository(context));
            RunMenu(service);
        }
        else
        {
            IBookRepository repository = new DapperBookRepository(connectionString);
            var service = new BookService(repository);
            RunMenu(service);
        }
    }

    private static DataProvider AskForProvider()
    {
        Console.WriteLine("Выберите способ доступа к данным:");
        Console.WriteLine("1. Entity Framework Core");
        Console.WriteLine("2. Dapper");

        while (true)
        {
            var input = Console.ReadLine();
            if (input == "1")
            {
                return DataProvider.EntityFramework;
            }

            if (input == "2")
            {
                return DataProvider.Dapper;
            }

            Console.Write("Некорректный ввод. Повторите попытку: ");
        }
    }

    private static void RunMenu(BookService service)
    {
        while (true)
        {
            PrintMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowBooks(service.GetAllBooks());
                    break;
                case "2":
                    CreateBook(service);
                    break;
                case "3":
                    DeleteBook(service);
                    break;
                case "4":
                    UpdateBook(service);
                    break;
                case "5":
                    FindByAuthor(service);
                    break;
                case "6":
                    GroupByGenre(service);
                    break;
                case "7":
                    FindByTitle(service);
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n--- Меню управления книгами ---");
        Console.WriteLine("1. Показать все книги");
        Console.WriteLine("2. Добавить книгу");
        Console.WriteLine("3. Удалить книгу");
        Console.WriteLine("4. Обновить книгу");
        Console.WriteLine("5. Найти книги по автору");
        Console.WriteLine("6. Сгруппировать книги по жанрам");
        Console.WriteLine("7. Найти книги по названию");
        Console.WriteLine("8. Выход");
        Console.Write("Выбор: ");
    }

    private static void ShowBooks(IEnumerable<Book> books)
    {
        var list = books.ToList();
        if (!list.Any())
        {
            Console.WriteLine("Книги не найдены.");
            return;
        }

        foreach (var book in list)
        {
            Console.WriteLine(book);
        }
    }

    private static void CreateBook(BookService service)
    {
        var title = ReadRequiredString("Название: ");
        var author = ReadRequiredString("Автор: ");
        var year = ReadInt("Год выхода: ");
        var genre = ReadRequiredString("Жанр: ");

        var book = service.CreateBook(title, author, year, genre);
        Console.WriteLine($"Книга добавлена с ID {book.ID}.");
    }

    private static void DeleteBook(BookService service)
    {
        var id = ReadInt("ID книги для удаления: ");
        if (service.DeleteBook(id))
        {
            Console.WriteLine("Книга удалена.");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    private static void UpdateBook(BookService service)
    {
        var id = ReadInt("ID книги для обновления: ");
        var title = ReadRequiredString("Новое название: ");
        var author = ReadRequiredString("Новый автор: ");
        var year = ReadInt("Новый год: ");
        var genre = ReadRequiredString("Новый жанр: ");

        if (service.UpdateBook(id, title, author, year, genre))
        {
            Console.WriteLine("Книга обновлена.");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    private static void FindByAuthor(BookService service)
    {
        var author = ReadRequiredString("Автор: ");
        var books = service.FindBooksByAuthor(author);
        ShowBooks(books);
    }

    private static void FindByTitle(BookService service)
    {
        var title = ReadRequiredString("Часть названия: ");
        var books = service.FindBooksByTitle(title);
        ShowBooks(books);
    }

    private static void GroupByGenre(BookService service)
    {
        var groups = service.GroupBooksByGenre();
        if (!groups.Any())
        {
            Console.WriteLine("Книги отсутствуют.");
            return;
        }

        foreach (var (genre, books) in groups)
        {
            Console.WriteLine($"\nЖанр: {genre}");
            foreach (var book in books)
            {
                Console.WriteLine($"  {book}");
            }
        }
    }

    private static string ReadRequiredString(string prompt)
    {
        Console.Write(prompt);
        while (true)
        {
            var value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.Write("Поле обязательно. Повторите ввод: ");
        }
    }

    private static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var number))
            {
                return number;
            }

            Console.Write("Введите корректное число: ");
        }
    }
}
