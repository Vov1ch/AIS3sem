using System.Collections.Generic;
using System.Linq;
using BookManagementSystem.DataAccessLayer.Contexts;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.DataAccessLayer.Repositories;

public class EfBookRepository : IBookRepository
{
    private readonly BookDbContext _context;

    public EfBookRepository(BookDbContext context)
    {
        _context = context;
    }

    public Book Add(Book entity)
    {
        _context.Books.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public bool Delete(int id)
    {
        var book = _context.Books.Find(id);
        if (book is null)
        {
            return false;
        }

        _context.Books.Remove(book);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<Book> FindByAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
        {
            return Enumerable.Empty<Book>();
        }

        var normalized = author.Trim();
        return _context.Books
            .AsNoTracking()
            .Where(b => b.Author.ToLower() == normalized.ToLower())
            .OrderBy(b => b.Title)
            .ToList();
    }

    public IEnumerable<Book> FindByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Enumerable.Empty<Book>();
        }

        var pattern = $"%{title.Trim()}%";
        return _context.Books
            .AsNoTracking()
            .Where(b => EF.Functions.Like(b.Title, pattern))
            .OrderBy(b => b.Title)
            .ToList();
    }

    public IEnumerable<Book> GetAll()
    {
        return _context.Books
            .AsNoTracking()
            .OrderBy(b => b.ID)
            .ToList();
    }

    public Book? GetById(int id)
    {
        return _context.Books
            .AsNoTracking()
            .FirstOrDefault(b => b.ID == id);
    }

    public bool Update(Book entity)
    {
        var existing = _context.Books.Find(entity.ID);
        if (existing is null)
        {
            return false;
        }

        existing.Title = entity.Title;
        existing.Author = entity.Author;
        existing.Year = entity.Year;
        existing.Genre = entity.Genre;

        _context.SaveChanges();
        return true;
    }
}
