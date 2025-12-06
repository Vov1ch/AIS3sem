using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.Domain.Entities;
using BookManagementSystem.PresentationLayer.Dto;

namespace BookManagementSystem.PresentationLayer;

/// <summary>
/// ViewModel для основного окна книг. Работает с DTO вместо доменных сущностей.
/// </summary>
public class BookViewModel : ViewModelBase
{
    private readonly IBookService _service;
    private readonly List<BookDto> _allBooks = new();
    private readonly IViewManager _viewManager;

    private string _title = string.Empty;
    private string _author = string.Empty;
    private string _year = string.Empty;
    private string _genresText = string.Empty;
    private string _authorFilter = string.Empty;
    private string _titleFilter = string.Empty;
    private string _genreFilter = string.Empty;
    private string _statusMessage = string.Empty;

    private BookDto? _selectedBook;

    public BookViewModel(IBookService service, IViewManager viewManager)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));

        AddBookCommand = new RelayCommand(AddBook, CanAddOrUpdate);
        UpdateBookCommand = new RelayCommand(UpdateBook, () => SelectedBook is not null && CanAddOrUpdate());
        DeleteBookCommand = new RelayCommand(DeleteBook, () => SelectedBook is not null);
        RefreshCommand = new RelayCommand(Refresh);
    }

    public BindingList<BookDto> Books { get; } = new();

    public BookDto? SelectedBook
    {
        get => _selectedBook;
        set
        {
            if (!SetProperty(ref _selectedBook, value))
            {
                return;
            }

            if (value is null)
            {
                Title = string.Empty;
                Author = string.Empty;
                Year = string.Empty;
                GenresText = string.Empty;
            }
            else
            {
                Title = value.Title;
                Author = value.Author;
                Year = value.Year.ToString();
                GenresText = string.Join(", ", value.Genres);
            }

            UpdateCommandStates();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            if (SetProperty(ref _title, value))
            {
                UpdateCommandStates();
            }
        }
    }

    public string Author
    {
        get => _author;
        set
        {
            if (SetProperty(ref _author, value))
            {
                UpdateCommandStates();
            }
        }
    }

    public string Year
    {
        get => _year;
        set
        {
            if (SetProperty(ref _year, value))
            {
                UpdateCommandStates();
            }
        }
    }

    /// <summary>
    /// Genres input represented as comma separated list.
    /// </summary>
    public string GenresText
    {
        get => _genresText;
        set
        {
            if (SetProperty(ref _genresText, value))
            {
                UpdateCommandStates();
            }
        }
    }

    public string AuthorFilter
    {
        get => _authorFilter;
        set
        {
            if (SetProperty(ref _authorFilter, value))
            {
                ApplyFilters();
            }
        }
    }

    public string TitleFilter
    {
        get => _titleFilter;
        set
        {
            if (SetProperty(ref _titleFilter, value))
            {
                ApplyFilters();
            }
        }
    }

    public string GenreFilter
    {
        get => _genreFilter;
        set
        {
            if (SetProperty(ref _genreFilter, value))
            {
                ApplyFilters();
            }
        }
    }

    public List<string> AvailableGenres { get; private set; } = new();

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public RelayCommand AddBookCommand { get; }

    public RelayCommand UpdateBookCommand { get; }

    public RelayCommand DeleteBookCommand { get; }

    public RelayCommand RefreshCommand { get; }

    /// <summary>
    /// Первичная инициализация данных.
    /// </summary>
    public void Initialize()
    {
        Refresh();
    }

    /// <summary>
    /// Перечитывает данные из сервиса и обновляет коллекции.
    /// </summary>
    private void Refresh()
    {
        _allBooks.Clear();
        var books = _service.GetAllBooks().OrderBy(b => b.ID);
        foreach (var book in books)
        {
            _allBooks.Add(ToDto(book));
        }

        AvailableGenres = _service.GetAllGenres().ToList();
        OnPropertyChanged(nameof(AvailableGenres));

        ApplyFilters();
        StatusMessage = "Данные обновлены";
    }

    /// <summary>
    /// Применяет фильтры и обновляет BindingList для UI.
    /// </summary>
    private void ApplyFilters()
    {
        IEnumerable<BookDto> filtered = _allBooks;

        if (!string.IsNullOrWhiteSpace(AuthorFilter))
        {
            filtered = filtered.Where(b =>
                !string.IsNullOrWhiteSpace(b.Author) &&
                b.Author.Contains(AuthorFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(TitleFilter))
        {
            filtered = filtered.Where(b =>
                !string.IsNullOrWhiteSpace(b.Title) &&
                b.Title.Contains(TitleFilter, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(GenreFilter))
        {
            filtered = filtered.Where(b => b.Genres.Any(g =>
                g.Contains(GenreFilter, StringComparison.CurrentCultureIgnoreCase)));
        }

        Books.Clear();
        foreach (var dto in filtered)
        {
            Books.Add(dto);
        }
    }

    /// <summary>
    /// Создаёт новую книгу через сервис.
    /// </summary>
    private void AddBook()
    {
        if (!TryBuildInput(out var title, out var author, out var year, out var genres))
        {
            return;
        }

        var created = _service.CreateBook(title, author, year, genres);
        StatusMessage = $"Книга создана с ID {created.ID}";
        ClearInput();
        Refresh();
    }

    /// <summary>
    /// Обновляет выбранную книгу через сервис.
    /// </summary>
    private void UpdateBook()
    {
        if (SelectedBook is null)
        {
            return;
        }

        if (!TryBuildInput(out var title, out var author, out var year, out var genres))
        {
            return;
        }

        var result = _service.UpdateBook(SelectedBook.Id, title, author, year, genres);
        StatusMessage = result ? "Книга обновлена" : "Не удалось обновить книгу";
        Refresh();
    }

    /// <summary>
    /// Удаляет выбранную книгу.
    /// </summary>
    private void DeleteBook()
    {
        if (SelectedBook is null)
        {
            return;
        }

        if (_service.DeleteBook(SelectedBook.Id))
        {
            StatusMessage = "Книга удалена";
            ClearInput();
            Refresh();
        }
        else
        {
            StatusMessage = "Не удалось удалить книгу";
        }
    }

    /// <summary>
    /// Валидирует ввод и формирует данные для сервисного вызова.
    /// </summary>
    private bool TryBuildInput(out string title, out string author, out int year, out IReadOnlyCollection<string> genres)
    {
        title = Title.Trim();
        author = Author.Trim();
        genres = ParseGenres();

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || !genres.Any())
        {
            StatusMessage = "Заполните название, автора и жанры.";
            year = 0;
            return false;
        }

        if (!int.TryParse(Year, out year))
        {
            StatusMessage = "Год должен быть числом.";
            return false;
        }

        return true;
    }

    /// <summary>
    /// Разбирает строку жанров в коллекцию.
    /// </summary>
    private IReadOnlyCollection<string> ParseGenres()
    {
        return GenresText
            .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(g => g.Trim())
            .Where(g => !string.IsNullOrWhiteSpace(g))
            .ToList();
    }

    /// <summary>
    /// Сбрасывает поля ввода и выбор.
    /// </summary>
    private void ClearInput()
    {
        Title = string.Empty;
        Author = string.Empty;
        Year = string.Empty;
        GenresText = string.Empty;
        SelectedBook = null;
    }

    /// <summary>
    /// Проверяет, заполнены ли обязательные поля для добавления/обновления.
    /// </summary>
    private bool CanAddOrUpdate()
    {
        return !string.IsNullOrWhiteSpace(Title) &&
               !string.IsNullOrWhiteSpace(Author) &&
               !string.IsNullOrWhiteSpace(GenresText);
    }

    /// <summary>
    /// Обновляет состояние команд.
    /// </summary>
    private void UpdateCommandStates()
    {
        AddBookCommand.RaiseCanExecuteChanged();
        UpdateBookCommand.RaiseCanExecuteChanged();
        DeleteBookCommand.RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Преобразует доменную модель в DTO.
    /// </summary>
    private static BookDto ToDto(Book book)
    {
        return new BookDto
        {
            Id = book.ID,
            Title = book.Title,
            Author = book.Author,
            Year = book.Year,
            Genres = book.Genres
                .Select(g => g.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToList()
        };
    }
}
