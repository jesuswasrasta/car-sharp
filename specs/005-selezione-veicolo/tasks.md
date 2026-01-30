# Tasks: Fase 5 - Scelta del mezzo

**Input**: Documenti di progettazione da `/specs/005-selezione-veicolo/`
**Prerequisiti**: plan.md, spec.md, research.md, data-model.md

**Skill (Costituzione Principio I)**: 
- OOP Tasks: `csharp-OOP-developer`
- FP Tasks: `csharp-FP-developer`

**Commenti (Costituzione Principio VII)**: 
- Ogni test e implementazione DEVE spiegare il *perché* dell'algoritmo Best Fit e come il determinismo sia garantito dal paradigma scelto.

## Path Conventions
- OOP: `src/CarSharp.Oop/`, `src/CarSharp.Oop.Tests/`
- FP: `src/CarSharp.Functional/`, `src/CarSharp.Functional.Tests/`

---

## Fase 1: Setup e Validazione Iniziale

- [x] T001 Verificare stato build del progetto
- [x] T002 Verificare che tutti i test esistenti passino (regressione)

## Fase 2: Fondamentali (Validazione Richieste)

- [x] T003 [OOP] RED: Test validazione capacità richiesta (fail-fast su <= 0)
- [x] T004 [OOP] GREEN: Implementazione validazione nel costruttore di `RichiestaNoleggio.cs`
- [x] T005 [FP] RED: Property test validazione capacità richiesta (Failure su <= 0)
- [x] T006 [FP] GREEN: Implementazione validazione nella factory di `RichiestaNoleggio.cs`

## Fase 3: User Story 1 - Selezione Best Fit [US1] 🎯 MVP

- [x] T007 [OOP] RED: Test "Best Fit" (sceglie auto più piccola sufficiente)
- [x] T008 [OOP] GREEN: Implementazione `OrderBy(Capacita)` in `ParcoMezzi.cs`
- [ ] T009 [FP] RED: Property "Capacity Invariant" (auto scelta sempre >= richiesta)
- [ ] T010 [FP] GREEN: Implementazione pipeline LINQ con `OrderBy` in `ParcoMezzi.cs`

## Fase 4: User Story 2 - Determinismo [US2]

- [ ] T011 [OOP] RED: Test determinismo (prima inserita vince a parità di capacità)
- [ ] T012 [OOP] GREEN: Verificare stabilità dell'ordinamento
- [ ] T013 [FP] RED: Property determinismo (stesso stato + stessa richiesta = stesso risultato)
- [ ] T014 [FP] GREEN: Verificare stabilità dell'ordinamento nella pipeline pura

## Fase 5: User Story 3 - Ottimizzazione in Batch [US3]

- [ ] T015 [OOP] RED: Test ottimizzazione sequenziale nel batch
- [ ] T016 [OOP] GREEN: Assicurarsi che `NoleggiaBatch` utilizzi la nuova logica di selezione
- [ ] T017 [FP] RED: Property ottimizzazione batch
- [ ] T018 [FP] GREEN: Assicurarsi che `NoleggiaBatch` componga correttamente le selezioni Best Fit

## Fase 6: Rifinitura e Documentazione

- [ ] T019 Aggiornamento `README.md` con recap su Best Fit e Determinismo
- [ ] T020 Revisione narrativa dei commenti (Fase 5)
- [ ] T021 Validazione scenari del `quickstart.md`

**Checkpoint**: ✅ Fase 5 completata e allineata agli standard qualitativi.