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
}
