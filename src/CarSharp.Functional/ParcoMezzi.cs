// ABOUTME: Il ParcoMezzi è modellato come un record immutabile.
// Invece di modificare il parco mezzi, le operazioni restituiscono una nuova istanza 
// che rappresenta lo stato aggiornato.

using System.Collections.Immutable;

namespace CarSharp.Functional;

/// <summary>
/// Rappresenta un parco mezzi di auto come una struttura dati immutabile.
/// In FP, il ParcoMezzi è un 'Dato' piuttosto che un 'Oggetto' con stato interno.
/// </summary>
/// <param name="auto">La collezione immutabile di auto.</param>
public record ParcoMezzi(ImmutableList<Auto> auto)
{
    /// <summary>
    /// Restituisce un'istanza di parco mezzi vuota.
    /// Questo è il punto di partenza per tutte le trasformazioni.
    /// </summary>
    public static ParcoMezzi Vuoto { get; } = new(ImmutableList<Auto>.Empty);

    /// <summary>
    /// Ottiene il numero totale di auto nel parco mezzi.
    /// </summary>
    public int TotaleAuto => auto.Count;
}

/// <summary>
/// Metodi di estensione per trasformare un ParcoMezzi.
/// In FP, spesso separiamo i dati dalle funzioni che operano su di essi.
/// </summary>
public static class ParcoMezziExtensions
{
    /// <summary>
    /// 'Aggiunge' un'auto al parco mezzi restituendo una nuova istanza di ParcoMezzi.
    /// In questa fase, l'aggiunta ha sempre successo.
    /// </summary>
    public static Result<ParcoMezzi> AggiungiAuto(this ParcoMezzi parco, Auto auto) =>
        Result<ParcoMezzi>.Success(parco with { auto = parco.auto.Add(auto) });

    /// <summary>
    /// 'Rimuovi' un'auto dal parco mezzi.
    /// Se l'auto viene trovata, restituisce Success con il nuovo parco.
    /// Se l'auto non viene trovata, restituisce un Failure (approccio ROP).
    /// </summary>
    public static Result<ParcoMezzi> RimuoviAuto(this ParcoMezzi parco, Auto auto)
    {
        var nuoveAuto = parco.auto.Remove(auto);
        
        // Se la collezione risultante è la stessa istanza dell'originale,
        // significa che nulla è stato rimosso (l'auto non era presente).
        return ReferenceEquals(nuoveAuto, parco.auto)
            ? Result<ParcoMezzi>.Failure(new Error("Auto non trovata nel parco mezzi"))
            : Result<ParcoMezzi>.Success(parco with { auto = nuoveAuto });
    }
}
