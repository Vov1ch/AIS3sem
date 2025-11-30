using System;
using System.Collections.Generic;
using BookManagementSystem.Domain.Entities;

namespace BookManagementSystem.Shared;

public record BookInput(string Title, string Author, int Year, IReadOnlyCollection<string> Genres);

public record BookFilters(string? AuthorFragment, string? TitleFragment, string? Genre);

public class BookSelectionChangedEventArgs : EventArgs
{
    public BookSelectionChangedEventArgs(Book? book)
    {
        Book = book;
    }

    public Book? Book { get; }
}

public interface IBookView
{
    event EventHandler? ViewLoaded;
    event EventHandler? AddBookRequested;
    event EventHandler? UpdateBookRequested;
    event EventHandler? DeleteBookRequested;
    event EventHandler? RefreshRequested;
    event EventHandler? GroupRequested;
    event EventHandler? EditGenresRequested;
    event EventHandler? AuthorFilterChanged;
    event EventHandler? TitleFilterChanged;
    event EventHandler? GenreFilterChanged;
    event EventHandler<BookSelectionChangedEventArgs>? BookSelectionChanged;

    bool TryGetBookInput(out BookInput input);
    bool TryGetSelectedBookId(out int id);
    BookFilters GetFilters();
    void ShowBooks(IReadOnlyCollection<Book> books);
    void ShowBookDetails(Book? book);
    void ShowGenres(IReadOnlyCollection<string> genres);
    void ShowInfo(string message);
    void ShowError(string message);
    void ClearInput();
    IReadOnlyCollection<string> RequestGenresSelection(IReadOnlyCollection<string> availableGenres);
    void SetCurrentGenres(IReadOnlyCollection<string> genres);
    void ShowAuthorSuggestions(IReadOnlyCollection<string> authors);
    void HideAuthorSuggestions();
    void ShowTitleSuggestions(IReadOnlyCollection<string> titles);
    void HideTitleSuggestions();
}
