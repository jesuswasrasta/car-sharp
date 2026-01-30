# Quickstart: Phase 5 - Scelta del mezzo

## Esempi di Utilizzo

### Scenario: Ottimizzazione Capacità
Hai tre auto disponibili:
- Fiat 500 (4 posti)
- VW Golf (5 posti)
- Ford Transit (9 posti)

Se richiedi un noleggio per **4 persone**:
```csharp
// Il sistema sceglierà la Fiat 500 (capacità minima sufficiente)
var richiesta = new RichiestaNoleggio(Posti: 4);
parcoMezzi.Noleggia(richiesta);
```

Se richiedi un noleggio per **5 persone**:
```csharp
// Il sistema sceglierà la VW Golf
var richiesta = new RichiestaNoleggio(Posti: 5);
parcoMezzi.Noleggia(richiesta);
```

### Scenario: Determinismo
Hai due auto identiche:
- Lancia Ypsilon A (4 posti, inserita alle 10:00)
- Lancia Ypsilon B (4 posti, inserita alle 10:05)

Se richiedi un noleggio per **2 persone**:
```csharp
// Il sistema sceglierà sempre la Lancia Ypsilon A
var richiesta = new RichiestaNoleggio(Posti: 2);
parcoMezzi.Noleggia(richiesta);
```

### Scenario: Batch con Concorrenza Interna
Richiesta batch: [4 persone, 4 persone].
Parco mezzi: [Fiat 500 (4 posti), VW Golf (5 posti)].

1. La prima richiesta (4 persone) prende la Fiat 500.
2. La seconda richiesta (4 persone) non può più prendere la Fiat 500 (occupata), quindi prende la VW Golf.
3. Risultato: Successo per entrambe.
