// ABOUTME: Oggetto che incapsula il risultato di un batch di noleggi nel paradigma OOP.
// Contiene l'elenco dei singoli noleggi, il riepilogo per cliente e il totale generale.

using System.Collections.Generic;
using System.Linq;

namespace CarSharp.Oop;

public class RisultatoBatch
{
    public IEnumerable<RisultatoNoleggio> Noleggi { get; }
    public IDictionary<string, DettaglioCostiCliente> RiepilogoClienti { get; }
    public decimal TotaleGenerale { get; }

    public RisultatoBatch(
        IEnumerable<RisultatoNoleggio> noleggi, 
        IDictionary<string, DettaglioCostiCliente> riepilogoClienti, 
        decimal totaleGenerale)
    {
        Noleggi = noleggi;
        RiepilogoClienti = riepilogoClienti;
        TotaleGenerale = totaleGenerale;
    }
}

public class DettaglioCostiCliente
{
    public string ClienteId { get; }
    public decimal TotaleLordo { get; }
    public decimal ScontoApplicato { get; }
    public decimal TotaleNetto { get; }

    public DettaglioCostiCliente(string clienteId, decimal totaleLordo, decimal scontoApplicato, decimal totaleNetto)
    {
        ClienteId = clienteId;
        TotaleLordo = totaleLordo;
        ScontoApplicato = scontoApplicato;
        TotaleNetto = totaleNetto;
    }
}
