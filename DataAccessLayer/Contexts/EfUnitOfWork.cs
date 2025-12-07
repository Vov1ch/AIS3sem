using BookManagementSystem.DataAccessLayer.Repositories;
using BookManagementSystem.Domain.Abstractions;
using BookManagementSystem.Domain.Repositories;

namespace BookManagementSystem.DataAccessLayer.Contexts;

/// <summary>
/// Реализация паттерна Unit of Work для работы с Entity Framework.
/// Инкапсулирует общий DbContext и репозитории, обеспечивая единое сохранение изменений.
/// </summary>
public class EfUnitOfWork : IUnitOfWork
{
    private readonly BookDbContext _context;
    private readonly IBookRepository _bookRepository;

    public EfUnitOfWork(BookDbContext context, IBookRepository bookRepository)
    {
        _context = context;
        _bookRepository = bookRepository;
    }

    /// <inheritdoc />
    public IBookRepository Books => _bookRepository;

    /// <inheritdoc />
    public void Commit()
    {
        _context.SaveChanges();
    }
}

