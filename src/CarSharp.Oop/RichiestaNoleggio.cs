// ABOUTME: Rappresenta una richiesta di noleggio nel sistema.
// Può specificare un'auto particolare tramite ID, un requisito di capacità, o entrambi.

namespace CarSharp.Oop;

public class RichiestaNoleggio
{
    public Guid? IdAuto { get; }
    public int PostiMinimi { get; }

    public RichiestaNoleggio(int postiMinimi, Guid? idAuto = null)
    {
        if (postiMinimi <= 0)
            throw new ArgumentException("La capacità richiesta deve essere almeno di 1 posto.", nameof(postiMinimi));
            
        PostiMinimi = postiMinimi;
        IdAuto = idAuto;
    }

    // Factory method per facilitare la creazione di richieste solo per ID (con capacità di default 1)
    public static RichiestaNoleggio PerId(Guid id, int postiMinimi = 1) => new(postiMinimi, id);
    
    // Factory method per richieste solo per capacità
    public static RichiestaNoleggio PerCapacita(int postiMinimi) => new(postiMinimi);
}
