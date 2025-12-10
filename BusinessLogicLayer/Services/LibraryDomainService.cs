using System.Linq;
using BookManagementSystem.Domain.Abstractions;
using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.BusinessLogicLayer.Services;

/// <summary>
/// Реализация доменного сервиса для операций с библиотекой.
/// Координирует бизнес-правила между сущностями Book и Borrower.
/// </summary>
public class LibraryDomainService : ILibraryDomainService
{
    /// <summary>
    /// Выдаёт книгу читателю с проверкой всех бизнес-правил.
    /// </summary>
    /// <param name="borrower">Читатель.</param>
    /// <param name="book">Книга для выдачи.</param>
    /// <returns>Результат операции.</returns>
    public DomainResult BorrowBook(Borrower borrower, Book book)
    {
        // Сначала проверяем все бизнес-правила
        var canBorrowResult = CanBorrow(borrower, book);
        if (!canBorrowResult.IsSuccess)
        {
            return canBorrowResult;
        }

        // Выполняем операцию выдачи
        var borrowResult = book.Borrow(borrower.ID);
        if (!borrowResult.IsSuccess)
        {
            return borrowResult;
        }

        // Добавляем запись о выдаче к читателю
        borrower.Loans.Add(new Loan(book.ID, borrower.ID));

        return DomainResult.Success();
    }

    /// <summary>
    /// Возвращает книгу от читателя.
    /// </summary>
    /// <param name="borrower">Читатель.</param>
    /// <param name="book">Книга для возврата.</param>
    /// <returns>Результат операции.</returns>
    public DomainResult ReturnBook(Borrower borrower, Book book)
    {
        // Проверяем, что книга была выдана именно этому читателю
        if (book.CurrentBorrowerId != borrower.ID)
        {
            return DomainResult.Fail("Эта книга не была выдана данному читателю");
        }

        // Выполняем возврат
        var returnResult = book.Return();
        if (!returnResult.IsSuccess)
        {
            return returnResult;
        }

        // Отмечаем выдачу как возвращённую
        var loan = borrower.Loans
            .FirstOrDefault(l => l.BookId == book.ID && !l.IsReturned);
        loan?.MarkReturned();

        return DomainResult.Success();
    }

    /// <summary>
    /// Проверяет, может ли читатель взять данную книгу.
    /// Объединяет все бизнес-правила в одну проверку.
    /// </summary>
    /// <param name="borrower">Читатель.</param>
    /// <param name="book">Книга.</param>
    /// <returns>Результат проверки.</returns>
    public DomainResult CanBorrow(Borrower borrower, Book book)
    {
        // Бизнес-правило 1: Книга не должна быть уже выдана
        if (!book.CanBeBorrowed())
        {
            return DomainResult.Fail("Книга уже выдана другому читателю");
        }

        // Бизнес-правило 2: Читатель не должен превысить лимит книг
        if (!borrower.CanBorrowMoreBooks())
        {
            return DomainResult.Fail(
                $"Читатель уже имеет максимум {Borrower.MaxBooksAllowed} книг на руках");
        }

        return DomainResult.Success();
    }
}
