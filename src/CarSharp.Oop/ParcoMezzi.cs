// ABOUTME: La classe ParcoMezzi gestisce una collezione di auto utilizzando uno stato mutabile.
// Questo è un classico approccio OOP in cui l'oggetto incapsula i dati 
// e fornisce metodi per modificarli.

using System.Collections.Generic;
using System.Linq;

namespace CarSharp.Oop;

/// <summary>
/// Gestisce una collezione di auto.
/// In OOP, il ParcoMezzi è un 'Contenitore' che mantiene il proprio stato interno.
/// </summary>
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
        // Semplice mutazione sul posto dello stato privato.
        _auto.Add(auto);
    }

    /// <summary>
    /// Rimuove un'auto dal parco mezzi.
    /// Restituisce true se l'auto è stata trovata e rimossa; altrimenti, false.
    /// </summary>
    /// <param name="auto">L'istanza dell'auto da rimuovere.</param>
    public bool RimuoviAuto(Auto auto)
    {
        // List.Remove utilizza il comparatore di uguaglianza predefinito.
        // Per le classi, questo significa uguaglianza per riferimento.
        return _auto.Remove(auto);
    }

    /// <summary>
    /// Noleggia un'auto che soddisfi il requisito minimo di posti.
    /// Viene scelta la prima auto disponibile in ordine di inserimento che ha capacità sufficiente.
    /// </summary>
    /// <param name="postiMinimi">Il numero minimo di posti richiesti.</param>
    /// <returns>L'istanza dell'auto noleggiata.</returns>
    /// <exception cref="InvalidOperationException">Lanciata se nessuna auto idonea è disponibile.</exception>
    public Auto Noleggia(int postiMinimi)
    {
        // Cerchiamo la prima auto che sia Disponibile E abbia Capacita >= postiMinimi.
        var autoIdonea = _auto.FirstOrDefault(a => a.Stato == StatoAuto.Disponibile && a.Capacita >= postiMinimi);

        if (autoIdonea == null)
        {
            throw new InvalidOperationException($"Nessuna auto disponibile con almeno {postiMinimi} posti.");
        }

        autoIdonea.Noleggia();
        return autoIdonea;
    }

    /// <summary>
    /// Noleggia un batch di richieste di noleggio.
    /// L'operazione è atomica (Check-Then-Act).
    /// </summary>
    public void NoleggiaBatch(IEnumerable<RichiestaNoleggio> richieste)
    {
        var listaRichieste = richieste.ToList();

        // Controllo preventivo duplicati ID (se presenti)
        var idsSemplici = listaRichieste.Where(r => r.IdAuto.HasValue).Select(r => r.IdAuto!.Value).ToList();
        if (idsSemplici.Distinct().Count() != idsSemplici.Count)
        {
            throw new ArgumentException("Il batch contiene dei duplicati di ID auto.");
        }

        // FASE CHECK: Validiamo tutte le richieste senza modificare lo stato.
        var autoAssegnate = new List<Auto>();
        var idsImpegnatiInQuestoBatch = new HashSet<Guid>();

        foreach (var richiesta in listaRichieste)
        {
            Auto? autoIdonea;

            if (richiesta.IdAuto.HasValue)
            {
                // Richiesta specifica per ID
                var id = richiesta.IdAuto.Value;
                autoIdonea = _auto.FirstOrDefault(a => a.Id == id);

                if (autoIdonea == null || autoIdonea.Stato == StatoAuto.Noleggiata || idsImpegnatiInQuestoBatch.Contains(id))
                {
                    throw new InvalidOperationException($"L'auto specifica con ID {id} non è disponibile.");
                }

                // FR-006: Verifica capacità se specificata
                if (autoIdonea.Capacita < richiesta.PostiMinimi)
                {
                    throw new InvalidOperationException($"L'auto con ID {id} ha capacità {autoIdonea.Capacita}, insufficiente per i {richiesta.PostiMinimi} posti richiesti.");
                }
            }
            else
            {
                // Richiesta per capacità (prima disponibile)
                autoIdonea = _auto.FirstOrDefault(a => 
                    a.Stato == StatoAuto.Disponibile && 
                    a.Capacita >= richiesta.PostiMinimi && 
                    !idsImpegnatiInQuestoBatch.Contains(a.Id));

                if (autoIdonea == null)
                {
                    throw new InvalidOperationException($"Nessuna auto disponibile con almeno {richiesta.PostiMinimi} posti.");
                }
            }

            autoAssegnate.Add(autoIdonea);
            idsImpegnatiInQuestoBatch.Add(autoIdonea.Id);
        }

        // FASE ACT: Applichiamo le mutazioni.
        foreach (var auto in autoAssegnate)
        {
            auto.Noleggia();
        }
    }

    /// <summary>
    /// Noleggia un batch di auto identificandole tramite targa (compatibilità Fase 3).
    /// </summary>
    public void NoleggiaBatch(IEnumerable<Guid> ids)
    {
        // Trasformiamo i semplici ID in oggetti RichiestaNoleggio.
        NoleggiaBatch(ids.Select(id => RichiestaNoleggio.PerId(id)));
    }
}
