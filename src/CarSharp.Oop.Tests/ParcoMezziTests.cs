// ABOUTME: Suite di test per l'implementazione OOP di ParcoMezzi.
// Utilizziamo test basati su esempi (Facts) per verificare le transizioni di stato specifiche.

using CarSharp.Oop;

namespace CarSharp.Oop.Tests;

public class ParcoMezziTests
{
    [Fact]
    public void NuovoParcoMezzi_DovrebbeEssereVuoto()
    {
        // Arrange & Act
        var parco = new ParcoMezzi();

        // Assert
        // Verifichiamo che un parco mezzi appena creato abbia un conteggio pari a zero.
        Assert.Equal(0, parco.TotaleAuto);
    }

    [Fact]
    public void AggiungiAuto_DovrebbeIncrementareIlConteggioTotale()
    {
        // Arrange
        var parco = new ParcoMezzi();
        var auto = new Auto();

        // Act
        // Mutiamo lo stato del parco mezzi aggiungendo un'auto.
        parco.AggiungiAuto(auto);

        // Assert
        // Il conteggio dovrebbe essere aumentato a 1.
        Assert.Equal(1, parco.TotaleAuto);
    }

    [Fact]
    public void RimuoviAuto_DovrebbeDecrementareIlConteggioTotale_QuandoLAutoEsiste()
    {
        // Arrange
        var parco = new ParcoMezzi();
        var auto = new Auto();
        parco.AggiungiAuto(auto);

        // Act
        // Rimuoviamo l'istanza specifica dell'auto.
        bool rimosso = parco.RimuoviAuto(auto);

        // Assert
        Assert.True(rimosso);
        Assert.Equal(0, parco.TotaleAuto);
    }

    [Fact]
    public void RimuoviAuto_DovrebbeRestituireFalse_QuandoLAutoNonEsiste()
    {
        // Arrange
        var parco = new ParcoMezzi();
        var auto = new Auto();

        // Act
        bool rimosso = parco.RimuoviAuto(auto);

        // Assert
        Assert.False(rimosso);
        Assert.Equal(0, parco.TotaleAuto);
    }

    [Fact]
    public void TotaleAuto_DovrebbeEssereIstantaneo_IndipendentementeDalVolume()
    {
        // Arrange
        var parco = new ParcoMezzi();
        for (int i = 0; i < 10_000; i++) parco.AggiungiAuto(new Auto());

        // Act
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var conteggio = parco.TotaleAuto;
        watch.Stop();

        // Assert
        // In OOP, List.Count è un'operazione O(1) perché il conteggio è 
        // memorizzato nella cache e aggiornato durante la mutazione.
        Assert.Equal(10_000, conteggio);
        Assert.True(watch.ElapsedMilliseconds < 10, $"Il conteggio ha richiesto {watch.ElapsedMilliseconds}ms");
    }
}