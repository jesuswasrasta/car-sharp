// ABOUTME: La classe ParcoMezzi gestisce una collezione di auto utilizzando uno stato mutabile.
// Questo è un classico approccio OOP in cui l'oggetto incapsula i dati 
// e fornisce metodi per modificarli.

using System.Collections.Generic;

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
}