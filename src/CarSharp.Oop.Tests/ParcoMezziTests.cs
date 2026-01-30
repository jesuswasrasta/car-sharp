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
            var auto = new Auto(Guid.NewGuid(), $"ABC{i}");
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
        var auto = new Auto(Guid.NewGuid(), "TEST1");
        parco.AggiungiAuto(auto);

        parco.RimuoviAuto(auto);

        Assert.Equal(0, parco.TotaleAuto);
    }
}
