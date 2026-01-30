# Research: Prezzi Base (Fase 6)

**Branch**: `006-prezzi-base`
**Spec**: [spec.md](./spec.md)
**Plan**: [plan.md](./plan.md)

## Contesto

Questa ricerca affronta le decisioni tecniche per l'introduzione del costo giornaliero nel sistema di car rental. L'obiettivo è garantire che l'implementazione sia coerente con i principi della Constitution e con il contrasto paradigmatico OOP/FP già stabilito nelle fasi precedenti.

---

## Decisione 1: Rappresentazione del CostoGiornaliero

### Decision
Utilizzare `decimal` come tipo per il `CostoGiornaliero`.

### Rationale
- `decimal` è il tipo standard in C# per valori monetari: offre precisione esatta per operazioni finanziarie.
- Evita errori di arrotondamento tipici di `float`/`double`.
- Consistente con le best practice del dominio finanziario.

### Alternatives Considered
- **`double`**: Scartato per problemi di precisione (es. 0.1 + 0.2 != 0.3).
- **`int` (centesimi)**: Valido, ma aggiunge complessità di conversione; `decimal` è più idiomatico in C#.

---

## Decisione 2: Validazione dell'invariante capacità-prezzo

### Decision
Validare l'invariante al momento dell'aggiunta di un'auto al parco mezzi.

### Rationale
- **FR-004** richiede che `Capacita(A) > Capacita(B)` implichi `CostoGiornaliero(A) >= CostoGiornaliero(B)`.
- La validazione all'inserimento (eager validation) previene configurazioni incoerenti fin dall'inizio.
- Approccio coerente con il principio "Parse, don't validate" nel track FP.

### Alternatives Considered
- **Validazione lazy (a noleggio)**: Scartato; permetterebbe configurazioni incoerenti temporaneamente.
- **Validazione globale periodica**: Troppo complesso per il contesto educativo.

### Implementazione

**OOP**: Il metodo `AggiungiAuto` verificherà che non esistano auto con capacità inferiore e prezzo superiore, né auto con capacità superiore e prezzo inferiore. Lancia `ArgumentException` in caso di violazione.

**FP**: L'extension method `AggiungiAuto` restituirà `Result<ParcoMezzi>.Failure` se l'invariante è violato.

---

## Decisione 3: Struttura del risultato del noleggio

### Decision
Introdurre un nuovo tipo `RisultatoNoleggio` che incapsula sia l'auto noleggiata che il costo.

### Rationale
- Permette di restituire il costo senza modificare la firma dei metodi esistenti in modo breaking.
- Mantiene il Single Responsibility Principle: il risultato contiene tutte le informazioni del noleggio.
- Facilita l'estensione futura (es. durata, sconti).

### Alternatives Considered
- **Out parameter**: Meno idiomatico, non funziona bene con Result types nel track FP.
- **Tuple `(Auto, decimal)`**: Meno espressivo, nomi non semantici.
- **Modificare `Auto` per esporre il costo**: Viola SRP; il costo è del noleggio, non dell'auto.

### Struttura

**OOP**:
```csharp
public class RisultatoNoleggio
{
    public Auto Auto { get; }
    public decimal Costo { get; }
}
```

**FP**:
```csharp
public record RisultatoNoleggio(IAuto Auto, decimal Costo);
```

---

## Decisione 4: Calcolo costo batch

### Decision
Il costo totale del batch è la somma dei costi individuali, calcolato atomicamente.

### Rationale
- **FR-005**: Il costo totale DEVE essere la somma dei costi singoli.
- **FR-006**: Se il batch fallisce, non c'è costo (atomicità).
- Coerente con l'approccio già implementato in Fase 3.

### Implementazione

**OOP**: `NoleggiaBatch` restituisce `List<RisultatoNoleggio>` o lancia eccezione. Il chiamante può sommare i costi.

**FP**: `NoleggiaBatch` restituisce `Result<RisultatoBatch>` dove `RisultatoBatch` contiene la lista dei noleggi e il costo totale pre-calcolato.

---

## Decisione 5: Contrasto paradigmatico nei test

### Decision
Mantenere la strategia di test paradigm-specific stabilita nella Constitution.

### Rationale
- **OOP**: Example-Based testing con scenari specifici (Fiat 500 a 30€, Ford Transit a 80€, etc.).
- **FP**: Property-Based testing per verificare:
  - Invariante `CostoGiornaliero(A) >= CostoGiornaliero(B)` se `Capacita(A) >= Capacita(B)`
  - Costo batch = Somma costi individuali
  - Validazione rifiuta prezzi non positivi

### Properties da testare (FP)

1. **Invariante capacità-prezzo**: Per ogni parco valido, non esistono coppie (A, B) dove A ha maggiore capacità ma minor costo.
2. **Costo positivo**: `CostoGiornaliero > 0` per ogni auto nel parco.
3. **Costo batch sommativo**: `CostoBatch = Σ CostiIndividuali`.
4. **Atomicità**: Se batch fallisce, costo = 0 (o errore).

---

## Riepilogo

| Aspetto | Decisione |
|---------|-----------|
| Tipo costo | `decimal` |
| Validazione invariante | All'aggiunta (eager) |
| Risultato noleggio | Nuovo tipo `RisultatoNoleggio` |
| Costo batch | Somma atomica |
| Testing OOP | Example-Based con scenari specifici |
| Testing FP | Property-Based su invarianti |
