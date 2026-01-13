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
        Assert.Equal(0, ParcoMezzi.Vuoto.TotaleAuto);
    }

    [Property]
    public bool AggiuntaDiNAuto_DovrebbeRisultareInConteggioTotaleN(PositiveInt n)
    {
        var parco = ParcoMezzi.Vuoto;
        var conteggio = n.Get;

        for (int i = 0; i < conteggio; i++)
        {
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}")).Value!;
        }

        return (parco.TotaleAuto == conteggio);
    }

    [Fact]
    public void GrandeVolume_DovrebbeEssereIstantaneo()
    {
        var parco = ParcoMezzi.Vuoto;
        var grandeConteggio = 10_000;

        for (int i = 0; i < grandeConteggio; i++)
        {
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}")).Value!;
        }

        var watch = System.Diagnostics.Stopwatch.StartNew();
        var conteggio = parco.TotaleAuto;
        watch.Stop();

        Assert.Equal(grandeConteggio, conteggio);
        Assert.True(watch.ElapsedMilliseconds < 10, $"Il conteggio ha richiesto {watch.ElapsedMilliseconds}ms");
    }

    [Property]
    public bool RimozioneAuto_DovrebbeDecrementareIlConteggio_QuandoLAutoEsiste(PositiveInt n)
    {
        var parco = ParcoMezzi.Vuoto;
        var autoList = new List<IAuto>();
        for (int i = 0; i < n.Get; i++)
        {
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}");
            autoList.Add(auto);
            parco = parco.AggiungiAuto(auto).Value!;
        }

        var bersaglio = autoList[new Random().Next(autoList.Count)];

        var risultato = parco.RimuoviAuto(bersaglio);

        return risultato.IsSuccess && risultato.Value!.TotaleAuto == parco.TotaleAuto - 1;
    }

    [Fact]
    public void ConteggioDisponibili_DovrebbeContareSoloLeIstanzeDiAutoDisponibile()
    {
        // Perché: In Type-Driven Design, lo stato è espresso dal tipo. 
        // Filtrare per tipo è l'equivalente funzionale del filtraggio per proprietà.
        var parco = ParcoMezzi.Vuoto
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "AA111AA")).Value!
            .AggiungiAuto(new AutoNoleggiata(Guid.NewGuid(), "BB222BB")).Value!
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "CC333CC")).Value!;

        Assert.Equal(2, parco.ConteggioDisponibili);
    }

    [Fact]
    public void RimozioneDaParcoVuoto_DovrebbeRestituireUnFallimento()
    {
        var parco = ParcoMezzi.Vuoto;
        var auto = new AutoDisponibile(Guid.NewGuid(), "TEST1");

        var risultato = parco.RimuoviAuto(auto);

        Assert.False(risultato.IsSuccess);
        Assert.Equal("Auto non trovata nel parco mezzi", risultato.Error!.Message);
    }
}