// ABOUTME: Record che incapsula il risultato di un noleggio nel paradigma Funzionale.
// Include l'auto noleggiata, il costo e il NUOVO stato del parco mezzi.

using System;

namespace CarSharp.Functional;

public record RisultatoNoleggio(
    IAuto Auto, 
    decimal Costo, 
    string ClienteId, 
    ParcoMezzi ParcoAggiornato
);

public record RisultatoBatch(
    System.Collections.Immutable.ImmutableList<RisultatoNoleggio> Noleggi,
    decimal TotaleGenerale,
    ParcoMezzi ParcoAggiornato
);
