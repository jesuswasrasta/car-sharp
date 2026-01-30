# Tasks: Fase 4 - Tipologie e dimensioni dei mezzi

**Input**: Documenti di progettazione da `/specs/004-dimensioni-auto/`
**Prerequisiti**: plan.md, spec.md, research.md, data-model.md

**Skill (Costituzione Principio I)**: 
- OOP Tasks: `csharp-OOP-developer`
- FP Tasks: `csharp-FP-developer`

**Commenti (Costituzione Principio VII)**: 
- Ogni test e implementazione DEVE includere commenti discorsivi che spieghino come il sistema dei tipi (FP) o l'incapsulamento (OOP) gestiscano i nuovi vincoli di business.

## Path Conventions
- OOP: `src/CarSharp.Oop/`, `src/CarSharp.Oop.Tests/`
- FP: `src/CarSharp.Functional/`, `src/CarSharp.Functional.Tests/`

---

## Fase 1: Infrastruttura Comune e Modello Dati

- [x] T001 [OOP] Aggiungere proprietà `Capacita` a `Auto.cs` e aggiornare costruttore
- [ ] T002 [FP] Aggiungere proprietà `Capacita` al record `Auto` e tipi specializzati
- [ ] T003 [OOP] Aggiornare `ParcoMezzi.AggiungiAuto` per accettare capacità (mutazione)
- [ ] T004 [FP] Aggiornare `ParcoMezzi.AggiungiAuto` per gestire record con capacità

## Fase 2: Noleggio Singolo con Capacità (US1 & US2) 🎯 MVP

- [ ] T005 [OOP] RED: Test `Noleggia(int)` assegna auto idonea
- [ ] T006 [OOP] GREEN: Implementazione logica di ricerca (First) in `ParcoMezzi.cs`
- [ ] T007 [OOP] RED: Test fallimento per capacità insufficiente
- [ ] T008 [FP] RED: Property `NoleggiaPerCapacita` rispetta sempre l'invariante del minimo
- [ ] T009 [FP] GREEN: Implementazione pipeline LINQ filtrata in `ParcoMezziExtensions.cs`
- [ ] T010 [FP] Refactor trasformazione di stato per includere lookup per capacità

## Fase 3: Richieste Batch con Capacità (US3)

- [ ] T011 [OOP] Aggiornare `NoleggiaBatch` per gestire requisiti di capacità (Check-Then-Act)
- [ ] T012 [OOP] Aggiungere test per rifiuto atomico dovuto a violazione capacità
- [ ] T013 [FP] Aggiornare pipeline `NoleggiaBatch` per supportare requisiti di capacità
- [ ] T014 [FP] Aggiungere property test per vincoli di capacità in batch

## Fase 4: Rifinitura e Documentazione

- [ ] T015 Aggiornamento `README.md` con recap Fase 4
- [ ] T016 Creazione `comparison.md` per analisi dei vincoli (Types vs Guards)
- [ ] T017 Regressione completa `dotnet test`

**Checkpoint**: ✅ Fase 4 completata e allineata agli standard qualitativi.