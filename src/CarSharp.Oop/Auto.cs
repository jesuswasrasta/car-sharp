// ABOUTME: Rappresentazione dell'entità Auto nel paradigma Object-Oriented.
// L'oggetto incapsula il proprio stato (Id, Targa, Stato) e ne garantisce la consistenza.

namespace CarSharp.Oop;

/// <summary>
/// Rappresenta un'auto nel sistema di noleggio.
/// In OOP, questa è un'entità con identità (Id) e stato mutabile (Stato).
/// </summary>
public class Auto
{
    /// <summary>
    /// Identificativo univoco tecnico (GUID). Stabile per tutta la vita dell'oggetto.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Identificativo di dominio (Targa). 
    /// </summary>
    public string Targa { get; }

    /// <summary>
    /// Stato operativo corrente dell'auto.
    /// In OOP, questo stato è mutabile internamente.
    /// </summary>
    public StatoAuto Stato { get; private set; }

    /// <summary>
    /// Capacità dell'auto espressa in numero di posti.
    /// </summary>
    public int Capacita { get; }

    /// <summary>
    /// Inizializza una nuova istanza dell'auto con un'identità, uno stato iniziale e la capacità.
    /// </summary>
    public Auto(Guid id, string targa, StatoAuto stato, int capacita)
    {
        if (capacita <= 0)
        {
            throw new ArgumentException("La capacità deve essere almeno di 1 posto.", nameof(capacita));
        }

        Id = id;
        Targa = targa;
        Stato = stato;
        Capacita = capacita;
    }

    /// <summary>
    /// Registra il noleggio dell'auto.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanciata se l'auto è già noleggiata.</exception>
    public void Noleggia()
    {
        // In OOP, l'oggetto è responsabile di proteggere i propri invarianti.
        // Impedire un noleggio doppio è una regola di business critica.
        if (Stato == StatoAuto.Noleggiata)
        {
            throw new InvalidOperationException("L'auto è già noleggiata.");
        }

        Stato = StatoAuto.Noleggiata;
    }

    /// <summary>
    /// Registra la restituzione dell'auto.
    /// </summary>
    public void Restituisci()
    {
        // L'operazione di restituzione è idempotente in questa fase, 
        // ma muta lo stato verso Disponibile.
        Stato = StatoAuto.Disponibile;
    }
}
