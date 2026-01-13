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
        var auto1 = new AutoNoleggiata(id, targa);
        var auto2 = new AutoNoleggiata(id, targa);

        return auto1 == auto2 && !ReferenceEquals(auto1, auto2);
    }
}
