// ABOUTME: Test unitari per l'entità Auto nel paradigma OOP.
// Verifichiamo il comportamento dell'oggetto e la gestione del suo stato interno.

using CarSharp.Oop;
using Xunit;

namespace CarSharp.Oop.Tests;

public class AutoTests
{
    [Fact]
    public void Costruttore_DovrebbeInizializzareCorrettamenteLeProprieta()
    {
        // Perché: Un'entità deve avere un'identità stabile (Id) e uno stato iniziale definito.
        var id = Guid.NewGuid();
        var targa = "AA123BB";
        var statoIniziale = StatoAuto.Disponibile;

        var auto = new Auto(id, targa, statoIniziale);

        Assert.Equal(id, auto.Id);
        Assert.Equal(targa, auto.Targa);
        Assert.Equal(statoIniziale, auto.Stato);
    }
}
