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
    /// In Type-Driven Design, filtriamo per il tipo specifico che rappresenta lo stato disponibile.
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
    /// In questa fase, l'aggiunta ha sempre successo.
    /// </summary>
    public static Result<ParcoMezzi> AggiungiAuto(this ParcoMezzi parco, IAuto auto) =>
        Result<ParcoMezzi>.Success(parco with { auto = parco.auto.Add(auto) });

    /// <summary>
    /// 'Rimuovi' un'auto dal parco mezzi.
    /// Se l'auto viene trovata, restituisce Success con il nuovo parco.
    /// Se l'auto non viene trovata, restituisce un Failure (approccio ROP).
    /// </summary>
    public static Result<ParcoMezzi> RimuoviAuto(this ParcoMezzi parco, IAuto auto)
    {
        var nuoveAuto = parco.auto.Remove(auto);
        
        // Se la collezione risultante è la stessa istanza dell'originale,
        // significa che nulla è stato rimosso (l'auto non era presente).
        return ReferenceEquals(nuoveAuto, parco.auto)
            ? Result<ParcoMezzi>.Failure(new Error("Auto non trovata nel parco mezzi"))
            : Result<ParcoMezzi>.Success(parco with { auto = nuoveAuto });
    }

    /// <summary>
    /// Tenta di noleggiare un'auto in base a una richiesta specifica.
    /// In FP, validiamo i vincoli (disponibilità, capacità) tramite pattern matching e Result.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaAuto(this ParcoMezzi parco, RichiestaNoleggio richiesta)
    {
        IAuto? autoTrovata;

        if (richiesta.IdAuto.HasValue)
        {
            autoTrovata = parco.auto.FirstOrDefault(a => a.Id == richiesta.IdAuto.Value);
        }
        else
        {
            autoTrovata = parco.auto
                .OfType<AutoDisponibile>()
                .FirstOrDefault(a => a.Capacita >= richiesta.PostiMinimi);
        }
        
        if (autoTrovata == null)
            return Result<ParcoMezzi>.Failure(new Error("Auto non trovata o non disponibile per i requisiti richiesti"));

        return autoTrovata switch
        {
            AutoDisponibile disponibile when disponibile.Capacita >= richiesta.PostiMinimi => 
                Result<ParcoMezzi>.Success(parco with { 
                    auto = parco.auto.Replace(disponibile, disponibile.Noleggia()) 
                }),
            AutoDisponibile => Result<ParcoMezzi>.Failure(new Error($"L'auto scelta non ha capacità sufficiente ({richiesta.PostiMinimi} richiesti)")),
            _ => Result<ParcoMezzi>.Failure(new Error("Auto già noleggiata o in stato non valido"))
        };
    }

    /// <summary>
    /// Tenta di noleggiare un'auto specifica dal parco (compatibilità).
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaAuto(this ParcoMezzi parco, Guid id) =>
        parco.NoleggiaAuto(new RichiestaNoleggio(1, id));

    /// <summary>
    /// Tenta di noleggiare un'auto che soddisfi il requisito di capacità.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaPerCapacita(this ParcoMezzi parco, int postiMinimi) =>
        parco.NoleggiaAuto(new RichiestaNoleggio(postiMinimi));

    /// <summary>
    /// Noleggia un batch di richieste atomicamente.
    /// In FP, utilizziamo la composizione di funzioni (Bind) su una sequenza di richieste.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaBatch(this ParcoMezzi parco, IEnumerable<RichiestaNoleggio> richieste)
    {
        // Nota per l'audience: L'uso di Aggregate con Bind garantisce l'atomicità:
        // Se una sola richiesta fallisce (es. capacità insufficiente o auto occupata),
        // l'intera catena si interrompe e restituisce l'errore, lasciando il parco originale intatto.
        return richieste.Aggregate(
            Result<ParcoMezzi>.Success(parco),
            (risultatoCorrente, richiesta) => risultatoCorrente.Bind(p => p.NoleggiaAuto(richiesta))
        );
    }

    /// <summary>
    /// Noleggia un batch di auto identificandole tramite ID (compatibilità Fase 3).
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaBatch(this ParcoMezzi parco, IEnumerable<Guid> ids)
    {
        // In questa fase, per i semplici ID, controlliamo i duplicati espliciti 
        // per mantenere il comportamento atteso dalla Fase 3 (Parse, don't validate).
        var listaIds = ids.ToList();
        if (listaIds.Distinct().Count() != listaIds.Count)
            return Result<ParcoMezzi>.Failure(new Error("Il batch contiene duplicati"));

        return parco.NoleggiaBatch(listaIds.Select(id => new RichiestaNoleggio(1, id)));
    }
}
