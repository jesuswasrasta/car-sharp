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

    [Property]
    public bool RimozioneAuto_DovrebbeDecrementareIlConteggio_QuandoLAutoEsiste(PositiveInt n)
    {
        // Nel mondo funzionale, la rimozione è una trasformazione che 'proietta' 
        // un nuovo stato del parco mezzi che non contiene più l'elemento specificato. 
        // Verifichiamo che per qualsiasi parco di dimensione N, la rimozione di 
        // un elemento esistente restituisca sempre un nuovo parco di dimensione N-1.
        var parco = ParcoMezzi.Vuoto;
        var autoList = new List<Auto>();
        for (int i = 0; i < n.Get; i++)
        {
            var auto = new Auto();
            autoList.Add(auto);
            parco = parco.AggiungiAuto(auto);
        }

        var bersaglio = autoList[new Random().Next(autoList.Count)];

        var nuovoParco = parco.RimuoviAuto(bersaglio);

        return nuovoParco.TotaleAuto == parco.TotaleAuto - 1;
    }
}
