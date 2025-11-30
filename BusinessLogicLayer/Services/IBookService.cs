using System.Collections.Generic;
using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.BusinessLogicLayer.Services;

/// <summary>
/// Контракт сервиса работы с книгами, отделяющий презентер от конкретной реализации бизнес-логики.
/// </summary>
public interface IBookService
{
    IReadOnlyCollection<Book> GetAllBooks();
    Book CreateBook(string title, string author, int year, IEnumerable<string> genres);
    bool DeleteBook(int id);
    Book? GetBookById(int id);
    bool UpdateBook(int id, string title, string author, int year, IEnumerable<string> genres);
    IReadOnlyCollection<Book> FindBooksByAuthor(string author);
    IReadOnlyCollection<string> GetAuthorSuggestions(string authorFragment);
    IReadOnlyCollection<Book> FindBooksByTitle(string title);
    IReadOnlyCollection<string> GetTitleSuggestions(string titleFragment);
    IReadOnlyCollection<string> GetAllGenres();
    IReadOnlyDictionary<string, IReadOnlyCollection<Book>> GroupBooksByGenre();
}
