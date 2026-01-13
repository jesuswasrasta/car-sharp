// ABOUTME: Estensioni per la gestione delle transizioni di stato delle auto.
// In FP, le transizioni sono funzioni pure che prendono un tipo e restituiscono 
// un nuovo tipo, rappresentando il cambiamento di stato in modo esplicito.

namespace CarSharp.Functional;

public static class AutoExtensions
{
    /// <summary>
    /// Trasforma un'auto disponibile in un'auto noleggiata.
    /// In FP, la transizione è totale e sicura se il tipo di partenza lo consente.
    /// Non serve validazione interna perché un'AutoDisponibile può sempre passare a AutoNoleggiata.
    /// </summary>
    public static AutoNoleggiata Noleggia(this AutoDisponibile auto) =>
        new(auto.Id, auto.Targa);

    /// <summary>
    /// Trasforma un'auto noleggiata in un'auto disponibile.
    /// La transizione inversa è anch'essa una funzione pura totale.
    /// </summary>
    public static AutoDisponibile Restituisci(this AutoNoleggiata auto) =>
        new(auto.Id, auto.Targa);
}
