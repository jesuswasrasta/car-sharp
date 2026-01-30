# Quickstart: Prezzi Base (Fase 6)

**Branch**: `006-prezzi-base`

## Esempi di Utilizzo

### Scenario 1: Noleggio singolo con costo

**OOP Track**:
```csharp
// Creo un'auto con costo giornaliero di 30€
var fiat500 = new Auto(Guid.NewGuid(), "AA000BB", StatoAuto.Disponibile, capacita: 4, costoGiornaliero: 30m);
var parco = new ParcoMezzi();
parco.AggiungiAuto(fiat500);

// Noleggio per 2 posti → Best Fit seleziona la Fiat 500
var risultato = parco.Noleggia(postiMinimi: 2);

// Il risultato contiene l'auto e il costo
Console.WriteLine($"Auto: {risultato.Auto.Targa}, Costo: {risultato.Costo}€");
// Output: Auto: AA000BB, Costo: 30€
```

**FP Track**:
```csharp
// Creo un'auto disponibile con costo
var fiat500 = new AutoDisponibile(Guid.NewGuid(), "AA000BB", Capacita: 4, CostoGiornaliero: 30m);
var parco = ParcoMezzi.Vuoto.AggiungiAuto(fiat500).Value;

// Noleggio per 2 posti
var risultato = parco.NoleggiaAuto(new RichiestaNoleggio(PostiMinimi: 2));

// Gestisco il Result
risultato.Match(
    success: r => Console.WriteLine($"Auto: {r.Auto.Targa}, Costo: {r.Costo}€"),
    failure: e => Console.WriteLine($"Errore: {e.Message}")
);
// Output: Auto: AA000BB, Costo: 30€
```

---

### Scenario 2: Invariante capacità-prezzo

**OOP Track**:
```csharp
var parco = new ParcoMezzi();

// Prima auto: 5 posti a 40€ → OK
var golf = new Auto(Guid.NewGuid(), "BB111CC", StatoAuto.Disponibile, capacita: 5, costoGiornaliero: 40m);
parco.AggiungiAuto(golf);

// Seconda auto: 7 posti a 35€ → ERRORE! Viola l'invariante
var transit = new Auto(Guid.NewGuid(), "CC222DD", StatoAuto.Disponibile, capacita: 7, costoGiornaliero: 35m);
try
{
    parco.AggiungiAuto(transit); // Lancia ArgumentException
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
    // Output: Un mezzo con capacità maggiore (7) non può costare meno (35€) di un mezzo esistente con capacità 5 e costo 40€.
}
```

**FP Track**:
```csharp
var parco = ParcoMezzi.Vuoto
    .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "BB111CC", 5, 40m))
    .Value;

// Tento di aggiungere un'auto che viola l'invariante
var risultato = parco.AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "CC222DD", 7, 35m));

risultato.Match(
    success: _ => Console.WriteLine("Auto aggiunta"),
    failure: e => Console.WriteLine($"Rifiutato: {e.Message}")
);
// Output: Rifiutato: Un mezzo con capacità maggiore non può costare meno di uno esistente.
```

---

### Scenario 3: Costo batch

**OOP Track**:
```csharp
var parco = new ParcoMezzi();
parco.AggiungiAuto(new Auto(Guid.NewGuid(), "AA000BB", StatoAuto.Disponibile, 4, 30m));
parco.AggiungiAuto(new Auto(Guid.NewGuid(), "BB111CC", StatoAuto.Disponibile, 5, 40m));
parco.AggiungiAuto(new Auto(Guid.NewGuid(), "CC222DD", StatoAuto.Disponibile, 9, 80m));

// Batch: richiedo 2 auto (3 posti e 5 posti)
var richieste = new[] {
    RichiestaNoleggio.PerCapacita(3),
    RichiestaNoleggio.PerCapacita(5)
};

var risultati = parco.NoleggiaBatch(richieste);

// Best Fit assegna: Fiat 500 (30€) e VW Golf (40€)
var costoTotale = risultati.Sum(r => r.Costo);
Console.WriteLine($"Costo totale: {costoTotale}€");
// Output: Costo totale: 70€
```

**FP Track**:
```csharp
var parco = ParcoMezzi.Vuoto
    .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "AA000BB", 4, 30m)).Value
    .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "BB111CC", 5, 40m)).Value
    .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "CC222DD", 9, 80m)).Value;

var richieste = new[] {
    new RichiestaNoleggio(PostiMinimi: 3),
    new RichiestaNoleggio(PostiMinimi: 5)
};

var risultato = parco.NoleggiaBatch(richieste);

risultato.Match(
    success: batch => Console.WriteLine($"Costo totale: {batch.CostoTotale}€"),
    failure: e => Console.WriteLine($"Batch fallito: {e.Message}")
);
// Output: Costo totale: 70€
```

---

### Scenario 4: Batch fallito (atomicità)

**OOP Track**:
```csharp
var parco = new ParcoMezzi();
parco.AggiungiAuto(new Auto(Guid.NewGuid(), "AA000BB", StatoAuto.Disponibile, 4, 30m));

// Batch con 2 richieste, ma solo 1 auto disponibile
var richieste = new[] {
    RichiestaNoleggio.PerCapacita(2),
    RichiestaNoleggio.PerCapacita(2)
};

try
{
    parco.NoleggiaBatch(richieste);
}
catch (InvalidOperationException)
{
    // Il batch è fallito: nessuna auto è stata noleggiata
    Console.WriteLine($"Auto disponibili: {parco.TotaleDisponibili}");
    // Output: Auto disponibili: 1 (stato invariato)
}
```

**FP Track**:
```csharp
var parco = ParcoMezzi.Vuoto
    .AggiungiAuto(new AutoDisponibile(Guid.NewGuid(), "AA000BB", 4, 30m)).Value;

var richieste = new[] {
    new RichiestaNoleggio(PostiMinimi: 2),
    new RichiestaNoleggio(PostiMinimi: 2)
};

var risultato = parco.NoleggiaBatch(richieste);

risultato.Match(
    success: _ => Console.WriteLine("Batch completato"),
    failure: e => Console.WriteLine($"Batch fallito, parco invariato: {parco.ConteggioDisponibili} auto disponibili")
);
// Output: Batch fallito, parco invariato: 1 auto disponibili
```
