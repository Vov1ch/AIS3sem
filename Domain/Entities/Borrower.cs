using System.Collections.Generic;
using System.Linq;
using BookManagementSystem.Domain.Abstractions;

namespace BookManagementSystem.Domain.Entities;

/// <summary>
/// Сущность читателя библиотеки.
/// Содержит информацию о читателе и его активных выдачах.
/// </summary>
public class Borrower : IDomainObject
{
    /// <summary>
    /// Максимальное количество книг, которое может взять один читатель.
    /// </summary>
    public const int MaxBooksAllowed = 3;

    /// <summary>
    /// Уникальный идентификатор читателя.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Имя читателя.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта читателя.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Список всех выдач читателя.
    /// </summary>
    public List<Loan> Loans { get; set; } = new();

    /// <summary>
    /// Активные (не возвращённые) выдачи читателя.
    /// </summary>
    public IEnumerable<Loan> ActiveLoans => Loans.Where(l => !l.IsReturned);

    /// <summary>
    /// Количество книг, которые сейчас на руках у читателя.
    /// </summary>
    public int CurrentBooksCount => ActiveLoans.Count();

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public Borrower()
    {
    }

    /// <summary>
    /// Создаёт нового читателя.
    /// </summary>
    /// <param name="name">Имя читателя.</param>
    /// <param name="email">Электронная почта.</param>
    public Borrower(string name, string email)
    {
        Name = name;
        Email = email;
    }

    /// <summary>
    /// Проверяет, может ли читатель взять ещё одну книгу.
    /// Бизнес-правило: максимум 3 книги на руках.
    /// </summary>
    /// <returns>True, если лимит не превышен.</returns>
    public bool CanBorrowMoreBooks()
    {
        return CurrentBooksCount < MaxBooksAllowed;
    }

    /// <summary>
    /// Возвращает количество книг, которые читатель ещё может взять.
    /// </summary>
    public int RemainingBooksAllowed => MaxBooksAllowed - CurrentBooksCount;
}
