# Quickstart: Fase 1

Esempi di utilizzo per entrambi i paradigmi.

## Approccio OOP
```csharp
var parco = new ParcoMezzi();
var auto = new Auto();

parco.AggiungiAuto(auto);
Console.WriteLine(parco.TotaleAuto); // Output: 1

parco.RimuoviAuto(auto);
Console.WriteLine(parco.TotaleAuto); // Output: 0
```

## Approccio Funzionale
```csharp
var parco = ParcoMezzi.Vuoto;
var auto = new Auto();

// Le operazioni restituiscono nuove istanze
var parcoConAuto = parco.AggiungiAuto(auto);
Console.WriteLine(parcoConAuto.TotaleAuto); // Output: 1

var parcoVuoto = parcoConAuto.RimuoviAuto(auto);
Console.WriteLine(parcoVuoto.TotaleAuto); // Output: 0

// Chaining fluido
var conteggio = ParcoMezzi.Vuoto
    .AggiungiAuto(new Auto())
    .AggiungiAuto(new Auto())
    .TotaleAuto; // conteggio = 2
```
