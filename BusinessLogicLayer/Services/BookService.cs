using System.Collections.Generic;
using System.Linq;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Domain.Repositories;

namespace BookManagementSystem.BusinessLogicLayer.Services;

public class BookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyCollection<Book> GetAllBooks()
    {
        return _repository
            .GetAll()
            .OrderBy(b => b.ID)
            .ToList();
    }

    public Book CreateBook(string title, string author, int year, string genre)
    {
        var book = new Book(title, author, year, genre);
        return _repository.Add(book);
    }

    public bool DeleteBook(int id)
    {
        return _repository.Delete(id);
    }

    public Book? GetBookById(int id)
    {
        return _repository.GetById(id);
    }

    public bool UpdateBook(int id, string title, string author, int year, string genre)
    {
        var book = new Book(title, author, year, genre) { ID = id };
        return _repository.Update(book);
    }

    public IReadOnlyCollection<Book> FindBooksByAuthor(string author)
    {
        return _repository
            .FindByAuthor(author)
            .ToList();
    }

    public IReadOnlyCollection<Book> FindBooksByTitle(string title)
    {
        return _repository
            .FindByTitle(title)
            .ToList();
    }

    public IReadOnlyDictionary<string, IReadOnlyCollection<Book>> GroupBooksByGenre()
    {
        return _repository
            .GetAll()
            .GroupBy(b => string.IsNullOrWhiteSpace(b.Genre) ? "Не указан" : b.Genre)
            .ToDictionary(g => g.Key, g => (IReadOnlyCollection<Book>)g.ToList());
    }
}
