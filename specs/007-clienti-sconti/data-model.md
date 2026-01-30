# Data Model: Clienti e Sconti

## Entità Condivise (Concettuali)

### Cliente
- **Id**: `string` (Identificativo univoco)

### Sconto
- **Valore**: `decimal` (Rappresenta la percentuale, es. 0.10 per 10%)
- **Validità**: 0 <= Valore <= 1

---

## Modello OOP (`CarSharp.Oop`)

### Classi Esistenti (Estese)

#### `RichiestaNoleggio`
- **Nuova Proprietà**: `public string ClienteId { get; }`
- **Costruttore**: Aggiornato per richiedere `ClienteId`.

#### `RisultatoNoleggio`
- **Nuova Proprietà**: `public string ClienteId { get; }`

### Nuove Classi / Strutture

#### `DettaglioCostiCliente` (DTO/Value Object)
- `ClienteId`: string
- `TotaleLordo`: decimal
- `ScontoApplicato`: decimal (Importo, non percentuale)
- `TotaleNetto`: decimal

#### `RisultatoBatch` (Aggiornamento)
- `RiepilogoClienti`: `IDictionary<string, DettaglioCostiCliente>` o `IEnumerable<DettaglioCostiCliente>`
- `TotaleGenerale`: decimal (Somma dei TotaleNetto)

---

## Modello Funzionale (`CarSharp.Functional`)

### Tipi / Record

#### `RichiestaNoleggio` (Record)
```csharp
public record RichiestaNoleggio(
    string Targa,
    int Giorni,
    string ClienteId // Nuovo campo
);
```

#### `RisultatoNoleggio` (Record)
```csharp
public record RisultatoNoleggio(
    RichiestaNoleggio Richiesta,
    Auto AutoNoleggiata,
    decimal Costo,
    string ClienteId // Nuovo campo (derivato da Richiesta)
);
```

#### `DettaglioCliente` (Record)
```csharp
public record DettaglioCliente(
    string ClienteId,
    int NumeroPrenotazioni,
    decimal TotaleLordo,
    decimal ScontoApplicato,
    decimal TotaleNetto
);
```

#### `RisultatoBatch` (Record)
```csharp
public record RisultatoBatch(
    IEnumerable<RisultatoNoleggio> NoleggiRiusciti,
    IEnumerable<Error> Errori,
    IEnumerable<DettaglioCliente> RiepilogoClienti, // Nuovo
    decimal TotaleGenerale // Nuovo
);
```

## Validazione

- **Percentuale Sconto**: Deve essere >= 0 e <= 1 (o 0-100 a seconda della convenzione scelta, useremo 0.0-1.0 decimale per calcoli).
- **Prezzo Finale**: Mai negativo.
