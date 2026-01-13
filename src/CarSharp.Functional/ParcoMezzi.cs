// ABOUTME: Il ParcoMezzi è modellato come un record immutabile.
// Invece di modificare il parco mezzi, le operazioni restituiscono una nuova istanza 
// che rappresenta lo stato aggiornato.

using System.Collections.Immutable;

namespace CarSharp.Functional;

/// <summary>
/// Rappresenta un parco mezzi di auto come una struttura dati immutabile.
/// In FP, il ParcoMezzi è un 'Dato' piuttosto che un 'Oggetto' con stato interno.
/// </summary>
/// <param name="Auto">La collezione immutabile di auto.</param>
public record ParcoMezzi(ImmutableList<Auto> Auto)
{
    /// <summary>
    /// Restituisce un'istanza di parco mezzi vuota.
    /// Questo è il punto di partenza per tutte le trasformazioni.
    /// </summary>
    public static ParcoMezzi Vuoto { get; } = new(ImmutableList<Auto>.Empty);

    /// <summary>
    /// Ottiene il numero totale di auto nel parco mezzi.
    /// </summary>
    public int TotaleAuto => Auto.Count;
}

/// <summary>
/// Metodi di estensione per trasformare un ParcoMezzi.
/// In FP, spesso separiamo i dati dalle funzioni che operano su di essi.
/// </summary>
public static class ParcoMezziExtensions
{
    /// <summary>
    /// 'Aggiunge' un'auto al parco mezzi restituendo una nuova istanza di ParcoMezzi.
    /// 
    /// Contrasto con OOP:
    /// In OOP, parco.AggiungiAuto(auto) modifica l'oggetto esistente.
    /// In Funzionale, parco.AggiungiAuto(auto) lascia il parco originale intatto 
    /// e restituisce un NUOVO parco.
    /// </summary>
    public static ParcoMezzi AggiungiAuto(this ParcoMezzi parco, Auto auto) =>
        parco with { Auto = parco.Auto.Add(auto) };

    /// <summary>
    /// 'Rimuove' un'auto dal parco mezzi restituendo una nuova istanza di ParcoMezzi.
    /// Se l'auto non viene trovata, viene restituita l'istanza originale del parco mezzi.
    /// 
    /// Contrasto con OOP:
    /// In OOP, Rimuovi(auto) restituisce un booleano che indica il successo.
    /// In Funzionale, restituiamo sempre un ParcoMezzi, che potrebbe essere la stessa 
    /// istanza se non sono state apportate modifiche (Structural sharing).
    /// </summary>
    public static ParcoMezzi RimuoviAuto(this ParcoMezzi parco, Auto auto)
    {
        var nuoveAuto = parco.Auto.Remove(auto);
        // Se la collezione risultante è la stessa istanza dell'originale,
        // significa che nulla è stato rimosso. Restituiamo il parco originale.
        return ReferenceEquals(nuoveAuto, parco.Auto) ? parco : parco with { Auto = nuoveAuto };
    }
}