using System;
using BookManagementSystem.Domain.Abstractions;

namespace BookManagementSystem.Domain.Entities;

/// <summary>
/// Сущность выдачи книги читателю.
/// Представляет запись о том, кто и когда взял книгу.
/// </summary>
public class Loan : IDomainObject
{
    /// <summary>
    /// Уникальный идентификатор записи о выдаче.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Идентификатор выданной книги.
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Идентификатор читателя, взявшего книгу.
    /// </summary>
    public int BorrowerId { get; set; }

    /// <summary>
    /// Дата выдачи книги.
    /// </summary>
    public DateTime BorrowDate { get; set; }

    /// <summary>
    /// Дата возврата книги. Null, если книга ещё не возвращена.
    /// </summary>
    public DateTime? ReturnDate { get; set; }

    /// <summary>
    /// Признак того, что книга возвращена.
    /// </summary>
    public bool IsReturned => ReturnDate.HasValue;

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public Loan()
    {
    }

    /// <summary>
    /// Создаёт новую запись о выдаче книги.
    /// </summary>
    /// <param name="bookId">Идентификатор книги.</param>
    /// <param name="borrowerId">Идентификатор читателя.</param>
    public Loan(int bookId, int borrowerId)
    {
        BookId = bookId;
        BorrowerId = borrowerId;
        BorrowDate = DateTime.Now;
    }

    /// <summary>
    /// Отмечает книгу как возвращённую.
    /// </summary>
    public void MarkReturned()
    {
        ReturnDate = DateTime.Now;
    }
}
