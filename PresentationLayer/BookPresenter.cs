using System;
using System.Collections.Generic;
using System.Linq;
using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Shared;

namespace BookManagementSystem.PresentationLayer;

/// <summary>
/// Презентер связывает слой представления и бизнес-логику, реагируя на события представления.
/// </summary>
public class BookPresenter
{
    private readonly IBookView _view;
    private readonly IBookService _service;
    private List<Book> _allBooks = new();

    public BookPresenter(IBookView view, IBookService service)
    {
        _view = view;
        _service = service;

        _view.ViewLoaded += OnViewLoaded;
        _view.AddBookRequested += OnAddBookRequested;
        _view.UpdateBookRequested += OnUpdateBookRequested;
        _view.DeleteBookRequested += OnDeleteBookRequested;
        _view.RefreshRequested += OnRefreshRequested;
        _view.GroupRequested += OnGroupRequested;
        _view.EditGenresRequested += OnEditGenresRequested;
        _view.AuthorFilterChanged += OnFiltersChanged;
        _view.TitleFilterChanged += OnFiltersChanged;
        _view.GenreFilterChanged += OnFiltersChanged;
        _view.BookSelectionChanged += OnBookSelectionChanged;
    }

    private void OnViewLoaded(object? sender, EventArgs e)
    {
        RefreshData();
    }

    private void OnRefreshRequested(object? sender, EventArgs e)
    {
        RefreshData();
        _view.ClearInput();
    }

    private void RefreshData()
    {
        _allBooks = _service.GetAllBooks().OrderBy(b => b.ID).ToList();
        _view.ShowGenres(_service.GetAllGenres());
        ApplyFiltersAndSuggestions();
    }

    private void OnAddBookRequested(object? sender, EventArgs e)
    {
        if (!_view.TryGetBookInput(out var input))
        {
            return;
        }

        var created = _service.CreateBook(input.Title, input.Author, input.Year, input.Genres);
        RefreshData();
        _view.ClearInput();
        _view.ShowInfo($"Книга создана с ID {created.ID}.");
    }

    private void OnUpdateBookRequested(object? sender, EventArgs e)
    {
        if (!_view.TryGetBookInput(out var input) || !_view.TryGetSelectedBookId(out var id))
        {
            _view.ShowError("Выберите запись и заполните данные для обновления.");
            return;
        }

        if (_service.UpdateBook(id, input.Title, input.Author, input.Year, input.Genres))
        {
            RefreshData();
            _view.ClearInput();
            _view.ShowInfo("Книга обновлена.");
        }
        else
        {
            _view.ShowError("Не удалось обновить книгу.");
        }
    }

    private void OnDeleteBookRequested(object? sender, EventArgs e)
    {
        if (!_view.TryGetSelectedBookId(out var id))
        {
            _view.ShowError("Сначала выберите книгу для удаления.");
            return;
        }

        if (_service.DeleteBook(id))
        {
            RefreshData();
            _view.ClearInput();
            _view.ShowInfo("Книга удалена.");
        }
        else
        {
            _view.ShowError("Не удалось удалить книгу.");
        }
    }

    private void OnGroupRequested(object? sender, EventArgs e)
    {
        if (!_allBooks.Any())
        {
            _view.ShowError("Список книг пуст.");
            return;
        }

        var grouped = _allBooks
            .OrderBy(GetPrimaryGenre, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(b => b.Title, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        _view.ShowBooks(grouped);
    }

    private void OnEditGenresRequested(object? sender, EventArgs e)
    {
        var available = _service.GetAllGenres();
        var selected = _view.RequestGenresSelection(available);
        _view.SetCurrentGenres(selected);
    }

    private void OnFiltersChanged(object? sender, EventArgs e)
    {
        ApplyFiltersAndSuggestions();
    }

    private void ApplyFiltersAndSuggestions()
    {
        var filters = _view.GetFilters();
        var filtered = ApplyFilters(filters).ToList();
        _view.ShowBooks(filtered);

        var authorSuggestions = _service.GetAuthorSuggestions(filters.AuthorFragment ?? string.Empty);
        _view.ShowAuthorSuggestions(authorSuggestions);

        var titleSuggestions = _service.GetTitleSuggestions(filters.TitleFragment ?? string.Empty);
        _view.ShowTitleSuggestions(titleSuggestions);
    }

    private IEnumerable<Book> ApplyFilters(BookFilters filters)
    {
        IEnumerable<Book> books = _allBooks;

        if (!string.IsNullOrWhiteSpace(filters.AuthorFragment))
        {
            var needle = filters.AuthorFragment.Trim();
            books = books.Where(b =>
                !string.IsNullOrWhiteSpace(b.Author) &&
                b.Author.Contains(needle, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filters.TitleFragment))
        {
            var needle = filters.TitleFragment.Trim();
            books = books.Where(b =>
                !string.IsNullOrWhiteSpace(b.Title) &&
                b.Title.Contains(needle, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filters.Genre))
        {
            books = books.Where(b => b.Genres
                .Any(g => string.Equals(g.Name, filters.Genre, StringComparison.CurrentCultureIgnoreCase)));
        }

        return books;
    }

    private void OnBookSelectionChanged(object? sender, BookSelectionChangedEventArgs e)
    {
        _view.ShowBookDetails(e.Book);
        if (e.Book is not null)
        {
            var genres = e.Book.Genres
                .Select(g => g.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToList();
            _view.SetCurrentGenres(genres);
        }
    }

    private static string GetPrimaryGenre(Book book)
    {
        return book.Genres
            .Select(g => g.Name)
            .FirstOrDefault(name => !string.IsNullOrWhiteSpace(name))
            ?? "Без жанра";
    }
}
