using BookManagementSystem.Domain.Abstractions;

namespace BookManagementSystem.Domain.Entities;

public class Book : IDomainObject
{
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Genre { get; set; } = string.Empty;

    public Book()
    {
    }

    public Book(string title, string author, int year, string genre)
    {
        Title = title;
        Author = author;
        Year = year;
        Genre = genre;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Название: {Title}, Автор: {Author}, Год: {Year}, Жанр: {Genre}";
    }
}
