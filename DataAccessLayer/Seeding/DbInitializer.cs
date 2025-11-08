using System.Linq;
using BookManagementSystem.DataAccessLayer.Contexts;
using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.DataAccessLayer.Seeding;

public static class DbInitializer
{
    public static void EnsureCreated(BookDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Books.Any())
        {
            return;
        }

        var seedBooks = new[]
        {
            new Book("1984", "Джордж Оруэлл", 1949, "Антиутопия"),
            new Book("Преступление и наказание", "Фёдор Достоевский", 1866, "Роман"),
            new Book("Властелин колец", "Джон Толкин", 1954, "Фэнтези"),
            new Book("Мастер и Маргарита", "Михаил Булгаков", 1966, "Роман"),
            new Book("Три товарища", "Эрих Мария Ремарк", 1936, "Драма")
        };

        context.Books.AddRange(seedBooks);
        context.SaveChanges();
    }

    public static void EnsureCreated(string connectionString)
    {
        using var context = BookDbContextFactory.Create(connectionString, ensureDatabase: false);
        EnsureCreated(context);
    }
}
