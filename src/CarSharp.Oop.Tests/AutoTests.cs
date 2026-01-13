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
        // Un'entità deve avere un'identità stabile (Id), uno stato iniziale e la capacità.
        var id = Guid.NewGuid();
        var targa = "AA123BB";
        var statoIniziale = StatoAuto.Disponibile;
        var capacita = 5;

        var auto = new Auto(id, targa, statoIniziale, capacita);

        Assert.Equal(id, auto.Id);
        Assert.Equal(targa, auto.Targa);
        Assert.Equal(statoIniziale, auto.Stato);
        Assert.Equal(capacita, auto.Capacita);
    }

    [Fact]
    public void Costruttore_DovrebbeLanciareArgumentException_QuandoCapacitaEZeroONegativa()
    {
        // Validiamo i vincoli di dominio già nel costruttore.
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Disponibile, 0));
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Disponibile, -1));
    }

    [Fact]
    public void Noleggia_DovrebbeCambiareLoStatoInNoleggiata_QuandoDisponibile()
    {
        // L'azione di noleggio deve mutare lo stato interno dell'oggetto per riflettere l'operazione avvenuta.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Disponibile, 5);

        auto.Noleggia();

        Assert.Equal(StatoAuto.Noleggiata, auto.Stato);
    }

    [Fact]
    public void Noleggia_DovrebbeLanciareInvalidOperationException_QuandoGiaNoleggiata()
    {
        // In OOP, le eccezioni sono il modo idiomatico per segnalare violazioni di regole di business o stati invalidi.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Noleggiata, 5);

        var ex = Assert.Throws<InvalidOperationException>(() => auto.Noleggia());
        Assert.Equal("L'auto è già noleggiata.", ex.Message);
    }

    [Fact]
    public void Restituisci_DovrebbeCambiareLoStatoInDisponibile_QuandoNoleggiata()
    {
        // L'azione di restituzione deve mutare lo stato interno per rendere l'auto nuovamente disponibile.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", StatoAuto.Noleggiata, 5);

        auto.Restituisci();

        Assert.Equal(StatoAuto.Disponibile, auto.Stato);
    }
}
