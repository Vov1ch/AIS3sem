namespace BookManagementSystem.Domain.Abstractions;

/// <summary>
/// Результат выполнения доменной операции.
/// Используется для передачи информации об успехе или неудаче бизнес-операции.
/// </summary>
public class DomainResult
{
    /// <summary>
    /// Признак успешного выполнения операции.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Сообщение об ошибке. Заполняется только при неуспешном результате.
    /// </summary>
    public string? ErrorMessage { get; }

    private DomainResult(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Создаёт успешный результат операции.
    /// </summary>
    public static DomainResult Success() => new(true, null);

    /// <summary>
    /// Создаёт неуспешный результат с указанием причины.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public static DomainResult Fail(string message) => new(false, message);
}
