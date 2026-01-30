// ABOUTME: Implementazione del tipo Result (Railway Oriented Programming).
// Utilizzato per gestire il flusso di operazioni che possono fallire senza eccezioni.

using System;

namespace CarSharp.Functional;

public record Error(string Message);

public abstract record Result<T>
{
    public bool IsSuccess => this is Success;
    public bool IsFailure => this is Failure;

    public T? Value => this is Success s ? s.Content : default;
    public Error? Error => this is Failure f ? f.Content : default;

    public sealed record Success(T Content) : Result<T>;
    public sealed record Failure(Error Content) : Result<T>;

    public static Result<T> From(T value) => new Success(value);
    public static Result<T> Fail(Error error) => new Failure(error);
    public static Result<T> Fail(string message) => new Failure(new Error(message));
    
    // Metodo Bind per chaining (la "ferrovia")
    public Result<U> Bind<U>(Func<T, Result<U>> func)
    {
        return this switch
        {
            Success s => func(s.Content),
            Failure f => new Result<U>.Failure(f.Content),
            _ => throw new NotImplementedException()
        };
    }
}
