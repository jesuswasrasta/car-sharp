# Tasks: Fase 3 - Richieste Batch

**Input**: Documenti di progettazione da `/specs/003-richieste-batch/`
**Prerequisiti**: plan.md, spec.md, research.md, data-model.md

**Skill (Costituzione Principio I)**: 
- OOP Tasks: `csharp-OOP-developer`
- FP Tasks: `csharp-FP-developer`

**Commenti (Costituzione Principio VII)**: 
- Ogni test e implementazione DEVE includere commenti discorsivi in italiano che spieghino la strategia di atomicità scelta.

## Path Conventions
- OOP: `src/CarSharp.Oop/`, `src/CarSharp.Oop.Tests/`
- FP: `src/CarSharp.Functional/`, `src/CarSharp.Functional.Tests/`

---

## Fase 1: Setup & Prerequisiti

- [x] T001 Verificare stato progetto e pulizia binari
- [x] T002 Assicurarsi che i runner xUnit siano operativi per entrambi i track

## Fase 2: User Story 1 - Processamento Happy Path [US1] 🎯 MVP

- [x] T003 [OOP] RED: Test `NoleggiaBatch` con auto tutte disponibili
- [ ] T004 [OOP] GREEN: Implementazione ciclo base di noleggio in `ParcoMezzi.cs`
- [ ] T005 [FP] RED: Property `NoleggiaBatch` con auto tutte disponibili
- [ ] T006 [FP] GREEN: Implementazione logica tramite `Aggregate` e `Bind`

## Fase 3: User Story 2 - Rifiuto Atomico [US2]

- [ ] T007 [OOP] RED: Test `NoleggiaBatch` fallisce se un'auto è già noleggiata (nessuna modifica)
- [ ] T008 [OOP] RED: Test `NoleggiaBatch` fallisce su duplicati nel batch
- [ ] T009 [OOP] GREEN: Refactor `NoleggiaBatch` aggiungendo validazione preventiva (Check-Then-Act)
- [ ] T010 [FP] RED: Property `NoleggiaBatch` fallisce se un'auto è inesistente o occupata
- [ ] T011 [FP] RED: Property `NoleggiaBatch` fallisce su duplicati (Parse, don't validate)
- [ ] T012 [FP] GREEN: Refactor pipeline per propagazione errori e gestione set di ID

## Fase 4: Conclusione Didattica

- [ ] T013 Aggiornamento `README.md` con recap su atomicità e transizioni in-memory
- [ ] T014 Creazione `comparison.md` con analisi tecnica Check-Then-Act vs Fold/Bind
- [ ] T015 Esecuzione regressione completa `dotnet test`

**Checkpoint**: ✅ Fase 3 completata e documentata secondo gli standard correnti.