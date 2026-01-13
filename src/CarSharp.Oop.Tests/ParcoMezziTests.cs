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
            var auto = new Auto(Guid.NewGuid(), $"ABC{i}", StatoAuto.Disponibile);
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
        // Rimuovere un oggetto esistente deve ridurne la presenza nell'aggregato.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "TEST1", StatoAuto.Disponibile);
        parco.AggiungiAuto(auto);

        parco.RimuoviAuto(auto);

        Assert.Equal(0, parco.TotaleAuto);
    }

    [Fact]
    public void TotaleDisponibili_DovrebbeContareSoloLeAutoInStatoDisponibile()
    {
        // Il sistema deve essere in grado di filtrare la flotta in base allo stato mutabile degli oggetti.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", StatoAuto.Noleggiata);
        var auto3 = new Auto(Guid.NewGuid(), "CC333CC", StatoAuto.Disponibile);

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
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", StatoAuto.Disponibile);
        
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
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", StatoAuto.Noleggiata); // Già noleggiata
        
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
        var auto = new Auto(Guid.NewGuid(), "AA111AA", StatoAuto.Disponibile);
        parco.AggiungiAuto(auto);

        var batch = new List<Guid> { auto.Id, auto.Id };

        Assert.Throws<ArgumentException>(() => parco.NoleggiaBatch(batch));
    }
}
