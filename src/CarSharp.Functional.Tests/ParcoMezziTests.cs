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
        // Nel paradigma funzionale, non iniziamo con un oggetto vuoto da 'riempire', 
        // ma con un valore costante che rappresenta lo stato 'Vuoto'. Questo approccio 
        // elimina la necessità di costruttori complessi e garantisce che lo stato 
        // iniziale sia una costante immutabile e sicura da condividere tra diverse 
        // parti del sistema senza rischi di side-effect.
        Assert.Equal(0, ParcoMezzi.Vuoto.TotaleAuto);
    }

    [Property]
    public bool AggiuntaDiNAuto_DovrebbeRisultareInConteggioTotaleN(PositiveInt n)
    {
        // Ogni operazione di aggiunta produce un nuovo valore del ParcoMezzi che riflette il cambiamento.
        var parco = ParcoMezzi.Vuoto;
        var conteggio = n.Get;

        for (int i = 0; i < conteggio; i++)
        {
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}"));
        }

        return (parco.TotaleAuto == conteggio);
    }

    [Fact]
    public void GrandeVolume_DovrebbeEssereIstantaneo()
    {
        // L'uso di strutture dati persistenti (Immutable Collections) garantisce performance 
        // comparabili alla mutazione tramite la condivisione strutturale (structural sharing).
        var parco = ParcoMezzi.Vuoto;
        var grandeConteggio = 10_000;

        for (int i = 0; i < grandeConteggio; i++)
        {
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}"));
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
        // Nel mondo funzionale, la rimozione è una trasformazione che 'proietta' 
        // un nuovo stato del parco mezzi che non contiene più l'elemento specificato. 
        // Verifichiamo che per qualsiasi parco di dimensione N, la rimozione di 
        // un elemento esistente restituisca sempre un nuovo parco di dimensione N-1.
        var parco = ParcoMezzi.Vuoto;
        var autoList = new List<IAuto>();
        for (int i = 0; i < n.Get; i++)
        {
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}");
            autoList.Add(auto);
            parco = parco.AggiungiAuto(auto);
        }

        var bersaglio = autoList[new Random().Next(autoList.Count)];

        var nuovoParco = parco.RimuoviAuto(bersaglio);

        return nuovoParco.TotaleAuto == parco.TotaleAuto - 1;
    }

    [Fact]
    public void ConteggioDisponibili_DovrebbeContareSoloLeIstanzeDiAutoDisponibile()
    {
        // In Type-Driven Design, lo stato è espresso dal tipo. 
        // Filtrare per tipo è l'equivalente funzionale del filtraggio per proprietà.
        var parco = ParcoMezzi.Vuoto
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "AA111AA"))
            .AggiungiAuto(new AutoNoleggiata(Guid.NewGuid(), "BB222BB"))
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "CC333CC"));

        Assert.Equal(2, parco.ConteggioDisponibili);
    }
}
