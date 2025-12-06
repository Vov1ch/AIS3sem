using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BookManagementSystem.PresentationLayer.Dto;

/// <summary>
/// DTO книги с поддержкой уведомлений об изменениях для биндинга.
/// </summary>
public class BookDto : INotifyPropertyChanged
{
    private int _id;
    private string _title = string.Empty;
    private string _author = string.Empty;
    private int _year;
    private List<string> _genres = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public string Title
    {
        get => _title;
        set => SetField(ref _title, value);
    }

    public string Author
    {
        get => _author;
        set => SetField(ref _author, value);
    }

    public int Year
    {
        get => _year;
        set => SetField(ref _year, value);
    }

    public List<string> Genres
    {
        get => _genres;
        set
        {
            _genres = value ?? new List<string>();
            OnPropertyChanged(nameof(Genres));
            OnPropertyChanged(nameof(GenresDisplay));
        }
    }

    public string GenresDisplay => _genres.Any()
        ? string.Join(", ", _genres.Where(g => !string.IsNullOrWhiteSpace(g)))
        : "Нет жанров";

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T storage, T value, string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName ?? string.Empty);
        if (propertyName == nameof(Genres))
        {
            OnPropertyChanged(nameof(GenresDisplay));
        }
    }

    /// <summary>
    /// Возвращает копию текущего DTO.
    /// </summary>
    public BookDto Clone()
    {
        return new BookDto
        {
            Id = Id,
            Title = Title,
            Author = Author,
            Year = Year,
            Genres = new List<string>(Genres)
        };
    }
}
