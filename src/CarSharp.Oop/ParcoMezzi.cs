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
    /// In questo modello OOP, modifichiamo la lista interna sul posto (in-place).
    /// </summary>
    /// <param name="auto">L'auto da aggiungere.</param>
    public void AggiungiAuto(Auto auto)
    {
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
}