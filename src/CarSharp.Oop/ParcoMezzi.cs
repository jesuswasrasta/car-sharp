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
}
