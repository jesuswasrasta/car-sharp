// ABOUTME: Test unitari per i tipi Auto nel paradigma funzionale.
// Verifichiamo l'uguaglianza basata sul valore e l'immutabilità dei record.

using CarSharp.Functional;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace CarSharp.Functional.Tests;

public class AutoTests
{
    [Property]
    public bool AutoDisponibile_DovrebbeAvereValueEquality(Guid id, string targa)
    {
        // Perché: In FP, due dati con lo stesso valore sono considerati identici.
        var auto1 = new AutoDisponibile(id, targa);
        var auto2 = new AutoDisponibile(id, targa);

        return auto1 == auto2 && !ReferenceEquals(auto1, auto2);
    }

    [Property]
    public bool AutoNoleggiata_DovrebbeAvereValueEquality(Guid id, string targa)
    {
        // Perché: L'uguaglianza basata sul valore deve valere per tutti i tipi di record del dominio.
        var auto1 = new AutoNoleggiata(id, targa);
        var auto2 = new AutoNoleggiata(id, targa);

        return auto1 == auto2 && !ReferenceEquals(auto1, auto2);
    }

    [Property]
    public bool Noleggia_DovrebbeTrasformareInAutoNoleggiata(Guid id, string targa)
    {
        // Perché: In FP, le operazioni di business sono trasformazioni di dati (Type-Driven Design).
        // Il successo è garantito dalla firma: un'auto disponibile può sempre essere noleggiata.
        var disponibile = new AutoDisponibile(id, targa);

        AutoNoleggiata noleggiata = disponibile.Noleggia();

        return noleggiata.Id == id && noleggiata.Targa == targa;
    }

    [Property]
    public bool Restituisci_DovrebbeTrasformareInAutoDisponibile(Guid id, string targa)
    {
        // Perché: La restituzione è l'operazione inversa, una trasformazione da Noleggiata a Disponibile.
        var noleggiata = new AutoNoleggiata(id, targa);

        AutoDisponibile disponibile = noleggiata.Restituisci();

        return disponibile.Id == id && disponibile.Targa == targa;
    }
}
