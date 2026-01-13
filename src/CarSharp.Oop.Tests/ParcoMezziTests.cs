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
        // L'aggiunta di un oggetto a una collezione deve incrementarne la dimensione.
        var parco = new ParcoMezzi();
        
        for (int i = 0; i < n; i++)
        {
            var auto = new Auto(Guid.NewGuid(), $"ABC{i}", StatoAuto.Disponibile, 5);
            parco.AggiungiAuto(auto);
        }

        Assert.Equal(n, parco.TotaleAuto);
    }

    [Fact]
    public void GrandeVolume_DovrebbeEssereIstantaneo()
    {
        // La gestione delle reference in OOP è estremamente efficiente.
        var parco = new ParcoMezzi();
        var grandeConteggio = 10_000;

        for (int i = 0; i < grandeConteggio; i++)
        {
            var auto = new Auto(Guid.NewGuid(), $"ABC{i}", StatoAuto.Disponibile, 5);
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
        // Rimuovere un oggetto esistente deve ridurne la presenza nell'aggregato.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "TEST1", StatoAuto.Disponibile, 5);
        parco.AggiungiAuto(auto);

        parco.RimuoviAuto(auto);

        Assert.Equal(0, parco.TotaleAuto);
    }

    [Fact]
    public void TotaleDisponibili_DovrebbeContareSoloLeAutoInStatoDisponibile()
    {
        // Il sistema deve essere in grado di filtrare la flotta in base allo stato mutabile degli oggetti.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile, 5);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", StatoAuto.Noleggiata, 5);
        var auto3 = new Auto(Guid.NewGuid(), "CC333CC", StatoAuto.Disponibile, 5);

        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);
        parco.AggiungiAuto(auto3);

        Assert.Equal(2, parco.TotaleDisponibili);
    }

    [Fact]
    public void NoleggiaBatch_ConAutoDisponibili_DovrebbeNoleggiarleTutte()
    {
        // Questo test verifica il caso d'uso principale del noleggio batch.
        // In OOP, ci aspettiamo che il parco mezzi coordini la mutazione dello stato
        // di più oggetti Auto contemporaneamente.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile, 5);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", StatoAuto.Disponibile, 5);
        
        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);

        var batch = new List<Guid> { auto1.Id, auto2.Id };

        parco.NoleggiaBatch(batch);

        // Verifichiamo che la mutazione sia avvenuta correttamente su tutti gli oggetti coinvolti.
        Assert.Equal(StatoAuto.Noleggiata, auto1.Stato);
        Assert.Equal(StatoAuto.Noleggiata, auto2.Stato);
        Assert.Equal(0, parco.TotaleDisponibili);
    }

    [Fact]
    public void NoleggiaBatch_ConAutoNonDisponibile_DovrebbeLanciareEccezioneENonModificareStato()
    {
        // Questo test verifica l'atomicità in caso di errore.
        // Se un'auto nel batch non è disponibile, nessuna deve essere noleggiata.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile, 5);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", StatoAuto.Noleggiata, 5); // Già noleggiata
        
        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);

        var batch = new List<Guid> { auto1.Id, auto2.Id };

        // Ci aspettiamo un'eccezione perché il batch non può essere soddisfatto interamente.
        Assert.Throws<InvalidOperationException>(() => parco.NoleggiaBatch(batch));

        // Fondamentale: l'auto1 deve essere rimasta DISPONIBILE (rollback in-memory).
        Assert.Equal(StatoAuto.Disponibile, auto1.Stato);
    }

    [Fact]
    public void NoleggiaBatch_ConDuplicati_DovrebbeLanciareEccezione()
    {
        // Un batch non deve contenere la stessa auto più volte. 
        // In OOP, validiamo questo vincolo prima di procedere.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile, 5);
        parco.AggiungiAuto(auto);

        var batch = new List<Guid> { auto.Id, auto.Id };

        Assert.Throws<ArgumentException>(() => parco.NoleggiaBatch(batch));
    }

    [Fact]
    public void NoleggiaPerCapacita_DovrebbeAssegnareLaPrimaAutoIdonea()
    {
        // Verifichiamo che il sistema scelga un'auto con capacità sufficiente.
        // Secondo la nostra chiarificazione, deve essere la prima inserita tra quelle valide.
        var parco = new ParcoMezzi();
        var autoPiccola = new Auto(Guid.NewGuid(), "SMALL", StatoAuto.Disponibile, 2);
        var autoMedia = new Auto(Guid.NewGuid(), "MEDIUM", StatoAuto.Disponibile, 5);
        var autoGrande = new Auto(Guid.NewGuid(), "LARGE", StatoAuto.Disponibile, 7);

        parco.AggiungiAuto(autoPiccola);
        parco.AggiungiAuto(autoMedia);
        parco.AggiungiAuto(autoGrande);

        // Richiediamo 4 posti. autoPiccola (2) non va bene, autoMedia (5) sì.
        var autoAssegnata = parco.Noleggia(4);

        Assert.Equal(autoMedia.Id, autoAssegnata.Id);
        Assert.Equal(StatoAuto.Noleggiata, autoMedia.Stato);
        Assert.Equal(2, parco.TotaleDisponibili);
    }

    [Fact]
    public void NoleggiaPerCapacita_DovrebbeLanciareInvalidOperationException_QuandoNessunaAutoIdonea()
    {
        // Se non ci sono auto con capacità sufficiente, il noleggio deve fallire.
        var parco = new ParcoMezzi();
        parco.AggiungiAuto(new Auto(Guid.NewGuid(), "SMALL", StatoAuto.Disponibile, 2));

        Assert.Throws<InvalidOperationException>(() => parco.Noleggia(5));
    }

    [Fact]
    public void NoleggiaPerIdECapacita_DovrebbeFallire_QuandoCapacitaInsufficiente()
    {
        // FR-006: Se chiedo un'auto specifica ma voglio più posti di quelli che ha, deve fallire.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "SMALL", StatoAuto.Disponibile, 2);
        parco.AggiungiAuto(auto);

        var richiesta = RichiestaNoleggio.PerId(auto.Id, 5);

        Assert.Throws<InvalidOperationException>(() => parco.NoleggiaBatch(new[] { richiesta }));
    }

    [Fact]
    public void NoleggiaBatchMisto_DovrebbeFallire_SeConflittoSuStessaAuto()
    {
        // Chiarificazione 2: L'ordine è sequenziale. Se una richiesta per capacità "ruba" l'auto
        // richiesta successivamente per ID nello stesso batch, il batch deve fallire.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "ONLY_ONE", StatoAuto.Disponibile, 5);
        parco.AggiungiAuto(auto);

        var richieste = new List<RichiestaNoleggio>
        {
            RichiestaNoleggio.PerCapacita(4), // Prenderà l'unica auto disponibile
            RichiestaNoleggio.PerId(auto.Id)    // Fallirà perché l'auto è già stata assegnata sopra
        };

        Assert.Throws<InvalidOperationException>(() => parco.NoleggiaBatch(richieste));
        Assert.Equal(StatoAuto.Disponibile, auto.Stato); // Atomicità: l'auto resta disponibile
    }

    [Fact]
    public void NoleggiaBatchMisto_DovrebbeAvereSuccesso_QuandoTutteRichiesteCompatibili()
    {
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AUTO1", StatoAuto.Disponibile, 5);
        var auto2 = new Auto(Guid.NewGuid(), "AUTO2", StatoAuto.Disponibile, 2);
        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);

        var richieste = new List<RichiestaNoleggio>
        {
            RichiestaNoleggio.PerId(auto2.Id), // Prende AUTO2
            RichiestaNoleggio.PerCapacita(4)    // Deve prendere AUTO1
        };

        parco.NoleggiaBatch(richieste);

        Assert.Equal(StatoAuto.Noleggiata, auto1.Stato);
        Assert.Equal(StatoAuto.Noleggiata, auto2.Stato);
    }
}
