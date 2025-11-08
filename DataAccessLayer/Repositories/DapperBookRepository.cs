using System.Collections.Generic;
using System.Linq;
using BookManagementSystem.DataAccessLayer.Seeding;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Domain.Repositories;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BookManagementSystem.DataAccessLayer.Repositories;

public class DapperBookRepository : IBookRepository
{
    private readonly string _connectionString;

    public DapperBookRepository(string connectionString)
    {
        _connectionString = connectionString;
        DbInitializer.EnsureCreated(connectionString);
    }

    public Book Add(Book entity)
    {
        const string sql = """
            INSERT INTO Books (Title, Author, Year, Genre)
            VALUES (@Title, @Author, @Year, @Genre);
            SELECT last_insert_rowid();
            """;

        using var connection = CreateConnection();
        var id = connection.ExecuteScalar<long>(sql, entity);
        entity.ID = (int)id;
        return entity;
    }

    public bool Delete(int id)
    {
        const string sql = "DELETE FROM Books WHERE ID = @ID";
        using var connection = CreateConnection();
        var affected = connection.Execute(sql, new { ID = id });
        return affected > 0;
    }

    public IEnumerable<Book> FindByAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
        {
            return Enumerable.Empty<Book>();
        }

        const string sql = """
            SELECT ID, Title, Author, Year, Genre
            FROM Books
            WHERE lower(Author) = lower(@Author)
            ORDER BY Title
            """;

        using var connection = CreateConnection();
        return connection.Query<Book>(sql, new { Author = author.Trim() }).ToList();
    }

    public IEnumerable<Book> FindByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Enumerable.Empty<Book>();
        }

        const string sql = """
            SELECT ID, Title, Author, Year, Genre
            FROM Books
            WHERE Title LIKE @Pattern
            ORDER BY Title
            """;

        using var connection = CreateConnection();
        return connection.Query<Book>(sql, new { Pattern = $"%{title.Trim()}%" }).ToList();
    }

    public IEnumerable<Book> GetAll()
    {
        const string sql = "SELECT ID, Title, Author, Year, Genre FROM Books ORDER BY ID";
        using var connection = CreateConnection();
        return connection.Query<Book>(sql).ToList();
    }

    public Book? GetById(int id)
    {
        const string sql = "SELECT ID, Title, Author, Year, Genre FROM Books WHERE ID = @ID";
        using var connection = CreateConnection();
        return connection.QuerySingleOrDefault<Book>(sql, new { ID = id });
    }

    public bool Update(Book entity)
    {
        const string sql = """
            UPDATE Books
            SET Title = @Title,
                Author = @Author,
                Year = @Year,
                Genre = @Genre
            WHERE ID = @ID
            """;

        using var connection = CreateConnection();
        var affected = connection.Execute(sql, entity);
        return affected > 0;
    }

    private SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
