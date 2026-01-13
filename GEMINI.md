# CarSharp - Core Domain Noleggio Auto

## Panoramica del Progetto

CarSharp è una libreria C# che implementa la logica di business principale per un servizio di noleggio auto. Funge da studio comparativo tra i paradigmi **Object-Oriented (OOP)** e **Funzionale** utilizzando il C# moderno. Il sistema è una libreria backend focalizzata rigorosamente sulla gestione dello stato, sulle regole di business e sul processo decisionale.

### Tecnologie Chiave
-   **Linguaggio**: C# (.NET 10.0)
-   **Testing**: xUnit vanilla per OOP (Testing basato su esempi) e xUnit con FsCheck.Xunit (Testing basato su proprietà)

## Struttura del Progetto

```
/
├── src/
│   ├── CarSharp.Oop/               # Implementazione OOP Idiomatica
│   ├── CarSharp.Functional/        # Implementazione C# Funzionale
│   ├── CarSharp.Oop.Tests/         # Progetto di Test OOP Idiomatico
│   ├── CarSharp.Functional.Tests/  # Progetto di Test C# Funzionale
├── CarSharp.sln                # File della soluzione
├── README.md                   # Requisiti e Fasi (Italiano)
└── CLAUDE.md                   # Contesto per gli assistenti AI
```

## Workflow di Sviluppo

### Build ed Esecuzione

```bash
# Build della soluzione
dotnet build

# Esecuzione di tutti i test
dotnet test

# Esecuzione dei test in modalità continua (Watch mode)
dotnet watch test
```

### Approccio allo Sviluppo

Il progetto è costruito in **Fasi** (definite in `README.md`). Ogni fase è implementata due volte per evidenziare le differenze architettoniche tra gli stili OOP e Funzionale in C#.

### Convenzioni di Codifica

1.  **Percorso OOP**: fare riferimento alla skill peresente nel file @.skills/csharp-OOP-developer per implementazioni orientate agli oggetti idiomatiche.
2.  **Percorso Funzionale**: fare riferimento alla skill presente nel file @.skills/csharp-FP-developer per implementazioni funzionali idiomatiche.
3.  **Coerenza**: Entrambe le implementazioni devono soddisfare gli stessi requisiti di business ma attraverso paradigmi diversi.


## Concetti Chiave del Dominio

*   **Auto**: Modellata come entità in OOP, come record/valore in Funzionale.
*   **ParcoMezzi**: Una collezione che gestisce i veicoli e i loro stati.
*   **Operazioni**: Aggiungi, Rimuovi, Conteggio, Prenota (complessità incrementale).

# Italian Language Usage
All comments, documentation, and domain concepts MUST be expressed in Italian to cater to the target audience of Italian programmers.

## Future Requirements (Roadmap)

Refer to `README.md` for the full list of 10 phases, covering everything from batch processing to profit optimization.
