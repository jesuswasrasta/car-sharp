# CarSharp - Core Domain Noleggio Auto

## Panoramica del Progetto

CarSharp è una libreria C# che implementa la logica di business principale per un servizio di noleggio auto. Funge da studio comparativo tra i paradigmi **Object-Oriented (OOP)** e **Funzionale** utilizzando il C# moderno. Lo scopo di questo repository è educativo: mostrare come risolvere lo stesso problema con due paradigmi radicalmente diversi, evidenziandone punti di forza e compromessi. È pensato per un pubblico di programmatori esperti abituati a C# con paradigma OOP, che però vogliono approcciare un mindset funzionale.

Il sistema è una libreria backend focalizzata rigorosamente sulla gestione dello stato, sulle regole di business e sul processo decisionale.

### Tecnologie Chiave
-   **Linguaggio**: C# (.NET 10.0)
-   **Testing**: xUnit vanilla per OOP (Testing basato su esempi) e xUnit con FsCheck.Xunit (Testing basato su proprietà)

## Struttura del Progetto

```
/
├── src/
│   ├── CarSharp.Oop/               # Implementazione OOP Idiomatica
│   ├── CarSharp.Functional/        # Implementazione C# Funzionale (NO F#!)
│   ├── CarSharp.Oop.Tests/         # Progetto di Test OOP Idiomatico
│   ├── CarSharp.Functional.Tests/  # Progetto di Test C# Funzionale
├── CarSharp.sln                # File della soluzione
├── README.md                   # Requisiti e Fasi (Italiano)
├── GEMINI.md                   # Contesto per gli assistenti AI
└── .gemini/skills/             # Skills specifiche per i paradigmi
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

## Costituzione e Regole (da .specify/memory/constitution.md)

### 1. Strategia Comparativa
L'obiettivo è creare materiale educativo che confronti i paradigmi all'interno dello stesso linguaggio (**C#**).
- **C# (OOP)**: Stile idiomatico Object-Oriented. Riferimento skill: `.gemini/skills/csharp-OOP-developer.md`.
- **C# (FP)**: Stile idiomatico Funzionale (Records, Immutabilità, Funzioni Pure, Result type). **NO F#**. Riferimento skill: `.gemini/skills/csharp-FP-developer.md`.

Agisci come un insegnante che mostra come pensare in FP usando C#. Sottolinea differenze e similitudini.  
Nello scrivere le implementazioni FP, cerca di mettere in risalto i paradigmi funzionali, in modo che ci siano più esempi da discutere con il pubblico. 

### 2. Test-Driven Development (TDD)
Il codice è documentazione.
- **Granularità**: Un commit per ogni step TDD (Rosso/Verde/Refactor).
- **Chiarezza**: Codice e test devono avere commenti che spiegano il *perché*. Questi commenti sono la base per la presentazione live.
- **Commenti**: Non spiegare cos'è il TDD. Spiega le scelte di design e le differenze di paradigma.

### 3. Testing Specifico per Paradigma
- **OOP**: Example-Based Testing (`[Fact]`). Verifica transizioni di stato.
- **Functional**: Property-Based Testing (`FsCheck.Xunit`). Verifica invarianti.

### 4. Gestione Errori
- **OOP**: Eccezioni o return booleani; mutazione di stato.
- **Functional**: Tipi `Result`; funzioni pure che ritornano nuovi stati.

### 5. Uso della Lingua Italiana
Il target sono programmatori italiani.
- **Italiano**: Commenti, concetti di dominio (classi, record, variabili), documentazione, messaggi di commit.
    - Es: `classe Auto`, `record Result<Valore, Errore>`, `// Questo test verifica...`, `feat: aggiunge...`
- **Inglese**: Solo elementi tecnici (nomi librerie, pattern standard come "Repository pattern", parole chiave linguaggio).

### 6. Git Strategy
- Sviluppo tramite feature branch (`001-fase-1-parco-mezzi`).
- Merge su `main` a fine fase.
- Tag della release.

## Concetti Chiave del Dominio

*   **Auto**: Entità in OOP, Record/Valore in FP.
*   **ParcoMezzi**: Collezione che gestisce veicoli e stati.
*   **Operazioni**: Aggiungi, Rimuovi, Conteggio, Prenota (complessità incrementale).

## Roadmap
Vedi `README.md` per le 10 fasi.