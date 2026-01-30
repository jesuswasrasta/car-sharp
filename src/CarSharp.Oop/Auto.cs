// ABOUTME: Entità Auto nel paradigma OOP.
// In questa fase, l'auto acquisisce proprietà che definiscono la sua identità (Id, Targa)
// e il suo stato operativo (StatoAuto).

using System;

namespace CarSharp.Oop;

public class Auto
{
    public Guid Id { get; }
    public string Targa { get; }
    public int Capacita { get; }
    public decimal CostoGiornaliero { get; }
    public StatoAuto Stato { get; private set; }

    public Auto(Guid id, string targa, int capacita, decimal costoGiornaliero)
    {
        if (id == Guid.Empty) throw new ArgumentException("L'ID non può essere vuoto", nameof(id));
        if (string.IsNullOrWhiteSpace(targa)) throw new ArgumentException("La targa non può essere vuota", nameof(targa));
        if (capacita <= 0) throw new ArgumentException("La capacità deve essere positiva", nameof(capacita));
        
        // Invariante commerciale: il prezzo deve essere una quantità positiva.
        // Utilizziamo decimal per evitare errori di precisione nel calcolo dei costi.
        if (costoGiornaliero <= 0) throw new ArgumentException("Il costo giornaliero deve essere positivo", nameof(costoGiornaliero));

        Id = id;
        Targa = targa;
        Capacita = capacita;
        CostoGiornaliero = costoGiornaliero;
        Stato = StatoAuto.Disponibile;
    }

    public void Noleggia()
    {
        if (Stato == StatoAuto.Noleggiata)
        {
            throw new InvalidOperationException($"L'auto con targa {Targa} è già noleggiata.");
        }

        Stato = StatoAuto.Noleggiata;
    }

    public void Restituisci()
    {
        Stato = StatoAuto.Disponibile;
    }
}