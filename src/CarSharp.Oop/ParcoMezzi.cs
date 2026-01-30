// ABOUTME: La classe ParcoMezzi gestisce una collezione di auto utilizzando uno stato mutabile.
// Questo è un classico approccio OOP in cui l'oggetto incapsula i dati 
// e fornisce metodi per modificarli.

using System.Collections.Generic;

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
}