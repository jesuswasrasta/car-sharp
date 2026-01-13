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
        // Il punto di partenza del dominio è un valore immutabile vuoto.
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
        // La rimozione produce un nuovo stato senza alterare il valore originale.
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

    [Fact]
    public void RimozioneDaParcoVuoto_DovrebbeRestituireUnFallimento()
    {
        // Le operazioni fallibili devono essere modellate tramite tipi che forzano 
        // la gestione esplicita dell'errore (Result), invece di lanciare eccezioni.
        var parco = ParcoMezzi.Vuoto;
        var auto = new AutoDisponibile(Guid.NewGuid(), "TEST1", 5);

        var risultato = parco.RimuoviAuto(auto);

                Assert.False(risultato.IsSuccess);

                Assert.Equal("Auto non trovata nel parco mezzi", risultato.Error!.Message);

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
        var risultato = parco.NoleggiaBatch(batch);

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
        parco = parco.NoleggiaAuto(bersaglio.Id).Value!;

        var batch = autoList.Select(a => a.Id).ToList();

        var risultato = parco.NoleggiaBatch(batch);

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

        var risultato = parco.NoleggiaBatch(batch);

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

        var risultato = parco.NoleggiaPerCapacita(postiRichiesti.Get);

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

    [Property]
    public bool NoleggioPerIdECapacita_DovrebbeRispettareEntrambiIVincoli(Guid id, PositiveInt capacitaAuto, PositiveInt capacitaRichiesta)
    {
        // FR-006: Se chiedo ID + Capacità, entrambi devono essere soddisfatti.
        var auto = new AutoDisponibile(id, "TEST", capacitaAuto.Get);
        var parco = ParcoMezzi.Vuoto.AggiungiAuto(auto).Value!;
        var richiesta = new RichiestaNoleggio(capacitaRichiesta.Get, id);

        var risultato = parco.NoleggiaAuto(richiesta);

        if (capacitaAuto.Get >= capacitaRichiesta.Get)
            return risultato.IsSuccess && risultato.Value!.ConteggioDisponibili == 0;
        
        return !risultato.IsSuccess && risultato.Error!.Message.Contains("capacità sufficiente");
    }

    [Fact]
    public void NoleggiaBatchMisto_Successo_QuandoRichiesteSonoCompatibili()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var parco = ParcoMezzi.Vuoto
            .AggiungiAuto(new AutoDisponibile(id1, "AUTO1", 5)).Value!
            .AggiungiAuto(new AutoDisponibile(id2, "AUTO2", 2)).Value!;

        var richieste = new List<RichiestaNoleggio>
        {
            new RichiestaNoleggio(1, id2), // Prende AUTO2
            new RichiestaNoleggio(4)       // Prende AUTO1
        };

        var risultato = parco.NoleggiaBatch(richieste);

        Assert.True(risultato.IsSuccess);
        Assert.Equal(0, risultato.Value!.ConteggioDisponibili);
    }

    [Fact]
    public void NoleggiaBatchMisto_Fallimento_QuandoConflittoSuStessaAuto()
    {
        var id = Guid.NewGuid();
        var parco = ParcoMezzi.Vuoto.AggiungiAuto(new AutoDisponibile(id, "ONLY_ONE", 5)).Value!;

        var richieste = new List<RichiestaNoleggio>
        {
            new RichiestaNoleggio(4), // Prenderà l'unica auto disponibile
            new RichiestaNoleggio(1, id) // Fallirà perché l'auto è già stata presa
        };

        var risultato = parco.NoleggiaBatch(richieste);

        Assert.False(risultato.IsSuccess);
        // L'atomicità funzionale garantisce che il parco originale resti intatto.
    }

    [Property]
    public bool NoleggiaBatch_DovrebbeEssereAtomicoSuVincoliCapacita(PositiveInt n)
    {
        // Se un batch ha una richiesta non soddisfabile per capacità, 
        // l'intero risultato deve essere un errore e lo stato non deve cambiare.
        var parco = ParcoMezzi.Vuoto.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "SMALL", 2)).Value!;
        
        var richieste = new List<RichiestaNoleggio>
        {
            new RichiestaNoleggio(1), // Soddisfabile
            new RichiestaNoleggio(5)  // Non soddisfabile
        };

        var risultato = parco.NoleggiaBatch(richieste);

        return !risultato.IsSuccess;
    }
}

                               

                       

        