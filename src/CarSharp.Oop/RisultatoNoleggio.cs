// ABOUTME: Oggetto che incapsula il risultato di un'operazione di noleggio nel paradigma OOP.
// Contiene il riferimento all'auto assegnata e il costo calcolato per l'operazione.

using System;

namespace CarSharp.Oop;

public class RisultatoNoleggio
{
    public Auto Auto { get; }
    public decimal Costo { get; }
    public string ClienteId { get; }

    private RisultatoNoleggio(Auto auto, decimal costo, string clienteId)
    {
        Auto = auto;
        Costo = costo;
        ClienteId = clienteId;
    }

    public static RisultatoNoleggio Da(Auto auto, string clienteId)
    {
        return new RisultatoNoleggio(auto, auto.CostoGiornaliero, clienteId);
    }
}
