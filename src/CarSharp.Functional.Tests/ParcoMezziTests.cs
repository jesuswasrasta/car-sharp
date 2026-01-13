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
            parco = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), $"ABC{i}")).Value!;
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
        // La rimozione produce un nuovo stato senza alterare il valore originale.
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
        // In Type-Driven Design, lo stato è espresso dal tipo. 
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
        // Le operazioni fallibili devono essere modellate tramite tipi che forzano 
        // la gestione esplicita dell'errore (Result), invece di lanciare eccezioni.
        var parco = ParcoMezzi.Vuoto;
        var auto = new AutoDisponibile(Guid.NewGuid(), "TEST1");

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

                    var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}");

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

                                   var auto = new AutoDisponibile(Guid.NewGuid(), $"ABC{i}");

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

                                       var parco = ParcoMezzi.Vuoto.AggiungiAuto(new AutoDisponibile(autoId, "AA111AA")).Value!;

                                       

                                       var batch = new List<Guid> { autoId, autoId };

                               

                                       var risultato = parco.NoleggiaBatch(batch);

                               

                                       Assert.False(risultato.IsSuccess);

                                       Assert.Equal("Il batch contiene duplicati", risultato.Error!.Message);

                                   }

                               }

                               

                       

        