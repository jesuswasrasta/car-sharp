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

    [Fact]
    public void Noleggia_DovrebbeCambiareLoStatoInNoleggiata_QuandoDisponibile()
    {
        // Perché: L'azione di noleggio deve mutare lo stato interno dell'oggetto per riflettere l'operazione avvenuta.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Disponibile);

        auto.Noleggia();

        Assert.Equal(StatoAuto.Noleggiata, auto.Stato);
    }

    [Fact]
    public void Noleggia_DovrebbeLanciareInvalidOperationException_QuandoGiaNoleggiata()
    {
        // Perché: In OOP, le eccezioni sono il modo idiomatico per segnalare violazioni di regole di business o stati invalidi.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Noleggiata);

        var ex = Assert.Throws<InvalidOperationException>(() => auto.Noleggia());
        Assert.Equal("L'auto è già noleggiata.", ex.Message);
    }

    [Fact]
    public void Restituisci_DovrebbeCambiareLoStatoInDisponibile_QuandoNoleggiata()
    {
        // Perché: L'azione di restituzione deve mutare lo stato interno per rendere l'auto nuovamente disponibile.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Noleggiata);

        auto.Restituisci();

        Assert.Equal(StatoAuto.Disponibile, auto.Stato);
    }
}
