// ABOUTME: Rappresenta la richiesta di noleggio per un mezzo nel paradigma OOP.
// Incapsula i criteri di selezione (ID specifico e/o capacità minima).

using System;

namespace CarSharp.Oop;

public class RichiestaNoleggio
{
    public string ClienteId { get; }
    public Guid? IdAuto { get; }
    public int PostiMinimi { get; }

    private RichiestaNoleggio(string clienteId, Guid? idAuto, int postiMinimi)
    {
        if (string.IsNullOrWhiteSpace(clienteId)) throw new ArgumentException("L'ID cliente è obbligatorio", nameof(clienteId));
        if (postiMinimi <= 0) throw new ArgumentException("I posti richiesti devono essere positivi", nameof(postiMinimi));
        
        ClienteId = clienteId;
        IdAuto = idAuto;
        PostiMinimi = postiMinimi;
    }

    public static RichiestaNoleggio PerId(string clienteId, Guid id, int postiMinimi = 1) => new(clienteId, id, postiMinimi);
    public static RichiestaNoleggio PerCapacita(string clienteId, int postiMinimi) => new(clienteId, null, postiMinimi);
    
    // Wrapper per retrocompatibilità test se necessario
    public static RichiestaNoleggio PerId(Guid id, int postiMinimi = 1) => new("ANONYMOUS", id, postiMinimi);
    public static RichiestaNoleggio PerCapacita(int postiMinimi) => new("ANONYMOUS", null, postiMinimi);
}
