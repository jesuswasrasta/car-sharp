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
        // Il punto di partenza del nostro dominio è la creazione di un aggregato 'ParcoMezzi'. 
        // Inizialmente, questo aggregato non contiene alcun riferimento ad oggetti 'Auto', 
        // rappresentando correttamente una flotta vuota. In OOP, l'inizializzazione dello stato 
        // interno è fondamentale per garantire l'integrità dell'oggetto fin dalla sua nascita.
        var parco = new ParcoMezzi();
        Assert.Equal(0, parco.TotaleAuto);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void AggiuntaDiNAuto_DovrebbeRisultareInConteggioTotaleN(int n)
    {
        // L'operazione di aggiunta dimostra la natura mutabile del nostro aggregato. 
        // Quando aggiungiamo un'auto, non creiamo un nuovo parco, ma modifichiamo 
        // internamente la collezione dell'oggetto esistente. Questo riflette il modello 
        // mentale OOP dove gli oggetti hanno un'identità stabile che persiste nel tempo 
        // nonostante i cambiamenti del loro stato interno.
        var parco = new ParcoMezzi();
        
        for (int i = 0; i < n; i++)
        {
            var auto = new Auto(Guid.NewGuid(), $"ABC{i}", 5, 50m);
            parco.AggiungiAuto(auto);
        }

        Assert.Equal(n, parco.TotaleAuto);
    }

    [Fact]
    public void RimozioneAuto_DovrebbeDecrementareIlConteggio_QuandoLAutoEsiste()
    {
        // La rimozione si basa sull'uguaglianza per riferimento. Per rimuovere un'auto, 
        // dobbiamo passare all'aggregato esattamente lo stesso oggetto (stessa identità) 
        // che era stato precedentemente aggiunto. Questo garantisce che stiamo operando 
        // sull'asset corretto all'interno della flotta gestita.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "TEST1", 5, 50m);
        parco.AggiungiAuto(auto);

        parco.RimuoviAuto(auto);

        Assert.Equal(0, parco.TotaleAuto);
    }

    [Fact]
    public void TotaleDisponibili_DovrebbeContareSoloLeAutoInStatoDisponibile()
    {
        // Il sistema deve essere in grado di filtrare la flotta in base allo stato mutabile degli oggetti.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", 5, 50m);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", 5, 50m);
        var auto3 = new Auto(Guid.NewGuid(), "CC333CC", 5, 50m);

        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);
        parco.AggiungiAuto(auto3);

        auto2.Noleggia();

        Assert.Equal(2, parco.TotaleDisponibili);
    }

    [Fact]
    public void NoleggiaBatch_ConAutoDisponibili_DovrebbeNoleggiarleTutte()
    {
        // Questo test verifica l'caso d'uso principale del noleggio batch.
        // In OOP, ci aspettiamo che il parco mezzi coordini la mutazione dello stato
        // di più oggetti Auto contemporaneamente.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", 5, 50m);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", 5, 50m);
        
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
        var auto1 = new Auto(Guid.NewGuid(), "AA111AA", 5, 50m);
        var auto2 = new Auto(Guid.NewGuid(), "BB222BB", 5, 50m);
        
        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);
        
        auto2.Noleggia(); // Già noleggiata

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
        var auto = new Auto(Guid.NewGuid(), "AA111AA", 5, 50m);
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
        var autoPiccola = new Auto(Guid.NewGuid(), "SMALL", 2, 20m);
        var autoMedia = new Auto(Guid.NewGuid(), "MEDIUM", 5, 50m);
        var autoGrande = new Auto(Guid.NewGuid(), "LARGE", 7, 70m);

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
        parco.AggiungiAuto(new Auto(Guid.NewGuid(), "SMALL", 2, 20m));

        Assert.Throws<InvalidOperationException>(() => parco.Noleggia(5));
    }

    [Fact]
    public void NoleggiaPerIdECapacita_DovrebbeFallire_QuandoCapacitaInsufficiente()
    {
        // FR-006: Se chiedo un'auto specifica ma voglio più posti di quelli che ha, deve fallire.
        var parco = new ParcoMezzi();
        var auto = new Auto(Guid.NewGuid(), "SMALL", 2, 20m);
        parco.AggiungiAuto(auto);

        var richiesta = RichiestaNoleggio.PerId(auto.Id, 5);

        Assert.Throws<InvalidOperationException>(() => parco.NoleggiaBatch(new[] { richiesta }));
    }

    [Fact]
    public void NoleggiaBatchMisto_DovrebbeAvereSuccesso_QuandoTutteRichiesteCompatibili()
    {
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AUTO1", 5, 50m);
        var auto2 = new Auto(Guid.NewGuid(), "AUTO2", 2, 20m);
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

    [Fact]
    public void Noleggia_ConBestFit_DovrebbeSelezionareAutoConCapacitaMinimaSufficiente()
    {
        // US1: Se più auto soddisfano la richiesta, il sistema deve scegliere la più piccola.
        // Questo ottimizza l'uso della flotta lasciando i mezzi grandi per i gruppi numerosi.
        var parco = new ParcoMezzi();
        var autoPiccola = new Auto(Guid.NewGuid(), "AA111AA", 4, 40m);
        var autoMedia = new Auto(Guid.NewGuid(), "BB222BB", 5, 50m);
        var autoGrande = new Auto(Guid.NewGuid(), "CC333CC", 7, 70m);

        // Aggiungiamo prima le auto grandi. Senza Best Fit, il sistema sceglierebbe la prima idonea (7).
        parco.AggiungiAuto(autoGrande);
        parco.AggiungiAuto(autoMedia);
        parco.AggiungiAuto(autoPiccola);

        // Richiesta per 4 posti.
        // Best Fit deve scegliere autoPiccola (4) anche se è stata inserita per ultima.
        var autoScelta = parco.Noleggia(4);

        Assert.Equal(autoPiccola.Id, autoScelta.Id);
    }

    [Fact]
    public void Noleggia_ConParitaCapacita_DovrebbeEssereDeterministico()
    {
        // US2: In caso di più auto con la stessa capacità ottimale, 
        // il sistema deve scegliere costantemente la prima inserita.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AUTO1", 5, 50m);
        var auto2 = new Auto(Guid.NewGuid(), "AUTO2", 5, 50m);
        var auto3 = new Auto(Guid.NewGuid(), "AUTO3", 5, 50m);

        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);
        parco.AggiungiAuto(auto3);

        // La prima auto (auto1) deve essere sempre quella scelta.
        var autoScelta = parco.Noleggia(5);

        Assert.Equal(auto1.Id, autoScelta.Id);
    }

    [Fact]
    public void NoleggiaBatch_ConRichiesteMultiple_DovrebbeOttimizzareOgniAssegnazione()
    {
        // US3: Anche in un batch, ogni richiesta deve essere ottimizzata singolarmente.
        var parco = new ParcoMezzi();
        var auto2Posti = new Auto(Guid.NewGuid(), "CAR2", 2, 20m);
        var auto4Posti = new Auto(Guid.NewGuid(), "CAR4", 4, 40m);
        var auto5Posti = new Auto(Guid.NewGuid(), "CAR5", 5, 50m);
        var auto7Posti = new Auto(Guid.NewGuid(), "CAR7", 7, 70m);

        parco.AggiungiAuto(auto7Posti);
        parco.AggiungiAuto(auto2Posti);
        parco.AggiungiAuto(auto5Posti);
        parco.AggiungiAuto(auto4Posti);

        var richieste = new List<RichiestaNoleggio>
        {
            RichiestaNoleggio.PerCapacita(2), // Deve prendere CAR2
            RichiestaNoleggio.PerCapacita(4)  // Deve prendere CAR4, non CAR5 o CAR7
        };

        parco.NoleggiaBatch(richieste);

        Assert.Equal(StatoAuto.Noleggiata, auto2Posti.Stato);
        Assert.Equal(StatoAuto.Noleggiata, auto4Posti.Stato);
        Assert.Equal(StatoAuto.Disponibile, auto5Posti.Stato);
        Assert.Equal(StatoAuto.Disponibile, auto7Posti.Stato);
    }

    [Fact]
    public void NoleggiaConCosto_DovrebbeRestituireRisultatoConCostoCorretto()
    {
        // US1: Quando viene noleggiata un'auto, il sistema deve restituire
        // il costo giornaliero dell'operazione insieme all'auto noleggiata.
        var parco = new ParcoMezzi();
        var costoGiornaliero = 75.50m;
        var auto = new Auto(Guid.NewGuid(), "PRICED", 5, costoGiornaliero);
        parco.AggiungiAuto(auto);

        var risultato = parco.NoleggiaConCosto(4, "CLIENTE_TEST");

        Assert.Equal(auto.Id, risultato.Auto.Id);
        Assert.Equal(costoGiornaliero, risultato.Costo);
        Assert.Equal(StatoAuto.Noleggiata, auto.Stato);
    }

    [Fact]
    public void AggiungiAuto_DovrebbeAccettareAutoCoerentiConInvarianteCapacitaPrezzo()
    {
        // US2: Auto con capacità maggiore possono avere costo maggiore o uguale.
        var parco = new ParcoMezzi();
        var autoPiccola = new Auto(Guid.NewGuid(), "SMALL", 2, 30m);
        var autoMedia = new Auto(Guid.NewGuid(), "MEDIUM", 5, 50m);
        var autoGrande = new Auto(Guid.NewGuid(), "LARGE", 7, 70m);

        parco.AggiungiAuto(autoPiccola);
        parco.AggiungiAuto(autoMedia);
        parco.AggiungiAuto(autoGrande);

        Assert.Equal(3, parco.TotaleAuto);
    }

    [Fact]
    public void PrenotaBatch_DovrebbeRestituireListaDiRisultatoNoleggio()
    {
        // US3: Il batch di noleggi deve restituire una lista di risultati,
        // ognuno con l'auto noleggiata e il rispettivo costo.
        var parco = new ParcoMezzi();
        var auto1 = new Auto(Guid.NewGuid(), "AUTO1", 4, 40m);
        var auto2 = new Auto(Guid.NewGuid(), "AUTO2", 6, 60m);
        parco.AggiungiAuto(auto1);
        parco.AggiungiAuto(auto2);

        var risultatoBatch = parco.PrenotaBatch(new[] {
            RichiestaNoleggio.PerCapacita(3),
            RichiestaNoleggio.PerCapacita(5)
        }, 0m);

        var noleggi = risultatoBatch.Noleggi.ToList();
        Assert.Equal(2, noleggi.Count);
        Assert.Equal(40m, noleggi[0].Costo); // Best Fit: auto1 per 3 posti
        Assert.Equal(60m, noleggi[1].Costo); // Best Fit: auto2 per 5 posti
    }

    [Fact]
    public void PrenotaBatch_CostoTotale_DovrebbeEssereSommaCostiIndividuali()
    {
        // US3: Il costo totale del batch deve essere la somma dei costi individuali.
        var parco = new ParcoMezzi();
        parco.AggiungiAuto(new Auto(Guid.NewGuid(), "AUTO1", 2, 25m));
        parco.AggiungiAuto(new Auto(Guid.NewGuid(), "AUTO2", 4, 45m));
        parco.AggiungiAuto(new Auto(Guid.NewGuid(), "AUTO3", 7, 70m));

        var risultatoBatch = parco.PrenotaBatch(new[] {
            RichiestaNoleggio.PerCapacita(1), // 25€
            RichiestaNoleggio.PerCapacita(3), // 45€
            RichiestaNoleggio.PerCapacita(6)  // 70€
        }, 0m);

        Assert.Equal(140m, risultatoBatch.TotaleGenerale); // 25 + 45 + 70 = 140
    }
}
