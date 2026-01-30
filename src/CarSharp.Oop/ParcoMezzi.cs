// ABOUTME: La classe ParcoMezzi gestisce una collezione di auto utilizzando uno stato mutabile.
// Questo è un classico approccio OOP in cui l'oggetto incapsula i dati 
// e fornisce metodi per modificarli.

using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSharp.Oop;

public class ParcoMezzi
{
    // Stato mutabile interno.
    private readonly List<Auto> _auto = new();

    /// <summary>
    /// Ottiene il numero totale di auto attualmente nel parco mezzi.
    /// </summary>
    public int TotaleAuto => _auto.Count;

    /// <summary>
    /// Ottiene il numero di auto attualmente disponibili per il noleggio.
    /// </summary>
    public int TotaleDisponibili => _auto.Count(a => a.Stato == StatoAuto.Disponibile);

    /// <summary>
    /// Aggiunge un'auto al parco mezzi.
    /// In questo modello OOP, l'aggiunta di un'auto è un'operazione di mutazione dello stato interno.
    /// </summary>
    /// <param name="auto">L'auto da aggiungere.</param>
    /// <exception cref="InvalidOperationException">
    /// Lanciata se l'auto viola l'invariante capacità-prezzo rispetto alle auto esistenti.
    /// </exception>
    public void AggiungiAuto(Auto auto)
    {
        // US2: Verifica invariante capacità-prezzo prima dell'inserimento.
        // In OOP, la validazione avviene spesso all'ingresso dell'oggetto nell'aggregato.
        foreach (var esistente in _auto)
        {
            if (esistente.Capacita == auto.Capacita)
                continue;

            // Invariante commerciale: mezzi più grandi non possono costare meno di mezzi più piccoli.
            if (auto.Capacita > esistente.Capacita && auto.CostoGiornaliero < esistente.CostoGiornaliero)
            {
                throw new InvalidOperationException(
                    $"Violazione invariante capacità-prezzo: l'auto con capacità {auto.Capacita} " +
                    $"non può costare meno ({auto.CostoGiornaliero}€) di un'auto con capacità {esistente.Capacita} " +
                    $"({esistente.CostoGiornaliero}€).");
            }

            if (auto.Capacita < esistente.Capacita && auto.CostoGiornaliero > esistente.CostoGiornaliero)
            {
                throw new InvalidOperationException(
                    $"Violazione invariante capacità-prezzo: l'auto con capacità {auto.Capacita} " +
                    $"non può costare più ({auto.CostoGiornaliero}€) di un'auto con capacità {esistente.Capacita} " +
                    $"({esistente.CostoGiornaliero}€).");
            }
        }

        // Modifichiamo direttamente la collezione interna.
        _auto.Add(auto);
    }

    /// <summary>
    /// Rimuove un'auto dal parco mezzi.
    /// L'operazione di rimozione si affida all'identità referenziale dell'oggetto.
    /// </summary>
    /// <param name="auto">L'istanza specifica dell'auto da rimuovere.</param>
    /// <returns>True se l'oggetto con l'identità fornita è stato trovato e rimosso.</returns>
    public bool RimuoviAuto(Auto auto)
    {
        // In C# OOP, l'uguaglianza predefinita per le classi è basata sul riferimento in memoria.
        // Pertanto, la rimozione ha successo solo se passiamo esattamente lo stesso puntatore 
        // che è stato aggiunto originariamente alla collezione.
        return _auto.Remove(auto);
    }

    /// <summary>
    /// Noleggia un batch di richieste di noleggio.
    /// L'operazione è atomica (Check-Then-Act).
    /// </summary>
    public void NoleggiaBatch(IEnumerable<Guid> ids)
    {
        // Wrapper per compatibilità con i test esistenti della Fase 3.
        NoleggiaBatch(ids.Select(id => RichiestaNoleggio.PerId(id)));
    }

    /// <summary>
    /// Noleggia un batch di richieste complesse.
    /// Garantisce atomicità considerando sia disponibilità che capacità.
    /// </summary>
    public void NoleggiaBatch(IEnumerable<RichiestaNoleggio> richieste)
    {
        var listaRichieste = richieste.ToList();

        // CHECK 1: Duplicati ID (se presenti nelle richieste)
        var idsSemplici = listaRichieste.Where(r => r.IdAuto.HasValue).Select(r => r.IdAuto!.Value).ToList();
        if (idsSemplici.Distinct().Count() != idsSemplici.Count)
        {
            throw new ArgumentException("Il batch contiene duplicati di ID auto.", nameof(richieste));
        }

        // FASE CHECK: Validazione atomica
        var autoAssegnate = new List<Auto>();
        var idsImpegnatiInQuestoBatch = new HashSet<Guid>();

        foreach (var richiesta in listaRichieste)
        {
            Auto? autoIdonea;

            if (richiesta.IdAuto.HasValue)
            {
                // Richiesta per ID specifico
                var id = richiesta.IdAuto.Value;
                autoIdonea = _auto.FirstOrDefault(a => a.Id == id);

                if (autoIdonea == null || autoIdonea.Stato == StatoAuto.Noleggiata || idsImpegnatiInQuestoBatch.Contains(id))
                {
                    throw new InvalidOperationException($"L'auto specifica con ID {id} non è disponibile.");
                }

                // Verifica capacità
                if (autoIdonea.Capacita < richiesta.PostiMinimi)
                {
                    throw new InvalidOperationException($"L'auto con ID {id} non ha capacità sufficiente.");
                }
            }
            else
            {
                // Richiesta per capacità: prendiamo la prima disponibile che soddisfa il requisito.
                // US1: Applichiamo l'algoritmo Best Fit ordinando per capacità.
                autoIdonea = _auto
                    .Where(a => 
                        a.Stato == StatoAuto.Disponibile && 
                        a.Capacita >= richiesta.PostiMinimi &&
                        !idsImpegnatiInQuestoBatch.Contains(a.Id))
                    .OrderBy(a => a.Capacita)
                    .FirstOrDefault();

                if (autoIdonea == null)
                {
                    throw new InvalidOperationException($"Nessuna auto disponibile con almeno {richiesta.PostiMinimi} posti.");
                }
            }

            autoAssegnate.Add(autoIdonea);
            idsImpegnatiInQuestoBatch.Add(autoIdonea.Id);
        }

        // FASE ACT: Mutazione
        foreach (var auto in autoAssegnate)
        {
            auto.Noleggia();
        }
    }

    /// <summary>
    /// Noleggia un'auto che soddisfi il requisito minimo di posti.
    /// In questa fase, viene scelta l'auto con la capacità minima che soddisfi il requisito (Best Fit).
    /// A parità di capacità, viene scelta la prima in ordine di inserimento (determinismo).
    /// </summary>
    /// <param name="postiMinimi">Il numero minimo di posti richiesti.</param>
    /// <returns>L'istanza dell'auto noleggiata.</returns>
    /// <exception cref="InvalidOperationException">Lanciata se nessuna auto idonea è disponibile.</exception>
    public Auto Noleggia(int postiMinimi)
    {
        // Best Fit Algorithm: ordiniamo per capacità crescente e prendiamo la prima.
        // Questo garantisce di assegnare l'auto più piccola che soddisfa il requisito,
        // preservando le auto più grandi per richieste future con maggiore capacità.
        // OrderBy è stabile in LINQ, quindi a parità di capacità mantiene l'ordine originale.
        var autoIdonea = _auto
            .Where(a => a.Stato == StatoAuto.Disponibile && a.Capacita >= postiMinimi)
            .OrderBy(a => a.Capacita)
            .FirstOrDefault();

        if (autoIdonea == null)
        {
            throw new InvalidOperationException($"Nessuna auto disponibile con almeno {postiMinimi} posti.");
        }

        autoIdonea.Noleggia();
        return autoIdonea;
    }

    /// <summary>
    /// Noleggia un'auto che soddisfi il requisito minimo di posti, restituendo anche il costo.
    /// Implementa l'algoritmo "Best Fit".
    /// </summary>
    /// <param name="postiMinimi">Il numero minimo di posti richiesti.</param>
    /// <param name="clienteId">L'identificativo del cliente.</param>
    /// <returns>Un RisultatoNoleggio contenente l'auto noleggiata e il costo giornaliero.</returns>
    /// <exception cref="InvalidOperationException">Lanciata se nessuna auto idonea è disponibile.</exception>
    public RisultatoNoleggio NoleggiaConCosto(int postiMinimi, string clienteId)
    {
        // Utilizziamo l'algoritmo Best Fit per minimizzare lo spreco di posti.
        var autoIdonea = _auto
            .Where(a => a.Stato == StatoAuto.Disponibile && a.Capacita >= postiMinimi)
            .OrderBy(a => a.Capacita)
            .FirstOrDefault();

        if (autoIdonea == null)
        {
            throw new InvalidOperationException($"Nessuna auto disponibile con almeno {postiMinimi} posti.");
        }

        autoIdonea.Noleggia();
        return RisultatoNoleggio.Da(autoIdonea, clienteId);
    }

    /// <summary>
    /// Processa un batch di richieste di noleggio applicando sconti per clienti multipli.
    /// L'operazione è atomica: se una richiesta fallisce, nessuna viene effettuata.
    /// </summary>
    /// <param name="richieste">Lista delle richieste di noleggio.</param>
    /// <param name="scontoPercentuale">Percentuale di sconto (0.0 - 1.0) per clienti con >1 prenotazione.</param>
    /// <returns>RisultatoBatch contenente i dettagli e i totali.</returns>
    public RisultatoBatch PrenotaBatch(IEnumerable<RichiestaNoleggio> richieste, decimal scontoPercentuale)
    {
        if (scontoPercentuale < 0 || scontoPercentuale > 1)
        {
            throw new ArgumentException("La percentuale di sconto deve essere compresa tra 0 e 1 (0-100%).", nameof(scontoPercentuale));
        }

        var listaRichieste = richieste.ToList();

        // CHECK 1: Duplicati ID
        var idsSemplici = listaRichieste.Where(r => r.IdAuto.HasValue).Select(r => r.IdAuto!.Value).ToList();
        if (idsSemplici.Distinct().Count() != idsSemplici.Count)
        {
            throw new ArgumentException("Il batch contiene duplicati di ID auto.", nameof(richieste));
        }

        // FASE CHECK: Validazione atomica e preparazione risultati
        var noleggiRiusciti = new List<RisultatoNoleggio>();
        var idsImpegnatiInQuestoBatch = new HashSet<Guid>();

        foreach (var richiesta in listaRichieste)
        {
            Auto? autoIdonea;

            if (richiesta.IdAuto.HasValue)
            {
                var id = richiesta.IdAuto.Value;
                autoIdonea = _auto.FirstOrDefault(a => a.Id == id);

                if (autoIdonea == null || autoIdonea.Stato == StatoAuto.Noleggiata || idsImpegnatiInQuestoBatch.Contains(id))
                {
                    throw new InvalidOperationException($"L'auto specifica con ID {id} non è disponibile.");
                }

                if (autoIdonea.Capacita < richiesta.PostiMinimi)
                {
                    throw new InvalidOperationException($"L'auto con ID {id} non ha capacità sufficiente.");
                }
            }
            else
            {
                autoIdonea = _auto
                    .Where(a => 
                        a.Stato == StatoAuto.Disponibile && 
                        a.Capacita >= richiesta.PostiMinimi &&
                        !idsImpegnatiInQuestoBatch.Contains(a.Id))
                    .OrderBy(a => a.Capacita)
                    .FirstOrDefault();

                if (autoIdonea == null)
                {
                    throw new InvalidOperationException($"Nessuna auto disponibile con almeno {richiesta.PostiMinimi} posti.");
                }
            }

            noleggiRiusciti.Add(RisultatoNoleggio.Da(autoIdonea, richiesta.ClienteId));
            idsImpegnatiInQuestoBatch.Add(autoIdonea.Id);
        }

        // FASE ACT: Mutazione
        foreach (var noleggio in noleggiRiusciti)
        {
            noleggio.Auto.Noleggia();
        }

        // FASE CALCOLO SCONTI (Modellata come aggregazione post-noleggio)
        var riepilogoClienti = noleggiRiusciti
            .GroupBy(n => n.ClienteId)
            .Select(g =>
            {
                var totaleLordo = g.Sum(n => n.Costo);
                var haSconto = g.Count() > 1;
                var importoSconto = haSconto ? totaleLordo * scontoPercentuale : 0m;
                var totaleNetto = Math.Max(0, totaleLordo - importoSconto);

                return new DettaglioCostiCliente(g.Key, totaleLordo, importoSconto, totaleNetto);
            })
            .ToDictionary(d => d.ClienteId);

        var totaleGenerale = riepilogoClienti.Values.Sum(d => d.TotaleNetto);

        return new RisultatoBatch(noleggiRiusciti, riepilogoClienti, totaleGenerale);
    }
}