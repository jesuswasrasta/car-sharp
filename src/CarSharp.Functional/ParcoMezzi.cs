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
    /// </summary>
    public static ParcoMezzi AggiungiAuto(this ParcoMezzi parco, Auto auto)
    {
        return parco with { auto = parco.auto.Add(auto) };
    }

    /// <summary>
    /// 'Rimuovi' un'auto dal parco mezzi producendo un nuovo valore.
    /// L'uguaglianza dei record si basa sul valore delle proprietà, rendendo l'identità 
    /// indipendente dal riferimento in memoria.
    /// </summary>
    public static ParcoMezzi RimuoviAuto(this ParcoMezzi parco, Auto auto)
    {
        return parco with { auto = parco.auto.Remove(auto) };
    }
}
