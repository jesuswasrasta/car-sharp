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
        // Per ora implementiamo solo la parte Act (per il test GREEN), 
        // la validazione (Check) arriverà con la US2.
        foreach (var id in ids)
        {
            var auto = _auto.First(a => a.Id == id);
            auto.Noleggia();
        }
    }
}