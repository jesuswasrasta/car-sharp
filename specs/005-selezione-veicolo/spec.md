# Feature Specification: Fase 5: Scelta del mezzo (Ottimizzazione Selezione)

**Feature Branch**: `005-selezione-veicolo`  
**Created**: 2026-01-13  
**Status**: Draft  
**Input**: User description: "Fase 5: Scelta del mezzo - Ottimizzazione della selezione basata sulla capacità residua minima"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Selezione ottimizzata per capacità (Priority: P1)

Come gestore del parco mezzi, voglio che il sistema assegni l'auto più piccola possibile che soddisfi la richiesta di posti, in modo da preservare le auto più grandi per richieste future che potrebbero averne bisogno.

**Why this priority**: Rappresenta il valore di business principale di questa fase: l'efficienza nell'uso delle risorse.

**Independent Test**: Può essere testato creando un parco mezzi con auto di diverse capacità (es. 4 posti e 7 posti) e richiedendo un noleggio per 4 persone. Il sistema deve assegnare l'auto da 4 posti.

**Acceptance Scenarios**:

1. **Given** un parco mezzi con un'auto A (4 posti) e un'auto B (7 posti) entrambe disponibili, **When** viene richiesta un'auto per 4 persone, **Then** viene assegnata l'auto A.
2. **Given** un parco mezzi con un'auto A (4 posti) occupata e un'auto B (7 posti) disponibile, **When** viene richiesta un'auto per 4 persone, **Then** viene assegnata l'auto B (essendo l'unica disponibile che soddisfa il requisito).

---

### User Story 2 - Determinismo in caso di parità (Priority: P2)

Come sviluppatore, voglio che la scelta del mezzo sia deterministica quando più auto hanno la stessa capacità ottimale, seguendo l'ordine di inserimento nel parco mezzi.

**Why this priority**: Garantisce la prevedibilità del sistema e facilita il testing e il debugging.

**Independent Test**: Può essere testato inserendo due auto identiche per capacità e verificando che venga scelta sempre la prima inserita.

**Acceptance Scenarios**:

1. **Given** un parco mezzi dove l'auto A (5 posti) è stata inserita prima dell'auto B (5 posti), **When** viene richiesta un'auto per 5 persone, **Then** viene assegnata l'auto A.

---

### User Story 3 - Ottimizzazione in batch (Priority: P3)

Come utente che effettua prenotazioni multiple, voglio che ogni singola richiesta nel batch venga ottimizzata singolarmente secondo la logica del miglior incastro.

**Why this priority**: Estende l'ottimizzazione alla funzionalità batch introdotta nella fase 3, garantendo coerenza in tutto il sistema.

**Independent Test**: Può essere testato inviando una richiesta batch con diverse capacità e verificando che ogni auto assegnata sia quella ottimale per quella specifica riga.

**Acceptance Scenarios**:

1. **Given** un parco mezzi con auto da 2, 4, 5 e 7 posti, **When** viene inviata una richiesta batch per [2 posti, 4 posti], **Then** il sistema assegna rispettivamente l'auto da 2 e quella da 4, non quella da 5 o 7.

### Edge Cases

- **Capacità superiore al massimo**: Cosa succede se viene richiesta una capacità superiore a quella di qualsiasi auto (anche la più grande) nel parco mezzi? (Deve fallire).
- **Parco mezzi vuoto**: Come si comporta il sistema se viene fatta una richiesta su un parco mezzi senza auto? (Deve fallire).
- **Parità di capacità**: Come gestisce il sistema la scelta tra due auto con identica capacità ottimale? (Deve usare l'ordine di inserimento).
- **Capacità non valida**: Come vengono gestite richieste con capacità zero o negativa? (Devono essere rifiutate: in OOP tramite eccezione, in FP tramite un `Result` di fallimento).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Il sistema DEVE selezionare, tra le auto disponibili, quella con la capacità minima che sia maggiore o uguale ai posti richiesti.
- **FR-002**: In caso di più auto con la stessa capacità ottimale, il sistema DEVE selezionare quella con l'ordine di inserimento più vecchio (determinismo).
- **FR-003**: La logica di ottimizzazione DEVE essere applicata a ogni elemento di una richiesta batch.
- **FR-004**: Se nessuna auto disponibile soddisfa la capacità richiesta, la richiesta (o l'intero batch) DEVE fallire.
- **FR-005**: Se viene specificato un ID specifico nella richiesta, l'ottimizzazione è ignorata e il sistema DEVE verificare solo la disponibilità e la capacità di quel mezzo specifico (ereditato da Fase 4).
  - **Batch Behavior**: In una richiesta batch con elementi misti (alcuni con ID specifico, altri con solo capacità), ogni elemento applica la sua logica: ID → verifica esatta; Capacità → ottimizzazione Best Fit.

### Key Entities *(include if feature involves data)*

- **RichiestaNoleggio**: Rappresenta il desiderio dell'utente, specificando opzionalmente l'ID del mezzo e/o la capacità richiesta (posti).
- **Auto**: Entità del dominio che ora include la proprietà `Capacita` (posti) utilizzata per il calcolo dell'ottimizzazione.
- **ParcoMezzi**: Gestisce la collezione di auto e implementa l'algoritmo di selezione ottimizzata.

## Success Criteria *(obbligatorio)*

### Risultati Misurabili

- **SC-001**: Il 100% dei noleggi basati su capacità deve assegnare il mezzo con il minor numero di posti in eccesso (Best Fit).
- **SC-002**: Il sistema deve garantire il determinismo assoluto: a parità di stato iniziale e sequenza di richieste, il risultato deve essere identico al 100%.
- **SC-003**: In caso di parità di capacità ottimale, la scelta deve ricadere costantemente sul mezzo inserito per primo nel parco (stabilità della selezione).
- **SC-004**: L'algoritmo di selezione deve mantenere una complessità temporale efficiente anche con l'aumentare dei vincoli di ordinamento.

## Assunzioni

- L'ordine di inserimento nel parco mezzi è l'unico criterio di parità (non si utilizzano timestamp o casualità).
- L'ottimizzazione Best Fit ha la priorità su qualsiasi altro criterio di selezione automatica.
- Se una richiesta specifica un ID, l'algoritmo di ottimizzazione viene ignorato a favore del vincolo esatto.
- Il sistema non tenta di riorganizzare i noleggi già effettuati per fare spazio a nuove richieste (nessuna ottimizzazione retroattiva).