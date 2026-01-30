// ABOUTME: Entità Auto nel paradigma OOP.
// In questa fase, l'auto acquisisce proprietà che definiscono la sua identità (Id, Targa)
// e il suo stato operativo (StatoAuto).

using System;

namespace CarSharp.Oop;

public class Auto
{
    public Guid Id { get; }
    public string Targa { get; }
    public StatoAuto Stato { get; private set; }

    public Auto(Guid id, string targa)
    {
        if (id == Guid.Empty) throw new ArgumentException("L'ID non può essere vuoto", nameof(id));
        if (string.IsNullOrWhiteSpace(targa)) throw new ArgumentException("La targa non può essere vuota", nameof(targa));

        Id = id;
        Targa = targa;
        Stato = StatoAuto.Disponibile;
    }
}