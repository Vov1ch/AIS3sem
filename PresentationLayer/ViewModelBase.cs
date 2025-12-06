using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookManagementSystem.PresentationLayer;

/// <summary>
/// Базовый класс для ViewModel с поддержкой INotifyPropertyChanged.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    private readonly Dictionary<string, object?> _values = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Устанавливает поле и уведомляет подписчиков, если значение изменилось.
    /// </summary>
    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    /// Получает значение по имени вызывающего свойства или возвращает значение по умолчанию.
    /// </summary>
    protected T Get<T>(T defaultValue = default!)
    {
        return _values.TryGetValue(GetCallerName(), out var value) && value is T typed
            ? typed
            : defaultValue;
    }

    /// <summary>
    /// Устанавливает значение по имени вызывающего свойства и поднимает событие изменений.
    /// </summary>
    protected void Set<T>(T value, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
        {
            return;
        }

        if (_values.TryGetValue(propertyName, out var existing) && Equals(existing, value))
        {
            return;
        }

        _values[propertyName] = value;
        OnPropertyChanged(propertyName);
    }

    private static string GetCallerName([CallerMemberName] string caller = "")
    {
        return caller;
    }

    /// <summary>
    /// Вызывает PropertyChanged для указанного свойства.
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
