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
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5, 50m)).Value!;
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
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5, 50m)).Value!;
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
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5, 50m);
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
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "AA111AA", 5, 50m)).Value!
            .AggiungiAuto(new AutoNoleggiata(Guid.NewGuid(), "BB222BB", 5, 50m)).Value!
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "CC333CC", 5, 50m)).Value!;

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
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5, 50m);
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
            var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}", 5, 50m);
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
        var parco = ParcoMezzi.Vuoto.AggiungiAuto(new AutoDisponibile(autoId, "AA111AA", 5, 50m)).Value!;

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
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "SMALL", 2, 20m)).Value!
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "MEDIUM", 5, 50m)).Value!
            .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "LARGE", 7, 70m)).Value!;

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

    [Property]
    public bool NoleggiaBatch_DovrebbeEssereAtomicoSuVincoliCapacita(PositiveInt n)
    {
        // Se un batch ha una richiesta non soddisfabile per capacità,
        // l'intero risultato deve essere un errore e lo stato non deve cambiare.
        var parco = ParcoMezzi.Vuoto.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "SMALL", 2, 20m)).Value!;

        var richieste = new List<RichiestaNoleggio>
        {
            new RichiestaNoleggio("CLIENTE", 1), // Soddisfabile
            new RichiestaNoleggio("CLIENTE", 5)  // Non soddisfabile
        };

        var risultato = parco.NoleggiaBatch(richieste);

        return !risultato.IsSuccess;
    }

    [Fact]
    public void NoleggiaPerCapacita_ConPiuAutoDisponibili_DovrebbeSelezionareQuellaPiuPiccola()
    {
        // In FP, verifichiamo che l'algoritmo Best Fit selezioni sempre l'auto
        // con la capacità minima sufficiente tra quelle disponibili.
        var parco = ParcoMezzi.Vuoto;
        var auto4Posti = new AutoDisponibile(Guid.NewGuid(), "SMALL", 4, 40m);
        var auto7Posti = new AutoDisponibile(Guid.NewGuid(), "LARGE", 7, 70m);

        parco = parco.AggiungiAuto(auto7Posti).Value!;
        parco = parco.AggiungiAuto(auto4Posti).Value!;

        var risultato = parco.NoleggiaPerCapacita(4, "CLIENTE");

        // L'algoritmo deve scegliere l'auto da 4 posti (minima capacità sufficiente).
        Assert.True(risultato.IsSuccess);
        var parcoAggiornato = risultato.Value!;

        // L'auto da 4 posti deve essere stata trasformata in AutoNoleggiata.
        Assert.Contains(parcoAggiornato.auto, a => a.Id == auto4Posti.Id && a is AutoNoleggiata);
        Assert.Contains(parcoAggiornato.auto, a => a.Id == auto7Posti.Id && a is AutoDisponibile);
    }

    [Property]
    public bool NoleggiaPerCapacita_SempreSelezionaAutoConCapacitaMinimaValida(PositiveInt postiRichiesti)
    {
        // Property: dato un parco con N auto disponibili e una richiesta di capacità,
        // l'auto selezionata deve SEMPRE avere la capacità minima tra tutte quelle >= postiRichiesti.
        var posti = Math.Min(postiRichiesti.Get, 10); 

        var parco = ParcoMezzi.Vuoto;
        var autoCreate = new List<IAuto>();
        for (int i = 1; i <= 5; i++)
        {
            var auto = new AutoDisponibile(Guid.NewGuid(), $"CAR{i * 2}", i * 2, i * 20m);
            autoCreate.Add(auto);
            parco = parco.AggiungiAuto(auto).Value!;
        }

        var risultato = parco.NoleggiaPerCapacita(posti, "CLIENTE");

        if (!risultato.IsSuccess)
            return autoCreate.All(a => a.Capacita < posti);

        var parcoAggiornato = risultato.Value!;
        var autoNoleggiata = parcoAggiornato.auto.OfType<AutoNoleggiata>().First();

        var capacitaValide = autoCreate
            .Where(a => a.Capacita >= posti)
            .Select(a => a.Capacita)
            .ToList();

        var capacitaMinima = capacitaValide.Min();
        return autoNoleggiata.Capacita == capacitaMinima;
    }

    [Fact]
    public void NoleggiaBatch_ConRichiesteMultiple_DovrebbeOttimizzareOgniSelezione()
    {
        // Verifichiamo che il batch processing applichi Best Fit a OGNI richiesta.
        var parco = ParcoMezzi.Vuoto;
        var auto2Posti = new AutoDisponibile(Guid.NewGuid(), "CAR2", 2, 20m);
        var auto4Posti = new AutoDisponibile(Guid.NewGuid(), "CAR4", 4, 40m);
        var auto7Posti = new AutoDisponibile(Guid.NewGuid(), "CAR7", 7, 70m);

        parco = parco.AggiungiAuto(auto7Posti).Value!;
        parco = parco.AggiungiAuto(auto4Posti).Value!;
        parco = parco.AggiungiAuto(auto2Posti).Value!;

        var richieste = new List<RichiestaNoleggio>
        {
            new RichiestaNoleggio("CLIENTE", 2), // Deve selezionare CAR2
            new RichiestaNoleggio("CLIENTE", 4)  // Deve selezionare CAR4
        };

        var risultato = parco.NoleggiaBatch(richieste);

        Assert.True(risultato.IsSuccess);
        var parcoAggiornato = risultato.Value!;

        // Verifichiamo che siano state noleggiate le auto ottimali.
        Assert.Contains(parcoAggiornato.auto, a => a.Id == auto2Posti.Id && a is AutoNoleggiata);
        Assert.Contains(parcoAggiornato.auto, a => a.Id == auto4Posti.Id && a is AutoNoleggiata);
        Assert.Contains(parcoAggiornato.auto, a => a.Id == auto7Posti.Id && a is AutoDisponibile);
    }

    // ========== TEST FASE 5 - US2: DETERMINISMO ==========

    [Fact]
    public void NoleggiaPerCapacita_ConAutoStessaCapacita_DovrebbeSelezionareLaPrimaInserita()
    {
        // Nel paradigma FP, il determinismo è ancora più critico perché la prevedibilità
        // è un pilastro fondamentale. Dato lo stesso input (parco con auto identiche),
        // dobbiamo sempre ottenere lo stesso output (prima auto inserita selezionata).
        // OrderBy in LINQ è stabile: preserva l'ordine originale a parità di chiave.
        var parco = ParcoMezzi.Vuoto;
        var primaAuto = new AutoDisponibile(Guid.NewGuid(), "FIRST5", 5, 50m);
        var secondaAuto = new AutoDisponibile(Guid.NewGuid(), "SECOND5", 5, 50m);
        var terzaAuto = new AutoDisponibile(Guid.NewGuid(), "THIRD5", 5, 50m);

        parco = parco.AggiungiAuto(primaAuto).Value!;
        parco = parco.AggiungiAuto(secondaAuto).Value!;
        parco = parco.AggiungiAuto(terzaAuto).Value!;

        var risultato = parco.NoleggiaPerCapacita(5, "CLIENTE");

        Assert.True(risultato.IsSuccess);
        var parcoAggiornato = risultato.Value!;
        Assert.Contains(parcoAggiornato.auto, a => a.Id == primaAuto.Id && a is AutoNoleggiata);
    }

    [Property]
    public bool NoleggiaPerCapacita_Determinismo(PositiveInt capacita)
    {
        // Property: Per qualsiasi capacità N, se inseriamo K auto identiche,
        // il sistema deve sempre selezionare la prima inserita.
        var cap = Math.Min(capacita.Get, 10);
        var parco = ParcoMezzi.Vuoto;
        var primaAuto = new AutoDisponibile(Guid.NewGuid(), "AUTO1", cap, 50m);
        var secondaAuto = new AutoDisponibile(Guid.NewGuid(), "AUTO2", cap, 50m);

        parco = parco.AggiungiAuto(primaAuto).Value!;
        parco = parco.AggiungiAuto(secondaAuto).Value!;

        var risultato = parco.NoleggiaPerCapacita(cap, "CLIENTE");

        return risultato.IsSuccess && 
               risultato.Value!.auto.OfType<AutoNoleggiata>().First().Id == primaAuto.Id;
    }

    // ========== TEST FASE 6 - US1: NOLEGGIO CON COSTO ==========

    [Property]
    public bool NoleggiaConCosto_DovrebbeRestituireRisultatoConCostoCorretto(
        Guid id, string targa, PositiveInt capacitaWrapper, PositiveInt costoWrapper)
    {
        // Property: Per ogni auto con costo X, il noleggio deve restituire X.
        var capacita = Math.Max(capacitaWrapper.Get, 1);
        var costoGiornaliero = (decimal)costoWrapper.Get;

        var auto = new AutoDisponibile(id, targa ?? "TEST", capacita, costoGiornaliero);
        var parco = ParcoMezzi.Vuoto.AggiungiAuto(auto).Value!;

        var risultato = parco.NoleggiaConCosto(new RichiestaNoleggio("CLIENTE", 1));

        if (!risultato.IsSuccess)
            return false;

        return risultato.Value!.Costo == costoGiornaliero &&
               risultato.Value.Auto.CostoGiornaliero == costoGiornaliero;
    }

    [Fact]
    public void PrenotaBatch_DovrebbeRestituireRisultatoBatch()
    {
        // US3: Il batch di noleggi deve restituire un RisultatoBatch contenente
        // la lista dei noleggi, il costo totale e il parco aggiornato.
        var parco = ParcoMezzi.Vuoto;
        var auto1 = new AutoDisponibile(Guid.NewGuid(), "AUTO1", 4, 40m);
        var auto2 = new AutoDisponibile(Guid.NewGuid(), "AUTO2", 6, 60m);
        parco = parco.AggiungiAuto(auto1).Value!;
        parco = parco.AggiungiAuto(auto2).Value!;

        var risultato = parco.PrenotaBatch(new[] {
            new RichiestaNoleggio("CLIENTE", 3),
            new RichiestaNoleggio("CLIENTE", 5)
        }, 0m);

        Assert.True(risultato.IsSuccess);
        Assert.Equal(2, risultato.Value!.Noleggi.Count);
        Assert.Equal(40m, risultato.Value.Noleggi[0].Costo);
        Assert.Equal(60m, risultato.Value.Noleggi[1].Costo);
        Assert.Equal(100m, risultato.Value.TotaleGenerale);
    }
}