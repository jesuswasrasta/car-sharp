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
        var listaIds = ids.ToList();

        // CHECK 1: Duplicati
        if (listaIds.Distinct().Count() != listaIds.Count)
        {
            throw new ArgumentException("Il batch contiene duplicati.", nameof(ids));
        }

        // CHECK 2: Disponibilità
        // Verifichiamo che tutte le auto esistano e siano disponibili PRIMA di modificare qualsiasi stato.
        foreach (var id in listaIds)
        {
            var auto = _auto.FirstOrDefault(a => a.Id == id);
            
            if (auto == null)
            {
                throw new InvalidOperationException($"L'auto con ID {id} non esiste.");
            }

            if (auto.Stato == StatoAuto.Noleggiata)
            {
                throw new InvalidOperationException($"L'auto con targa {auto.Targa} è già noleggiata.");
            }
        }

        // ACT: Esecuzione delle mutazioni
        // A questo punto siamo sicuri che tutte le operazioni avranno successo.
        foreach (var id in listaIds)
        {
            var auto = _auto.First(a => a.Id == id);
            auto.Noleggia();
        }
    }
}