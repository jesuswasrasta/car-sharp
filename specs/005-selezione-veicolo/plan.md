# Implementation Plan - Phase 5: Scelta del mezzo

**Branch**: `005-selezione-veicolo`  
**Spec**: [spec.md](./spec.md)  
**Research**: [research.md](./research.md)

## Technical Context

### Existing Capabilities
- Gestione parco mezzi con stati (Disponibile/Noleggiata).
- Proprietà `Capacita` (posti) introdotta nella Fase 4.
- Supporto per `RichiestaNoleggio` (ID e/o Capacità).
- Batch processing atomico.

### New Requirements
- Algoritmo di selezione "Best Fit" (minima capacità sufficiente).
- Garanzia di determinismo (ordine di inserimento).
- Integrazione dell'ottimizzazione nel batch sequenziale.

### Unknowns & Clarifications
- Tutte le incertezze sono state risolte in `research.md`.

## Constitution Check

- **I. Comparative Paradigm**: Implementazione parallela OOP e FP.
- **II. Italian Language**: Logica, commenti e commit in italiano.
- **III. Test-First**: TDD obbligatorio.
- **IV. Clean Logic**: Determinismo e atomicità garantiti.

## Phase 0: Research & Foundation (DONE)
- [x] Definizione strategia di selezione ottimale in `research.md`.

## Phase 1: Design & Contracts (DONE)
- [x] Aggiornamento `data-model.md`.
- [x] Definizione contratti in `contracts/api-summary.md`.
- [x] Creazione `quickstart.md`.

## Phase 2: Implementation & Testing (NEXT)

### Task 1: OOP Track - Ottimizzazione Selezione
- Implementare la ricerca dell'auto ottimale in `ParcoMezzi.cs`.
- Ordinamento per `Capacita` e mantenimento dell'ordine di inserimento.
- Test unitari per scenari di capacità minima e determinismo.

### Task 2: FP Track - Ottimizzazione Selezione
- Aggiornare la pipeline di noleggio in `ParcoMezzi.cs` (Functional).
- Utilizzo di LINQ `OrderBy(a => a.Capacita)` sulla collezione disponibile.
- Property-Based tests per verificare l'invariante di capacità minima.

### Task 3: Batch Integration
- Verificare che `NoleggiaBatch` in entrambi i track utilizzi correttamente la nuova logica di selezione.
- Test per batch con auto multiple di diverse capacità.