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
    /// Inizializza una nuova istanza dell'auto con un'identità e uno stato iniziale.
    /// </summary>
    public Auto(Guid id, string targa, StatoAuto stato)
    {
        Id = id;
        Targa = targa;
        Stato = stato;
    }
}
