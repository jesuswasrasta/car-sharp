// ABOUTME: Test per la gestione del ParcoMezzi nel paradigma OOP.
// In OOP, il parco mezzi è un aggregato che gestisce una collezione di oggetti Auto mutabili.

using CarSharp.Oop;
using Xunit;

namespace CarSharp.Oop.Tests;

public class ParcoMezziTests
{
    [Fact]
    public void ParcoMezziVuoto_DovrebbeAvereZeroAuto()
    {
        var parco = new ParcoMezzi();
        Assert.Equal(0, parco.TotaleAuto);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void AggiuntaDiNAuto_DovrebbeRisultareInConteggioTotaleN(int n)
    {
        // Perché: L'aggiunta di un oggetto a una collezione deve incrementarne la dimensione.
        var parco = new ParcoMezzi();
        
        for (int i = 0; i < n; i++)
        {
            var auto = new Auto(Guid.NewGuid(), $"ABC{i}", StatoAuto.Disponibile);
            parco.AggiungiAuto(auto);
        }

        Assert.Equal(n, parco.TotaleAuto);
    }

    [Fact]
    public void GrandeVolume_DovrebbeEssereIstantaneo()
    {
        // Perché: La gestione delle reference in OOP è estremamente efficiente.
        var parco = new ParcoMezzi();
        var grandeConteggio = 10_000;

        for (int i = 0; i < grandeConteggio; i++)
        {
            var auto = new Auto(Guid.NewGuid(), $"ABC{i}", StatoAuto.Disponibile);
            parco.AggiungiAuto(auto);
        }

        var watch = System.Diagnostics.Stopwatch.StartNew();
        var conteggio = parco.TotaleAuto;
        watch.Stop();

        Assert.Equal(grandeConteggio, conteggio);
        Assert.True(watch.ElapsedMilliseconds < 10, $"Il conteggio ha richiesto {watch.ElapsedMilliseconds}ms");
    }

    [Fact]
    public void RimozioneAuto_DovrebbeDecrementareIlConteggio_QuandoLAutoEsiste()
    {
        // Perché: Rimuovere un oggetto esistente deve ridurne la presenza nell'aggregato.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "TEST1", StatoAuto.Disponibile);
        parco.AggiungiAuto(auto);

        parco.RimuoviAuto(auto);

        Assert.Equal(0, parco.TotaleAuto);
    }
}
