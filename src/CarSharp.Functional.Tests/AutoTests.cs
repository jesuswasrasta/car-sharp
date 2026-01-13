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
    public bool AutoDisponibile_DovrebbeAvereValueEquality(Guid id, string targa, int capacita)
    {
        // In FP, due dati con lo stesso valore sono considerati identici.
        var auto1 = new AutoDisponibile(id, targa, capacita);
        var auto2 = new AutoDisponibile(id, targa, capacita);

        return auto1 == auto2 && !ReferenceEquals(auto1, auto2);
    }

    [Property]
    public bool AutoNoleggiata_DovrebbeAvereValueEquality(Guid id, string targa, int capacita)
    {
        // L'uguaglianza basata sul valore deve valere per tutti i tipi di record del dominio.
        var auto1 = new AutoNoleggiata(id, targa, capacita);
        var auto2 = new AutoNoleggiata(id, targa, capacita);

        return auto1 == auto2 && !ReferenceEquals(auto1, auto2);
    }

    [Property]
    public bool Noleggia_DovrebbeTrasformareInAutoNoleggiata(Guid id, string targa, int capacita)
    {
        // In FP, le operazioni di business sono trasformazioni di dati (Type-Driven Design).
        // Il successo è garantito dalla firma: un'auto disponibile può sempre essere noleggiata.
        var disponibile = new AutoDisponibile(id, targa, capacita);

        AutoNoleggiata noleggiata = disponibile.Noleggia();

        return noleggiata.Id == id && noleggiata.Targa == targa && noleggiata.Capacita == capacita;
    }

    [Property]
    public bool Restituisci_DovrebbeTrasformareInAutoDisponibile(Guid id, string targa, int capacita)
    {
        // La restituzione è l'operazione inversa, una trasformazione da Noleggiata a Disponibile.
        var noleggiata = new AutoNoleggiata(id, targa, capacita);

        AutoDisponibile disponibile = noleggiata.Restituisci();

        return disponibile.Id == id && disponibile.Targa == targa && disponibile.Capacita == capacita;
    }
}
