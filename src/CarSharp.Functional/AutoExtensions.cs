// ABOUTME: Estensioni per le trasformazioni di stato delle auto.
// In FP, le "transizioni" sono funzioni pure che mappano un tipo in un altro.

namespace CarSharp.Functional;

public static class AutoExtensions
{
    /// <summary>
    /// Trasforma un'AutoDisponibile in AutoNoleggiata.
    /// È una funzione pura: non modifica l'oggetto originale ma ne restituisce uno nuovo.
    /// </summary>
    public static AutoNoleggiata Noleggia(this AutoDisponibile auto)
    {
        return new AutoNoleggiata(auto.Id, auto.Targa, auto.Capacita);
    }

    /// <summary>
    /// Trasforma un'AutoNoleggiata in AutoDisponibile.
    /// Ripristina lo stato iniziale.
    /// </summary>
    public static AutoDisponibile Restituisci(this AutoNoleggiata auto)
    {
        return new AutoDisponibile(auto.Id, auto.Targa, auto.Capacita);
    }
}