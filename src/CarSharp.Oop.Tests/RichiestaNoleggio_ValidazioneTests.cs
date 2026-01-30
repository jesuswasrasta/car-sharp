// ABOUTME: Test di validazione per la classe RichiestaNoleggio (OOP).
// Verifichiamo che i parametri di input siano validati immediatamente per evitare stati inconsistenti.

using CarSharp.Oop;
using Xunit;

namespace CarSharp.Oop.Tests;

public class RichiestaNoleggio_ValidazioneTests
{
    [Fact]
    public void Costruttore_DovrebbeLanciareArgumentException_QuandoCapacitaEZeroONegativa()
    {
        // Fail-fast: la capacità richiesta deve essere un numero positivo.
        Assert.Throws<ArgumentException>(() => RichiestaNoleggio.PerCapacita(0));
        Assert.Throws<ArgumentException>(() => RichiestaNoleggio.PerCapacita(-1));
    }
}
