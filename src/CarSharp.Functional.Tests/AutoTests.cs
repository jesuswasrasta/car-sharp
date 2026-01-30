// ABOUTME: Test property-based per le trasformazioni di stato di Auto (FP).
// Verifichiamo che le transizioni di tipo rispettino gli invarianti.

using CarSharp.Functional;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace CarSharp.Functional.Tests;

public class AutoTests
{
    [Property]
    public bool Noleggia_TrasformaAutoDisponibileInAutoNoleggiata(Guid id, string targa)
    {
        // Property: Data un'auto disponibile, la funzione Noleggia deve SEMPRE
        // restituire un'auto noleggiata con lo stesso ID e Targa.
        if (string.IsNullOrWhiteSpace(targa)) return true; // Skip invalid inputs

        var auto = new AutoDisponibile(id, targa, 5);
        var autoNoleggiata = auto.Noleggia();

        return autoNoleggiata is AutoNoleggiata &&
               autoNoleggiata.Id == id &&
               autoNoleggiata.Targa == targa;
    }

    [Property]
    public bool Restituisci_TrasformaAutoNoleggiataInAutoDisponibile(Guid id, string targa)
    {
        // Property: Data un'auto noleggiata, la funzione Restituisci deve SEMPRE
        // restituire un'auto disponibile con lo stesso ID e Targa.
        if (string.IsNullOrWhiteSpace(targa)) return true; // Skip invalid inputs

        var auto = new AutoNoleggiata(id, targa, 5);
        var autoDisponibile = auto.Restituisci();

        return autoDisponibile is AutoDisponibile &&
               autoDisponibile.Id == id &&
               autoDisponibile.Targa == targa;
    }
}
