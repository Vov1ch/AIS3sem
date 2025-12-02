using System;
using System.Collections.Generic;
using System.Linq;
using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.Shared;

namespace BookManagementSystem.PresentationLayer
{
    /// <summary>
    /// Контроллер управляет потоком данных между View и Service.
    /// </summary>
    public class BookController
    {
        private readonly IBookService _service;
        private readonly IBookView _view;
        private List<Book> _allBooks = new();

        public BookController(IBookService service, IBookView view)
        {
            _service = service;
            _view = view;
        }

        public void Initialize()
        {
            RefreshData();
        }

        public void OnRefresh()
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

        public void OnAdd()
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

        public void OnUpdate()
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

        public void OnDelete()
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

        public void OnGroup()
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

        public void OnEditGenres()
        {
            var available = _service.GetAllGenres();
            var selected = _view.RequestGenresSelection(available);
            _view.SetCurrentGenres(selected);
        }

        public void OnFiltersChanged()
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

        public void OnBookSelected(Book? book)
        {
            _view.ShowBookDetails(book);
            if (book is not null)
            {
                var genres = book.Genres
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
}
