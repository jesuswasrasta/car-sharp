// ABOUTME: Il ParcoMezzi è modellato come un record immutabile.
// Invece di modificare il parco mezzi, le operazioni restituiscono una nuova istanza 
// che rappresenta lo stato aggiornato.

using System.Collections.Immutable;

using System.Linq;

namespace CarSharp.Functional;

/// <summary>
/// Rappresenta un parco mezzi di auto come una struttura dati immutabile.
/// In FP, il ParcoMezzi è un 'Dato' piuttosto che un 'Oggetto' con stato interno.
/// </summary>
/// <param name="auto">La collezione immutabile di auto.</param>
public record ParcoMezzi(ImmutableList<IAuto> auto)
{
    /// <summary>
    /// Restituisce un'istanza di parco mezzi vuota.
    /// Questo è il punto di partenza per tutte le trasformazioni.
    /// </summary>
    public static ParcoMezzi Vuoto { get; } = new(ImmutableList<IAuto>.Empty);

    /// <summary>
    /// Ottiene il numero totale di auto nel parco mezzi.
    /// </summary>
    public int TotaleAuto => auto.Count;

    /// <summary>
    /// Ottiene il numero di auto attualmente disponibili per il noleggio.
    /// In Type-Driven Design, lo stato è codificato nel tipo. Filtrare per il tipo 
    /// 'AutoDisponibile' è il modo idiomatico per estrarre la sottoparte del 
    /// parco mezzi che risponde ai criteri operativi richiesti.
    /// </summary>
    public int ConteggioDisponibili => auto.OfType<AutoDisponibile>().Count();
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
    public static ParcoMezzi AggiungiAuto(this ParcoMezzi parco, IAuto auto)
    {
        return parco with { auto = parco.auto.Add(auto) };
    }

    /// <summary>
    /// 'Rimuovi' un'auto dal parco mezzi producendo un nuovo valore.
    /// L'uguaglianza dei record si basa sul valore delle proprietà, rendendo l'identità 
    /// indipendente dal riferimento in memoria.
    /// </summary>
    public static ParcoMezzi RimuoviAuto(this ParcoMezzi parco, IAuto auto)
    {
        return parco with { auto = parco.auto.Remove(auto) };
    }

    /// <summary>
    /// Tenta di noleggiare un'auto specifica dal parco.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaAuto(this ParcoMezzi parco, Guid id, string clienteId)
    {
        var autoTrovata = parco.auto.FirstOrDefault(a => a.Id == id);

        if (autoTrovata == null)
            return Result<ParcoMezzi>.Fail(new Error("Auto non trovata o non disponibile"));

        return autoTrovata switch
        {
            AutoDisponibile disponibile => Result<ParcoMezzi>.From(
                parco with { auto = parco.auto.Replace(disponibile, disponibile.Noleggia()) }),
            _ => Result<ParcoMezzi>.Fail(new Error("Auto già noleggiata o in stato non valido"))
        };
    }

    /// <summary>
    /// Noleggia un batch di auto identificandole tramite ID.
    /// In FP, utilizziamo la composizione di funzioni (Bind) su una sequenza di richieste.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaBatch(this ParcoMezzi parco, IEnumerable<Guid> ids, string clienteId)
    {
        // Parse, Don't Validate: Validiamo la struttura dell'input prima di processarlo.
        // Se il batch contiene duplicati, è strutturalmente invalido per la nostra logica di dominio atomica.
        var listaIds = ids.ToList();
        if (listaIds.Distinct().Count() != listaIds.Count)
            return Result<ParcoMezzi>.Fail(new Error("Il batch contiene duplicati"));

        // Nota per l'audience: L'uso di Aggregate con Bind garantisce l'atomicità:
        // Se una sola richiesta fallisce, l'intera catena si interrompe e restituisce l'errore,
        // lasciando il parco originale intatto (poiché è immutabile).
        return listaIds.Aggregate(
            Result<ParcoMezzi>.From(parco),
            (risultatoCorrente, id) => risultatoCorrente.Bind(p => p.NoleggiaAuto(id, clienteId))
        );
    }
}
