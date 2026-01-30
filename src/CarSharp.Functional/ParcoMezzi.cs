// ABOUTME: Il ParcoMezzi è modellato come un record immutabile.
// Invece di modificare il parco mezzi, le operazioni restituiscono una nuova istanza 
// che rappresenta lo stato aggiornato.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CarSharp.Functional;

/// <summary>
/// Rappresenta un parco mezzi di auto come una struttura dati immutabile.
/// In FP, il ParcoMezzi non è un contenitore che cambia nel tempo, ma un 'Valore' 
/// che descrive lo stato della flotta in un istante preciso. Ogni operazione non 
/// altera questo valore, ma ne produce uno nuovo, garantendo la thread-safety 
/// e la predicibilità del comportamento.
/// </summary>
/// <param name="auto">La collezione immutabile di auto.</param>
public record ParcoMezzi(ImmutableList<IAuto> auto)
{
    /// <summary>
    /// Restituisce un'istanza di parco mezzi vuota.
    /// Invece di un costruttore, utilizziamo una costante (Singleton) che funge da 
    /// 'Elemento Neutro' per le nostre operazioni di trasformazione. Questo è il 
    /// punto di partenza (Seed) per popolare la nostra flotta.
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

public record DettaglioCliente(
    string ClienteId,
    int NumeroPrenotazioni,
    decimal TotaleLordo,
    decimal ScontoApplicato,
    decimal TotaleNetto
);

/// <summary>
/// Metodi di estensione per trasformare un ParcoMezzi.
/// Coerentemente con il paradigma FP, separiamo i dati (record) dalle funzioni 
/// (metodi di estensione) che operano su di essi, mantenendo la logica pura e 
/// facilmente componibile.
/// </summary>
public static class ParcoMezziExtensions
{
    /// <summary>
    /// 'Aggiunge' un'auto al parco mezzi restituendo una nuova istanza.
    /// </summary>
    public static Result<ParcoMezzi> AggiungiAuto(this ParcoMezzi parco, IAuto auto)
    {
        // US2: Verifica invariante capacità-prezzo.
        // Nel mondo funzionale, la validazione restituisce un Failure invece di lanciare eccezioni.
        foreach (var esistente in parco.auto)
        {
            if (esistente.Capacita == auto.Capacita)
                continue;

            // Invariante commerciale: mezzi più grandi non possono costare meno di mezzi più piccoli.
            if (auto.Capacita > esistente.Capacita && auto.CostoGiornaliero < esistente.CostoGiornaliero)
            {
                return Result<ParcoMezzi>.Fail(
                    $"Violazione invariante capacità-prezzo: l'auto con capacità {auto.Capacita} " +
                    $"non può costare meno ({auto.CostoGiornaliero}€) di un'auto con capacità {esistente.Capacita} " +
                    $"({esistente.CostoGiornaliero}€).");
            }

            if (auto.Capacita < esistente.Capacita && auto.CostoGiornaliero > esistente.CostoGiornaliero)
            {
                return Result<ParcoMezzi>.Fail(
                    $"Violazione invariante capacità-prezzo: l'auto con capacità {auto.Capacita} " +
                    $"non può costare più ({auto.CostoGiornaliero}€) di un'auto con capacità {esistente.Capacita} " +
                    $"({esistente.CostoGiornaliero}€).");
            }
        }

        return Result<ParcoMezzi>.From(parco with { auto = parco.auto.Add(auto) });
    }

    /// <summary>
    /// 'Rimuovi' un'auto dal parco mezzi producendo un nuovo valore.
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
    /// Noleggia un'auto e restituisce un RisultatoNoleggio contenente l'auto,
    /// il costo giornaliero e il nuovo stato del parco.
    /// </summary>
    public static Result<RisultatoNoleggio> NoleggiaConCosto(this ParcoMezzi parco, RichiestaNoleggio richiesta)
    {
        IAuto? autoIdonea;

        if (richiesta.IdAuto.HasValue)
        {
            autoIdonea = parco.auto.FirstOrDefault(a => a.Id == richiesta.IdAuto.Value);
        }
        else
        {
            // Best Fit Algorithm: ordiniamo per capacità crescente.
            autoIdonea = parco.auto
                .OfType<AutoDisponibile>()
                .Where(a => a.Capacita >= richiesta.PostiMinimi)
                .OrderBy(a => a.Capacita)
                .FirstOrDefault();
        }

        if (autoIdonea == null)
            return Result<RisultatoNoleggio>.Fail($"Nessuna auto disponibile con almeno {richiesta.PostiMinimi} posti");

        return autoIdonea switch
        {
            AutoDisponibile disponibile when disponibile.Capacita >= richiesta.PostiMinimi =>
                Result<RisultatoNoleggio>.From(ExecuteNoleggio(parco, disponibile, richiesta.ClienteId)),
            AutoDisponibile => Result<RisultatoNoleggio>.Fail($"L'auto scelta non ha capacità sufficiente ({richiesta.PostiMinimi} richiesti)"),
            _ => Result<RisultatoNoleggio>.Fail("Auto già noleggiata o in stato non valido")
        };
    }

    private static RisultatoNoleggio ExecuteNoleggio(ParcoMezzi parco, AutoDisponibile autoIdonea, string clienteId)
    {
        var autoNoleggiata = autoIdonea.Noleggia();
        var nuovoParco = parco with {
            auto = parco.auto.Replace(autoIdonea, autoNoleggiata)
        };

        return new RisultatoNoleggio(autoNoleggiata, autoNoleggiata.CostoGiornaliero, clienteId, nuovoParco);
    }

    /// <summary>
    /// Tenta di noleggiare un'auto in base a una richiesta specifica.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaAuto(this ParcoMezzi parco, RichiestaNoleggio richiesta)
    {
        return parco.NoleggiaConCosto(richiesta).Bind(r => Result<ParcoMezzi>.From(r.ParcoAggiornato));
    }

    /// <summary>
    /// Noleggia un batch di richieste di noleggio.
    /// In FP, utilizziamo la composizione di funzioni (Bind) su una sequenza di richieste.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaBatch(this ParcoMezzi parco, IEnumerable<RichiestaNoleggio> richieste)
    {
        return richieste.Aggregate(
            Result<ParcoMezzi>.From(parco),
            (risultatoCorrente, richiesta) => risultatoCorrente.Bind(p => p.NoleggiaAuto(richiesta))
        );
    }

    /// <summary>
    /// Noleggia un batch di auto identificandole tramite ID.
    /// </summary>
    public static Result<ParcoMezzi> NoleggiaBatch(this ParcoMezzi parco, IEnumerable<Guid> ids, string clienteId)
    {
        var listaIds = ids.ToList();
        if (listaIds.Distinct().Count() != listaIds.Count)
            return Result<ParcoMezzi>.Fail("Il batch contiene duplicati");

        return parco.NoleggiaBatch(listaIds.Select(id => new RichiestaNoleggio(clienteId, 1, id)));
    }

    /// <summary>
    /// Processa un batch di richieste e restituisce un RisultatoBatch contenente
    /// la lista dei noleggi, il riepilogo per cliente e il costo totale.
    /// L'operazione è atomica: se una richiesta fallisce, nessuna viene effettuata.
    /// </summary>
    public static Result<RisultatoBatch> PrenotaBatch(this ParcoMezzi parco, IEnumerable<RichiestaNoleggio> richieste, decimal scontoPercentuale)
    {
        if (scontoPercentuale < 0 || scontoPercentuale > 1)
            return Result<RisultatoBatch>.Fail("La percentuale di sconto deve essere compresa tra 0 e 1.");

        var listaRichieste = richieste.ToList();
        var risultatiNoleggi = new List<RisultatoNoleggio>();
        var parcoCorrente = parco;

        foreach (var richiesta in listaRichieste)
        {
            var risultatoNoleggio = parcoCorrente.NoleggiaConCosto(richiesta);

            if (!risultatoNoleggio.IsSuccess)
            {
                return Result<RisultatoBatch>.Fail(risultatoNoleggio.Error!);
            }

            risultatiNoleggi.Add(risultatoNoleggio.Value!);
            parcoCorrente = risultatoNoleggio.Value!.ParcoAggiornato;
        }

        var noleggiImmutabili = ImmutableList.CreateRange(risultatiNoleggi);
        
        var riepilogoClienti = risultatiNoleggi
            .GroupBy(r => r.ClienteId)
            .Select(g =>
            {
                var lordo = g.Sum(r => r.Costo);
                var numPrenotazioni = g.Count();
                var importoSconto = numPrenotazioni > 1 ? lordo * scontoPercentuale : 0m;
                var netto = Math.Max(0, lordo - importoSconto);

                return new DettaglioCliente(g.Key, numPrenotazioni, lordo, importoSconto, netto);
            })
            .ToList();

        var costoTotaleGenerale = riepilogoClienti.Sum(d => d.TotaleNetto);

        return Result<RisultatoBatch>.From(
            new RisultatoBatch(noleggiImmutabili, costoTotaleGenerale, parcoCorrente));
    }
}