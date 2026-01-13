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
        // Partiamo da un parco mezzi vuoto.
        var parco = ParcoMezzi.Vuoto;
        var conteggio = n.Get;

        // Act
        // Eseguiamo un 'fold' dell'aggiunta sul parco mezzi 'n' volte.
        // In FP, questa è una sequenza di trasformazioni, ognuna delle quali produce una nuova istanza di parco mezzi.
        for (int i = 0; i < conteggio; i++)
        {
            parco = parco.AggiungiAuto(new Auto());
        }

        // Assert
        // L'istanza finale del parco mezzi dovrebbe riflettere il numero totale di aggiunte.
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
            parco = parco.AggiungiAuto(new Auto());
        }

        // Misuriamo le prestazioni dell'accesso alla proprietà TotaleAuto.
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var conteggio = parco.TotaleAuto;
        watch.Stop();

        // Assert
        // In C# Funzionale, anche ImmutableList.Count è un'operazione O(1) 
        // poiché la collezione mantiene internamente il proprio conteggio.
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
            parco = parco.AggiungiAuto(auto);
        }

        // Scegliamo un'auto casuale dalla lista
        var bersaglio = autoList[new Random().Next(autoList.Count)];

        // Act
        var parcoAggiornato = parco.RimuoviAuto(bersaglio);

        // Assert
        return parcoAggiornato.TotaleAuto == parco.TotaleAuto - 1;
    }

    [Fact]
    public void RimozioneDaParcoVuoto_DovrebbeRestituireLoStessoParco()
    {
        // Arrange
        var parco = ParcoMezzi.Vuoto;
        var auto = new Auto();

        // Act
        var parcoAggiornato = parco.RimuoviAuto(auto);

        // Assert
        Assert.Same(parco, parcoAggiornato);
        Assert.Equal(0, parcoAggiornato.TotaleAuto);
    }
}