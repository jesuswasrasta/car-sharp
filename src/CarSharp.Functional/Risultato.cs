// ABOUTME: Questo file implementa il pattern Result (Risultato) per il Railway-Oriented Programming.
// In FP, evitiamo le eccezioni per il controllo di flusso, preferendo tipi che rendono
// esplicito il possibile fallimento di un'operazione.

namespace CarSharp.Functional;

/// <summary>
/// Rappresenta un errore nel dominio.
/// </summary>
public record Errore(string Messaggio);

/// <summary>
/// Rappresenta l'esito di un'operazione che può fallire.
/// </summary>
/// <typeparam name="T">Il tipo del valore restituito in caso di successo.</typeparam>
public record Risultato<T>
{
    public T? Valore { get; }
    public Errore? Errore { get; }
    public bool IsSuccess => Errore == null;

    private Risultato(T valore) => Valore = valore;
    private Risultato(Errore errore) => Errore = errore;

    public static Risultato<T> Successo(T valore) => new(valore);
    public static Risultato<T> Fallimento(Errore errore) => new(errore);

    // Permette di trasformare il valore interno in caso di successo
    public Risultato<TResult> Map<TResult>(Func<T, TResult> mapper) =>
        IsSuccess ? Risultato<TResult>.Successo(mapper(Valore!)) : Risultato<TResult>.Fallimento(Errore!);
}
