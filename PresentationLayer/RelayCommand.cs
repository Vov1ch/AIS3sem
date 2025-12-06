using System;
using System.Windows.Input;

namespace BookManagementSystem.PresentationLayer;

/// <summary>
/// Простая реализация команды для WPF биндингов.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Проверяет, доступно ли выполнение команды.
    /// </summary>
    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    /// <summary>
    /// Выполняет действие команды.
    /// </summary>
    public void Execute(object? parameter) => _execute();

    /// <summary>
    /// Принудительно оповещает об изменении доступности выполнения.
    /// </summary>
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
