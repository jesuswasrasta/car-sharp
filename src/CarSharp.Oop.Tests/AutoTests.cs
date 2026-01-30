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
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), null!));
        
        // Targa vuota
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), ""));
        
        // ID vuoto
        Assert.Throws<ArgumentException>(() => new Auto(Guid.Empty, "AA123BB"));
    }

    [Fact]
    public void Costruttore_InizializzaCorrettamenteLeProprieta()
    {
        // Verifichiamo che i valori passati al costruttore siano correttamente
        // assegnati e accessibili tramite le proprietà pubbliche.
        var id = Guid.NewGuid();
        var targa = "AA123BB";
        
        var auto = new Auto(id, targa);
        
        Assert.Equal(id, auto.Id);
        Assert.Equal(targa, auto.Targa);
        // Lo stato iniziale deve essere Disponibile per default.
        Assert.Equal(StatoAuto.Disponibile, auto.Stato);
    }
}
