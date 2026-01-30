// ABOUTME: Test per la classe Auto nel paradigma OOP.
// Verifichiamo che l'entità mantenga i suoi invarianti (identità e validità dati).

using CarSharp.Oop;
using Xunit;

namespace CarSharp.Oop.Tests;

public class AutoTests
{
    [Fact]
    public void Costruttore_RichiedeIdETargaValidi()
    {
        // In OOP, il costruttore è il guardiano dell'integrità dell'oggetto.
        // Non deve essere possibile creare un'auto senza un'identità tecnica (Guid)
        // e un identificativo di dominio (Targa).
        
        // Targa nulla
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), null!, 5, 50m));
        
        // Targa vuota
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "", 5, 50m));
        
        // ID vuoto
        Assert.Throws<ArgumentException>(() => new Auto(Guid.Empty, "AA123BB", 5, 50m));

        // Capacità non valida
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "AA123BB", 0, 50m));
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "AA123BB", -1, 50m));

        // Costo giornaliero non valido
        // L'uso di decimal garantisce la precisione necessaria per i calcoli finanziari,
        // evitando gli errori di arrotondamento tipici dei tipi a virgola mobile.
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "AA123BB", 5, 0m));
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "AA123BB", 5, -10m));
    }

    [Fact]
    public void Costruttore_InizializzaCorrettamenteLeProprieta()
    {
        // Verifichiamo che i valori passati al costruttore siano correttamente
        // assegnati e accessibili tramite le proprietà pubbliche.
        var id = Guid.NewGuid();
        var targa = "AA123BB";
        var capacita = 5;
        var costo = 45.50m;
        
        var auto = new Auto(id, targa, capacita, costo);
        
        Assert.Equal(id, auto.Id);
        Assert.Equal(targa, auto.Targa);
        Assert.Equal(capacita, auto.Capacita);
        Assert.Equal(costo, auto.CostoGiornaliero);
        // Lo stato iniziale deve essere Disponibile per default.
        Assert.Equal(StatoAuto.Disponibile, auto.Stato);
    }

    [Fact]
    public void Noleggia_DovrebbeCambiareStatoInNoleggiata()
    {
        // US1: Quando noleggio un'auto disponibile, il suo stato deve cambiare.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", 5, 50m);
        
        auto.Noleggia();
        
        Assert.Equal(StatoAuto.Noleggiata, auto.Stato);
    }

    [Fact]
    public void Noleggia_SeGiaNoleggiata_DovrebbeLanciareEccezione()
    {
        // US1: Non è possibile noleggiare un'auto già occupata.
        // In OOP, l'oggetto protegge il proprio invariante lanciando un'eccezione.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", 5, 50m);
        auto.Noleggia(); // Prima volta OK

        Assert.Throws<InvalidOperationException>(() => auto.Noleggia());
    }

    [Fact]
    public void Restituisci_DovrebbeRiportareStatoADisponibile()
    {
        // US2: Quando un'auto noleggiata viene restituita, torna disponibile.
        var auto = new Auto(Guid.NewGuid(), "AA123BB", 5, 50m);
        auto.Noleggia();
        
        auto.Restituisci();
        
        Assert.Equal(StatoAuto.Disponibile, auto.Stato);
    }
}