// ABOUTME: Rappresenta la richiesta di noleggio per un mezzo nel paradigma OOP.
// Incapsula i criteri di selezione (ID specifico e/o capacità minima).

using System;

namespace CarSharp.Oop;

public class RichiestaNoleggio
{
    public Guid? IdAuto { get; }
    public int PostiMinimi { get; }

    private RichiestaNoleggio(Guid? idAuto, int postiMinimi)
    {
        if (postiMinimi <= 0) throw new ArgumentException("I posti richiesti devono essere positivi", nameof(postiMinimi));
        
        IdAuto = idAuto;
        PostiMinimi = postiMinimi;
    }

    public static RichiestaNoleggio PerId(Guid id, int postiMinimi = 1) => new(id, postiMinimi);
    public static RichiestaNoleggio PerCapacita(int postiMinimi) => new(null, postiMinimi);
}
