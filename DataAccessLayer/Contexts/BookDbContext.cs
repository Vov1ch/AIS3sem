using BookManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.DataAccessLayer.Contexts;

public class BookDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var bookEntity = modelBuilder.Entity<Book>();

        bookEntity.ToTable("Books");
        bookEntity.HasKey(b => b.ID);
        bookEntity.Property(b => b.ID)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();
        bookEntity.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(256);
        bookEntity.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(128);
        bookEntity.Property(b => b.Genre)
            .IsRequired()
            .HasMaxLength(128);
        bookEntity.Property(b => b.Year)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
