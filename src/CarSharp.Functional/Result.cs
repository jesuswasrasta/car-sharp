// ABOUTME: Questo file implementa il pattern Result per il Railway-Oriented Programming.
// Seguendo la Costituzione, utilizziamo nomi tecnici in inglese per i design pattern (Result, Error).

namespace CarSharp.Functional;

/// <summary>
/// Rappresenta un errore tecnico o di dominio.
/// </summary>
public record Error(string Message);

/// <summary>
/// Rappresenta l'esito di un'operazione (Pattern Result).
/// </summary>
/// <typeparam name="T">Il tipo del valore restituito in caso di successo.</typeparam>
public record Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error == null;

    private Result(T value) => Value = value;
    private Result(Error error) => Error = error;

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);

    public Result<TResult> Map<TResult>(Func<T, TResult> mapper) =>
        IsSuccess ? Result<TResult>.Success(mapper(Value!)) : Result<TResult>.Failure(Error!);

    public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> binder) =>
        IsSuccess ? binder(Value!) : Result<TResult>.Failure(Error!);
}
