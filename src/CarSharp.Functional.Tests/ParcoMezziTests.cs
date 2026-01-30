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
            parco = parco.AggiungiAuto(new Auto());
        }

        return (parco.TotaleAuto == conteggio);
    }
}
