using BookManagementSystem.Domain.Repositories;

namespace BookManagementSystem.Domain.Abstractions;

/// <summary>
/// Представляет единицу работы (Unit of Work), объединяющую операции с несколькими репозиториями
/// в рамках одного согласованного набора изменений.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Репозиторий для работы с книгами.
    /// </summary>
    IBookRepository Books { get; }

    /// <summary>
    /// Сохраняет все накопленные изменения в хранилище данных.
    /// </summary>
    void Commit();
}

