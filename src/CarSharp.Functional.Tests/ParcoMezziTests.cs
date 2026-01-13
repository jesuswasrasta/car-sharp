// ABOUTME: Suite di test per l'implementazione Funzionale di ParcoMezzi.
// Utilizziamo test basati su proprietà (FsCheck) per verificare gli invarianti su input casuali.

using CarSharp.Functional;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace CarSharp.Functional.Tests;

public class ParcoMezziTests
{
    [Fact]
    public void ParcoMezziVuoto_DovrebbeAvereZeroAuto()
    {
        // Assert
        Assert.Equal(0, ParcoMezzi.Vuoto.TotaleAuto);
    }

    [Property]
    public bool AggiuntaDiNAuto_DovrebbeRisultareInConteggioTotaleN(PositiveInt n)
    {
        // Arrange
        var parco = ParcoMezzi.Vuoto;
        var conteggio = n.Get;

        // Act
        for (int i = 0; i < conteggio; i++)
        {
            parco = parco.AggiungiAuto(new Auto()).Valore!;
        }

        // Assert
        return (parco.TotaleAuto == conteggio);
    }

    [Fact]
    public void GrandeVolume_DovrebbeEssereIstantaneo()
    {
        // Arrange
        var parco = ParcoMezzi.Vuoto;
        var grandeConteggio = 10_000;

        // Act
        for (int i = 0; i < grandeConteggio; i++)
        {
            parco = parco.AggiungiAuto(new Auto()).Valore!;
        }

        var watch = System.Diagnostics.Stopwatch.StartNew();
        var conteggio = parco.TotaleAuto;
        watch.Stop();

        // Assert
        Assert.Equal(grandeConteggio, conteggio);
        Assert.True(watch.ElapsedMilliseconds < 10, $"Il conteggio ha richiesto {watch.ElapsedMilliseconds}ms");
    }

    [Property]
    public bool RimozioneAuto_DovrebbeDecrementareIlConteggio_QuandoLAutoEsiste(PositiveInt n)
    {
        // Arrange
        var parco = ParcoMezzi.Vuoto;
        var autoList = new List<Auto>();
        for (int i = 0; i < n.Get; i++)
        {
            var auto = new Auto();
            autoList.Add(auto);
            parco = parco.AggiungiAuto(auto).Valore!;
        }

        var bersaglio = autoList[new Random().Next(autoList.Count)];

        // Act
        var risultato = parco.RimuoviAuto(bersaglio);

        // Assert
        return risultato.IsSuccess && risultato.Valore!.TotaleAuto == parco.TotaleAuto - 1;
    }

    [Fact]
    public void RimozioneDaParcoVuoto_DovrebbeRestituireUnFallimento()
    {
        // Arrange
        var parco = ParcoMezzi.Vuoto;
        var auto = new Auto();

        // Act
        var risultato = parco.RimuoviAuto(auto);

        // Assert
        Assert.False(risultato.IsSuccess);
        Assert.Equal("Auto non trovata nel parco mezzi", risultato.Errore!.Messaggio);
    }
}