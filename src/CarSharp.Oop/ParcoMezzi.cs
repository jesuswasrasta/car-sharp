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
    /// Noleggia un batch di auto identificandole tramite targa.
    /// L'operazione deve essere atomica: o tutte le auto vengono noleggiate, o nessuna.
    /// </summary>
    /// <param name="targhe">Elenco delle targhe delle auto da noleggiare.</param>
    public void NoleggiaBatch(IEnumerable<Guid> targhe)
    {
        // In OOP, l'atomicità in-memory può essere garantita con un approccio "Check-Then-Act".
        // Prima validiamo che l'intera operazione sia possibile, poi applichiamo le mutazioni.

        var listaTarghe = targhe.ToList();

        // Nota per l'audience: Iniziamo con i controlli di pre-condizione.
        // Se il batch contiene duplicati, è una richiesta invalida a priori.
        if (listaTarghe.Distinct().Count() != listaTarghe.Count)
        {
            throw new ArgumentException("Il batch contiene dei duplicati.");
        }

        // FASE CHECK: Cerchiamo tutte le auto e verifichiamo la loro disponibilità.
        // Se una sola auto non è disponibile, interrompiamo tutto PRIMA di modificare lo stato.
        var autoDaNoleggiare = new List<Auto>();
        foreach (var id in listaTarghe)
        {
            var auto = _auto.FirstOrDefault(a => a.Id == id);
            
            // Verifichiamo l'esistenza e lo stato. 
            // In OOP, l'oggetto ParcoMezzi coordina gli oggetti Auto.
            if (auto == null || auto.Stato == StatoAuto.Noleggiata)
            {
                throw new InvalidOperationException($"L'auto con ID {id} non è disponibile o non esiste.");
            }
            autoDaNoleggiare.Add(auto);
        }

        // FASE ACT: Solo se siamo sicuri che tutto il batch sia valido, procediamo alla mutazione.
        // Questo garantisce l'atomicità in-memory senza bisogno di rollback complessi.
        foreach (var auto in autoDaNoleggiare)
        {
            auto.Noleggia();
        }
    }
}
