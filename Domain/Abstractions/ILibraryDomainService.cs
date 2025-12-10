using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.Domain.Abstractions;

/// <summary>
/// Доменный сервис для операций с библиотекой.
/// Инкапсулирует бизнес-правила, которые не принадлежат одной сущности.
/// </summary>
public interface ILibraryDomainService
{
    /// <summary>
    /// Выдаёт книгу читателю.
    /// Бизнес-правила:
    /// - Нельзя выдать книгу, если она уже на руках.
    /// - Нельзя выдать книгу, если читатель достиг лимита (3 книги).
    /// </summary>
    /// <param name="borrower">Читатель.</param>
    /// <param name="book">Книга для выдачи.</param>
    /// <returns>Результат операции.</returns>
    DomainResult BorrowBook(Borrower borrower, Book book);

    /// <summary>
    /// Возвращает книгу от читателя.
    /// Бизнес-правила:
    /// - Нельзя вернуть книгу, которая не была выдана.
    /// - Нельзя вернуть книгу от имени другого читателя.
    /// </summary>
    /// <param name="borrower">Читатель.</param>
    /// <param name="book">Книга для возврата.</param>
    /// <returns>Результат операции.</returns>
    DomainResult ReturnBook(Borrower borrower, Book book);

    /// <summary>
    /// Проверяет, может ли читатель взять данную книгу.
    /// </summary>
    /// <param name="borrower">Читатель.</param>
    /// <param name="book">Книга.</param>
    /// <returns>Результат проверки с возможным сообщением об ошибке.</returns>
    DomainResult CanBorrow(Borrower borrower, Book book);
}
