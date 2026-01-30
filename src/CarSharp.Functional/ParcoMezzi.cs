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
/// Rappresenta l'intenzione di noleggiare un mezzo con determinati requisiti.
/// </summary>
public record RichiestaNoleggio(string ClienteId, int PostiMinimi, Guid? IdAuto = null)
{
    /// <summary>
    /// Factory method per creare una richiesta validata.
    /// In FP, preferiamo validare i dati al momento della creazione (Parse, don't validate).
    /// </summary>
    public static Result<RichiestaNoleggio> Crea(string clienteId, int postiMinimi, Guid? idAuto = null)
    {
        if (postiMinimi <= 0)
            return Result<RichiestaNoleggio>.Fail("I posti richiesti devono essere positivi");

        return Result<RichiestaNoleggio>.From(new RichiestaNoleggio(clienteId, postiMinimi, idAuto));
    }
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
    public static Result<ParcoMezzi> AggiungiAuto(this ParcoMezzi parco, IAuto auto)
    {
        return Result<ParcoMezzi>.From(parco with { auto = parco.auto.Add(auto) });
    }

    /// <summary>
    /// 'Rimuovi' un'auto dal parco mezzi producendo un nuovo valore.
    /// L'uguaglianza dei record si basa sul valore delle proprietà, rendendo l'identità 
    /// indipendente dal riferimento in memoria.
    /// </summary>
    public static Result<ParcoMezzi> RimuoviAuto(this ParcoMezzi parco, IAuto auto)
    {
        var nuoveAuto = parco.auto.Remove(auto);
        
        return ReferenceEquals(nuoveAuto, parco.auto)
            ? Result<ParcoMezzi>.Fail(new Error("Auto non trovata nel parco mezzi"))
            : Result<ParcoMezzi>.From(parco with { auto = nuoveAuto });
    }

    /// <summary>
    /// Tenta di noleggiare un'auto specifica dal parco.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaAuto(this ParcoMezzi parco, Guid id, string clienteId)
    {
        return parco.NoleggiaAuto(new RichiestaNoleggio(clienteId, 1, id));
    }

    /// <summary>
    /// Tenta di noleggiare un'auto che soddisfi il requisito di capacità.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaPerCapacita(this ParcoMezzi parco, int postiMinimi, string clienteId)
    {
        return parco.NoleggiaAuto(new RichiestaNoleggio(clienteId, postiMinimi));
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
            // In questa fase, scegliamo la prima auto disponibile che soddisfi il requisito.
            autoTrovata = parco.auto
                .OfType<AutoDisponibile>()
                .FirstOrDefault(a => a.Capacita >= richiesta.PostiMinimi);
        }

        if (autoTrovata == null)
            return Result<ParcoMezzi>.Fail(new Error("Auto non trovata o non disponibile per i requisiti richiesti"));

        return autoTrovata switch
        {
            AutoDisponibile disponibile when disponibile.Capacita >= richiesta.PostiMinimi =>
                Result<ParcoMezzi>.From(parco with {
                    auto = parco.auto.Replace(disponibile, disponibile.Noleggia())
                }),
            AutoDisponibile => Result<ParcoMezzi>.Fail(new Error($"L'auto scelta non ha capacità sufficiente ({richiesta.PostiMinimi} richiesti)")),
            _ => Result<ParcoMezzi>.Fail(new Error("Auto già noleggiata o in stato non valido"))
        };
    }

    /// <summary>
    /// Noleggia un batch di richieste di noleggio.
    /// In FP, utilizziamo la composizione di funzioni (Bind) su una sequenza di richieste.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaBatch(this ParcoMezzi parco, IEnumerable<RichiestaNoleggio> richieste)
    {
        // Nota per l'audience: L'uso di Aggregate con Bind garantisce l'atomicità:
        // Se una sola richiesta fallisce (es. capacità insufficiente o auto occupata),
        // l'intera catena si interrompe e restituisce l'errore, lasciando il parco originale intatto.
        return richieste.Aggregate(
            Result<ParcoMezzi>.From(parco),
            (risultatoCorrente, richiesta) => risultatoCorrente.Bind(p => p.NoleggiaAuto(richiesta))
        );
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

        return parco.NoleggiaBatch(listaIds.Select(id => new RichiestaNoleggio(clienteId, 1, id)));
    }
}
