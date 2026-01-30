# Tasks: Fase 6 - Prezzi Base

**Input**: Documenti di progettazione da `/specs/006-prezzi-base/`
**Prerequisiti**: plan.md, spec.md, research.md, data-model.md

**Skill (Costituzione Principio I)**: 
- OOP Tasks: `csharp-OOP-developer`
- FP Tasks: `csharp-FP-developer`

**Commenti (Costituzione Principio VII)**: 
- Ogni test e implementazione DEVE spiegare l'importanza della precisione decimale e come l'invariante commerciale capacità-prezzo protegga il modello di business.

## Path Conventions
- OOP: `src/CarSharp.Oop/`, `src/CarSharp.Oop.Tests/`
- FP: `src/CarSharp.Functional/`, `src/CarSharp.Functional.Tests/`

---

## Fase 1: Estensione Modello Auto (Foundational)

- [ ] T001 [OOP] RED: Test che Auto richieda CostoGiornaliero > 0 nel costruttore
- [ ] T002 [OOP] GREEN: Aggiungere proprietà `CostoGiornaliero` (decimal) e validazione
- [ ] T003 [FP] RED: Property test che Auto richieda CostoGiornaliero > 0
- [ ] T004 [FP] GREEN: Aggiungere `CostoGiornaliero` a IAuto e record specializzati

## Fase 2: User Story 1 - Noleggio con Costo [US1] 🎯 MVP

- [ ] T005 [OOP] Creare classe `RisultatoNoleggio` (Auto, Costo)
- [ ] T006 [OOP] RED: Test che `NoleggiaConCosto` restituisca importo corretto
- [ ] T007 [OOP] GREEN: Implementazione `NoleggiaConCosto` in `ParcoMezzi.cs`
- [ ] T008 [FP] Creare record `RisultatoNoleggio` (Auto, Costo, ParcoAggiornato)
- [ ] T009 [FP] RED: Property test che `NoleggiaConCosto` emetta il costo dell'auto
- [ ] T010 [FP] GREEN: Implementazione pipeline in `ParcoMezziExtensions.cs`

## Fase 3: User Story 2 - Invariante Capacità-Prezzo [US2]

- [ ] T011 [OOP] RED: Test che `AggiungiAuto` rifiuti mezzi più grandi che costano meno
- [ ] T012 [OOP] GREEN: Validazione preventiva all'inserimento nel parco
- [ ] T013 [FP] RED: Property test invariante capacità-prezzo nel parco
- [ ] T014 [FP] GREEN: Validazione tramite `Result.Failure` nell'estensione `AggiungiAuto`

## Fase 4: User Story 3 - Calcolo Batch [US3]

- [ ] T015 [OOP] RED: Test che `PrenotaBatch` calcoli la somma totale dei costi
- [ ] T016 [OOP] GREEN: Implementazione aggregazione costi nel batch atomico
- [ ] T017 [FP] RED: Property test atomicità e somma batch (CostoBatch = Σ Singoli)
- [ ] T018 [FP] GREEN: Implementazione trasformazione atomica con calcolo totale

## Fase 5: Rifinitura e Documentazione

- [ ] T019 Aggiornamento `README.md` con recap economico (Fase 6)
- [ ] T020 Revisione narrativa dei commenti (Fase 6)
- [ ] T021 Validazione scenari del `quickstart.md` con precisione decimale

**Checkpoint**: ✅ Fase 6 completata e documentata secondo gli standard correnti.