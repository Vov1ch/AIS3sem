using BookManagementSystem.Domain.Abstractions;
using BookManagementSystem.Domain.Repositories;

namespace BookManagementSystem.DataAccessLayer.Contexts;

/// <summary>
/// Реализация Unit of Work для варианта с Dapper.
/// Репозиторий сам управляет транзакциями, поэтому Commit не выполняет дополнительных действий.
/// </summary>
public class DapperUnitOfWork : IUnitOfWork
{
    private readonly IBookRepository _bookRepository;

    public DapperUnitOfWork(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <inheritdoc />
    public IBookRepository Books => _bookRepository;

    /// <inheritdoc />
    public void Commit()
    {
        // Для Dapper-репозитория транзакции управляются самим репозиторием.
        // Этот метод оставлен пустым для совместимости с интерфейсом IUnitOfWork.
    }
}

