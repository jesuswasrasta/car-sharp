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
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5)).Value!;
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
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5)).Value!;
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
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5);
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
        // In Type-Driven Design, lo stato è espresso dal tipo. 
        // Filtrare per tipo è l'equivalente funzionale del filtraggio per proprietà.
        var parco = ParcoMezzi.Vuoto
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "AA111AA", 5)).Value!
            .AggiungiAuto(new AutoNoleggiata(Guid.NewGuid(), "BB222BB", 5)).Value!
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "CC333CC", 5)).Value!;

        Assert.Equal(2, parco.ConteggioDisponibili);
    }

    [Property]
    public bool NoleggiaBatch_ConAutoDisponibili_DovrebbeRitornareNuovoStato(PositiveInt n)
    {
        // In FP, il noleggio batch è una funzione di aggregazione che trasforma 
        // l'intero parco mezzi. Il risultato deve riflettere il nuovo stato di tutte le auto.
        var conteggio = n.Get;
        var parco = ParcoMezzi.Vuoto;
        var autoList = new List<IAuto>();

        for (int i = 0; i < conteggio; i++)
        {
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5);
            autoList.Add(auto);
            parco = parco.AggiungiAuto(auto).Value!;
        }

        var batch = autoList.Select(a => a.Id).ToList();

        // NoleggiaBatch restituisce un nuovo valore del parco.
        var risultato = parco.NoleggiaBatch(batch, "CLIENTE");

        return risultato.IsSuccess &&
               risultato.Value!.ConteggioDisponibili == 0 &&
               risultato.Value.TotaleAuto == conteggio;
    }

    [Property]
    public bool NoleggiaBatch_ConAutoNonDisponibile_DovrebbeRitornareErrore(PositiveInt n)
    {
        // Questo test verifica l'atomicità funzionale: se una delle auto è già noleggiata,
        // l'intera operazione deve fallire e restituire l'errore appropriato.
        var conteggio = n.Get;
        var parco = ParcoMezzi.Vuoto;
        var autoList = new List<IAuto>();

        for (int i = 0; i < conteggio; i++)
        {
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5);
            autoList.Add(auto);
            parco = parco.AggiungiAuto(auto).Value!;
        }

        // Rendiamo una delle auto già noleggiata prima del batch.
        var bersaglio = autoList[new Random().Next(autoList.Count)];
        parco = parco.NoleggiaAuto(bersaglio.Id, "CLIENTE").Value!;

        var batch = autoList.Select(a => a.Id).ToList();

        var risultato = parco.NoleggiaBatch(batch, "CLIENTE");

        // Il batch deve fallire e il parco risultante (se ignorassimo il fallimento) 
        // non deve essere stato creato.
        return !risultato.IsSuccess &&
               risultato.Error!.Message == "Auto già noleggiata o in stato non valido";
    }

    [Fact]
    public void NoleggiaBatch_ConDuplicati_DovrebbeRitornareErrore()
    {
        // Anche in FP, inviare lo stesso ID più volte nel batch è considerato un errore.
        var autoId = Guid.NewGuid();
        var parco = ParcoMezzi.Vuoto.AggiungiAuto(new AutoDisponibile(autoId, "AA111AA", 5)).Value!;

        var batch = new List<Guid> { autoId, autoId };

        var risultato = parco.NoleggiaBatch(batch, "CLIENTE");

        Assert.False(risultato.IsSuccess);
        Assert.Equal("Il batch contiene duplicati", risultato.Error!.Message);
    }

    [Property]
    public bool NoleggiaPerCapacita_DovrebbeSempreRispettareIlMinimo(PositiveInt postiRichiesti)
    {
        // Proprietà: Un noleggio per capacità deve restituire un'auto idonea O un errore.
        // Se ha successo, la capacità dell'auto deve essere >= ai posti richiesti.
        var parco = ParcoMezzi.Vuoto
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "SMALL", 2)).Value!
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "MEDIUM", 5)).Value!
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "LARGE", 7)).Value!;

        var risultato = parco.NoleggiaPerCapacita(postiRichiesti.Get, "CLIENTE");

        if (risultato.IsSuccess)
        {
            // Verifichiamo che l'auto trovata e spostata in AutoNoleggiata abbia capacità sufficiente.
            var idAutoNoleggiata = parco.auto.Except(risultato.Value!.auto).OfType<AutoDisponibile>().First().Id;
            var autoNoleggiata = risultato.Value.auto.OfType<AutoNoleggiata>().First(a => a.Id == idAutoNoleggiata);
            
            return autoNoleggiata.Capacita >= postiRichiesti.Get;
        }

        // Se fallisce, non devono esserci auto disponibili con capacità sufficiente.
        return !parco.auto.OfType<AutoDisponibile>().Any(a => a.Capacita >= postiRichiesti.Get);
    }
}
